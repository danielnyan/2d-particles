using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimedProjectile : MonoBehaviour
{
    public abstract void SetupProjectile(Vector3 target);
}
