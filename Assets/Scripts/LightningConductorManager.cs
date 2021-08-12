using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningConductorManager : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 0.2f;
    [SerializeField]
    private GameObject lightning;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;

    private float currentInterval;

    // Start is called before the first frame update
    void Start()
    {
        currentInterval = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInterval < spawnInterval)
        {
            currentInterval += spawnInterval;
            GameObject currentLightning = Instantiate(lightning, 
                start.position, Quaternion.identity);
            LightningConductorDrawer drawer = 
                currentLightning.GetComponent<LightningConductorDrawer>();
            drawer.target = end;
            currentLightning.SetActive(true);
        }
        currentInterval -= Time.deltaTime;
    }
}
