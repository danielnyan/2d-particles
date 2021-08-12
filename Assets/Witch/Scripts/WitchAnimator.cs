using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace danielnyan
{
    public class WitchAnimator : CharacterAnimator
    {
        public void StartThrust()
        {
            animator.SetTrigger("Thrust Forward");
        }

        public void EndThrust()
        {
            animator.SetTrigger("End Thrust");
        }

        public void Throw()
        {
            animator.SetTrigger("Swipe Up");
        }

        public void StartPray()
        {
            animator.SetBool("Pray", true);
        }

        public void EndPray()
        {
            animator.SetBool("Pray", false);
        }
    }
}
