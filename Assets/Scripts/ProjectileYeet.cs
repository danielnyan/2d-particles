using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileYeet : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform yeetDirection;
    public float directionPerturbation;
    public float yeetVelocity;
    public float velocityPerturbation;
    public int yeetAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < yeetAmount; i++)
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject instance = Instantiate(spawnObject, transform.position, Quaternion.identity);
        Vector3 direction = yeetDirection.up + Random.insideUnitSphere * directionPerturbation;
        float velocity = yeetVelocity + Random.Range(-1, 1) * velocityPerturbation;
        instance.GetComponent<Rigidbody2D>().velocity = 
            direction * velocity;
    }
}
