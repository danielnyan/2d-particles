using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedProjectileSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 target;
    public int spawnNumber = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject instance = Instantiate(spawnObject, transform.position, Quaternion.identity);
            instance.GetComponent<AimedProjectile>().SetupProjectile(target);
        }
    }
}
