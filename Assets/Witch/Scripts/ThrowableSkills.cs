using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using danielnyan;

public class ThrowableSkills : SkillCategory
{
    public override void HandleInput(WitchMovement player, int index)
    {
        if (Input.GetMouseButtonDown(0) && player.isGrounded)
        {
            player.charAnimator.Throw();
            GameObject currentAction = Instantiate(skills[index]);
            currentAction.transform.position = player.handPosition.position;
            currentAction.transform.localScale = player.transform.localScale;
        }
    }
}
