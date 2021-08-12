using UnityEngine;

namespace danielnyan
{
    public class SplashProjectile : MonoBehaviour
    {
        public GameObject instantiatedObject;
        public bool killParent;
        public float expiryTime = 0f;

        private float currentExpiryTime;

        private void Start()
        {
            currentExpiryTime = expiryTime;
        }

        private void Update()
        {
            if (expiryTime != 0f)
            {
                currentExpiryTime -= Time.deltaTime;
                if (currentExpiryTime < 0f)
                {
                    PerformSplash();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                PerformSplash();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PerformSplash();
        }

        private void PerformSplash()
        {
            if (instantiatedObject != null)
            {
                Instantiate(instantiatedObject, transform.position, Quaternion.identity);
            }
            if (killParent)
            {
                ContinuousProjectile k = transform.parent.GetComponent<ContinuousProjectile>();
                if (k != null)
                {
                    k.KillProjectile();
                }
            }
            Destroy(gameObject);
        }
    }
}
