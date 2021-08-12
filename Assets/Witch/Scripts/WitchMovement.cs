using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace danielnyan
{
    public class WitchMovement : MonoBehaviour
    {
        public Transform handPosition;

        public MovementController movementController;
        public WitchAnimator charAnimator;

        private int isMoving; //if isMoving is -1, it is moving left, if 1, it is moving right, else it is not moving
        private bool isJumping;
        public bool isGrounded;

        [SerializeField]
        public float movementSpeed;

        [SerializeField]
        public float jumpForce;

        [SerializeField]
        private GameObject flamethrower;
        [SerializeField]
        private GameObject nuke;

        public bool isBusy = false;
        private GameObject currentAction;
        private string currentActionString;

        // Start is called before the first frame update
        private void Start()
        {
            movementController = GetComponent<MovementController>();
            charAnimator = GetComponent<WitchAnimator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.position.y < -30f)
            {
                transform.position = new Vector3(12, -7, 0);
            }
            if (!isBusy)
            {
                if (Input.GetAxisRaw("Vertical") > 0 && isGrounded)
                {
                    isJumping = true;
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    isMoving = 1;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    isMoving = -1;
                }
                else
                {
                    isMoving = 0;
                }
            }
        }

        private void FixedUpdate()
        {
            if (isJumping)
            {
                movementController.Jump(jumpForce);
                charAnimator.Jump();
                charAnimator.Move(false);
                charAnimator.Ground(false);
                isGrounded = false;
                isJumping = false;
            }

            if (isMoving < 0)
            {
                movementController.MoveLeft(movementSpeed);
                charAnimator.faceLeft();
                if (isGrounded)
                {
                    charAnimator.Move();
                }
            }
            else if (isMoving > 0)
            {
                movementController.MoveRight(movementSpeed);
                charAnimator.faceRight();
                if (isGrounded)
                {
                    charAnimator.Move();
                }
            }
            else
            {
                movementController.Stationary();
                charAnimator.Move(false);
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckGrounded();
        }

        public void CheckGrounded()
        {
            Vector2[] rays = { new Vector2(0f, 0f), new Vector2(0.5f, 0f), new Vector2(-0.5f, 0f), new Vector2(0.25f, 0f), new Vector2(-0.25f, 0f) };
            Collider2D collider2d = GetComponent<Collider2D>();
            float distance = collider2d.bounds.extents.y + 0.1f;
            float rayLength = (new Vector2(-0.5f, distance)).magnitude;
            int hits = 0;

            foreach (Vector2 displace in rays)
            {
                RaycastHit2D[] hitArray = new RaycastHit2D[3];
                collider2d.Raycast(new Vector2(0f, -distance) + displace, hitArray, rayLength, 1 << LayerMask.NameToLayer("Ground"));

                if (hitArray[0].collider != null && hitArray[0].collider.tag == "Ground")
                {
                    hits++;
                }
            }
            if (hits > rays.Length / 2)
            {
                if (!isGrounded)
                {
                    charAnimator.Ground();
                }
                isGrounded = true;
            }
        }
    }
}
