using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using danielnyan;

public class ContinuousSkills : SkillCategory
{
    GameObject currentAction = null;
    public override void HandleInput(WitchMovement player, int index)
    {
        if (Input.GetMouseButtonDown(0) && player.isGrounded)
        {
            player.charAnimator.StartPray();
            player.movementController.Stationary();
            player.movementController.MovementEnabled = false;
            player.isBusy = true;

            currentAction = Instantiate(skills[index]);
            currentAction.transform.position = player.handPosition.position;
            currentAction.transform.localScale = player.transform.localScale;
        }
        if (Input.GetMouseButtonUp(0))
        {
            player.charAnimator.EndPray();
            if (currentAction != null)
                currentAction.GetComponent<ContinuousProjectile>().KillProjectile();
            currentAction = null;
            player.isBusy = false;
            player.movementController.MovementEnabled = true;
        }
    }
}
