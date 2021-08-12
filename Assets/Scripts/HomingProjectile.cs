using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : AimedProjectile
{
    public float velocity;
    public Vector3 target;
    public float rotateSpeed;
    public float hitThreshold;
    public float anglePerturbation;

    private Rigidbody2D rb;
    private bool isReady = false;
    [SerializeField]
    private bool hasHit = false;
    private float rotateAmount;

    public override void SetupProjectile(Vector3 target)
    {
        this.target = target;
        Vector2 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI +
            Random.Range(-1f, 1f) * anglePerturbation;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        rb = GetComponent<Rigidbody2D>();
        isReady = true;
    }

    private void FixedUpdate()
    {
        if (isReady)
        {
            if (!hasHit)
            {
                Vector2 direction = target - transform.position;
                direction.Normalize();
                rotateAmount = -Vector3.Cross(direction, transform.up).z;
                rb.angularVelocity = rotateAmount * rotateSpeed;
                rb.velocity = transform.up * velocity;

                if ((transform.position - target).magnitude < hitThreshold)
                {
                    hasHit = true;
                }
            } 
            else
            {
                rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f,
                    Time.fixedDeltaTime * 10f);
            }
        }
    }
}
