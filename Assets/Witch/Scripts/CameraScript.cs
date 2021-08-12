using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector2 velocity;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float smoothTime = 0.5f;

    void FixedUpdate()
    {
        if (player != null && player.gameObject.activeSelf)
        {
            //Let the initial position x_0 = 0, the final position x_inf = 1, and let
            //x_(i+1) = (1-t)(x_i) + t(1), which is iterated once per fixed update (default 0.02s).
            //Then, x_i = 1 - (1-t)^i. If x_k = 0.9, then (1-t)^k = 0.1. Thus,

            float smoothingFactor = 1f - Mathf.Pow(0.1f, Time.deltaTime / smoothTime);

            float posX = Mathf.Lerp(transform.position.x, player.transform.position.x, smoothingFactor);
            float posY = Mathf.Lerp(transform.position.y, player.transform.position.y, smoothingFactor);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

    private void Update()
    {
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y;
        if (Camera.main.orthographicSize > 20f)
        {
            Camera.main.orthographicSize = 20f;
        } else if (Camera.main.orthographicSize < 3f)
        {
            Camera.main.orthographicSize = 3f;
        }
    }
}
