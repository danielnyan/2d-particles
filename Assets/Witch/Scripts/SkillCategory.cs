using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using danielnyan;

public abstract class SkillCategory : MonoBehaviour
{
    public GameObject[] skills;

    public abstract void HandleInput(WitchMovement player, int index);
}
