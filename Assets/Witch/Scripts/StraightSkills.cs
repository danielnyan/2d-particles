using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using danielnyan;

public class StraightSkills : SkillCategory
{
    public override void HandleInput(WitchMovement player, int index)
    {
        if (Input.GetMouseButtonDown(0) && player.isGrounded)
        {
            StartCoroutine(CastSkill(player, index));
        }
    }

    public IEnumerator CastSkill(WitchMovement player, int index)
    {
        player.charAnimator.StartThrust();
        player.movementController.Stationary();
        player.movementController.MovementEnabled = false;
        player.isBusy = true;

        GameObject currentAction = Instantiate(skills[index]);
        currentAction.transform.position = player.handPosition.position;
        currentAction.transform.localScale = player.transform.localScale;
        Destroy(currentAction, 4f);
        yield return new WaitForSeconds(0.5f);

        player.charAnimator.EndThrust();
        player.isBusy = false;
        player.movementController.MovementEnabled = true;
    }
}
