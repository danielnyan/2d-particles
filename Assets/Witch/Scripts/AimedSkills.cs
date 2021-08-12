using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using danielnyan;

public class AimedSkills : SkillCategory
{
    public override void HandleInput(WitchMovement player, int index)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.charAnimator.Throw();
            GameObject currentAction = Instantiate(skills[index]);
            currentAction.transform.position = player.handPosition.position;
            currentAction.transform.localScale = player.transform.localScale;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentAction.GetComponent<AimedProjectileSpawner>().target = mousePos;
            currentAction.SetActive(true);
        }
    }
}
