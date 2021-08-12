using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningConductorDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private bool isReady = false;
    [SerializeField]
    private bool isInitialised = false;
    private Vector3 normal;
    private Vector3 binormal;
    private Vector3 distance;
    private Vector2 previousNoise = Vector2.zero;
    private Vector2 previousOutput = Vector2.zero;

    [SerializeField]
    // Time taken before next lightning is generated
    private float delay = 0.04f;

    [SerializeField]
    // Lightning length
    private int chainSize = 5;

    [SerializeField]
    // Jump distance
    private float jumpDistance = 0.5f;

    /* The offsets in the normal and binormal directions are calculated 
    using z_n = smoothness * z_(n-1) + a_n + damping * a_(n-1)
    where a_n = noiseFactor * Random.insideUnitCircle */
    [SerializeField]
    /* Values between -1 to 0 makes the bolt less chaotic; any other 
    values aggravates the effect of the previous noise value. */
    private float damping = -1f;
    [SerializeField]
    // Scales the effect of the random perturbations
    private float noiseFactor = 0.8f;
    [SerializeField]
    /* Values close to 0 makes the bolt look like random noise while 
    values close to 1 makes the bolt follow an arc more often. 
    Any values outside 0 to 1 creates funky behaviour. */
    private float smoothness = 1f;

    [SerializeField]
    private float killDistance = 0.6f;

    public Transform target;

    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isReady = true;
    }

    private void OnEnable()
    {
        StartCoroutine(HandleLightning());
    }

    private void OnDisable()
    {
        StopCoroutine(HandleLightning());
        isInitialised = false;
    }

    private void UpdateNormals()
    {
        distance = target.position - lineRenderer.GetPosition(chainSize - 1);
        Vector3 direction = distance.normalized;
        if (direction == Vector3.up || direction == -Vector3.up)
        {
            normal = Vector3.forward;
        }
        else
        {
            normal = Vector3.up;
        }
        Vector3.OrthoNormalize(ref direction, ref normal);
        if (direction == Vector3.right || direction == -Vector3.right)
        {
            binormal = Vector3.forward;
        }
        else
        {
            binormal = Vector3.right;
        }
        Vector3.OrthoNormalize(ref direction, ref binormal);
    }

    private Vector2 GenerateNoise()
    {
        Vector2 noise = Random.insideUnitCircle * noiseFactor;
        Vector2 output = smoothness * previousOutput + noise + previousNoise * damping;

        previousNoise = noise;
        previousOutput = output;

        return output;
    }

    // Update is called once per frame
    private IEnumerator HandleLightning()
    {
        while (true)
        {
            if (!enabled)
            {
                yield break;
            }
            if (!isReady)
            {
                yield return new WaitForSeconds(0.02f);
            }
            if (!isInitialised)
            {
                lineRenderer.positionCount = chainSize;
                Vector3 perturbation = Random.insideUnitSphere * noiseFactor;
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, transform.position + perturbation);
                }
                isInitialised = true;
                yield return new WaitForSeconds(0.02f);
            }

            if (isInitialised)
            {
                UpdateNormals();
            }

            Vector3[] oldPositions = new Vector3[chainSize];
            lineRenderer.GetPositions(oldPositions);

            for (int i = 0; i < chainSize - 1; i++)
            {
                oldPositions[i] = oldPositions[i + 1];
            }

            Vector2 offset = GenerateNoise();

            Vector3 finalPosition = oldPositions[chainSize - 1];
            finalPosition += distance.normalized * jumpDistance;
            finalPosition += normal * offset.x;
            finalPosition += binormal * offset.y;
            oldPositions[chainSize - 1] = finalPosition;

            if ((finalPosition - target.position).magnitude < killDistance)
            {
                Destroy(gameObject);
            }

            lineRenderer.SetPositions(oldPositions);

            if (delay == 0f)
            {
                yield break;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
