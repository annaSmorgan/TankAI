using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class SleepState 
    {
        public void Rest(Tank owner)
        {
            owner.timer = 0.0f;
            owner.timer += Time.deltaTime;

            if (owner.timer > 10.0f)
            {
                owner.currentState = TankState.WANDER;
            }

            owner.NavComponent.isStopped = true;
        }
    }
}