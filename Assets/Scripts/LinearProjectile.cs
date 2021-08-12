using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : AimedProjectile
{
    public float directionPerturbation;
    public float yeetVelocity;
    public float velocityPerturbation;
    public Vector3 target;

    public override void SetupProjectile(Vector3 target)
    {
        this.target = target;
        Vector3 direction = (target - transform.position).normalized + Random.insideUnitSphere * directionPerturbation;
        float velocity = yeetVelocity + Random.Range(-1, 1) * velocityPerturbation;
        GetComponent<Rigidbody2D>().velocity = direction * velocity;
    }
}
