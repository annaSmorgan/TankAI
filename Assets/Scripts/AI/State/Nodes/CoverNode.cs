using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility //within the Utility (now state) namespace
{
    public class CoverNode //preforms the action of getting the tank to the nearest cover point which is away from the enemy
    {
        private float distance;

        public void Hide(Tank owner, Transform target)
        {

            if (target != null)//if there is a target
            {

                distance = Vector3.Distance(target.position, owner.transform.position);//calculate the distance

                if (distance > owner.sharedInfo.stoppingDistance)//if the target is bigger than the stopping distance
                {
                    owner.NavComponent.isStopped = false;//is moving
                    owner.covered = false;//set cover check to false
                    owner.NavComponent.speed = owner.sharedInfo.retreatSpeed; //set the speed to the retreat speed
                    owner.NavComponent.SetDestination(target.position); //move to the destination
                    Debug.Log("hiding");
                }
                else//if within stopping distance
                {
                    owner.NavComponent.isStopped = true; //stop moving
                    Debug.Log("hidden");
                    owner.covered = true;//set the cover check to true as the tank is now in cover
                }

            }

            owner.NavComponent.isStopped = true;//stop moving
            Debug.Log("hide fail");
        }

    }

}
