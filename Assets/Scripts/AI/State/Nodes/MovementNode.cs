using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Utility //within the Utility (now state) namespace
{
    public class MovementNode //used to move the tank then stop when within stopping distance
    {
        private float distance;
 
        public void Wandering(Tank owner)
        {

            if (owner.target != null)//if there is target
            {
                distance = Vector3.Distance(owner.wanderTarget, owner.transform.position);//calculate dsiatnce to target

                if (distance > owner.sharedInfo.stoppingDistance)//if it is greater than stopping distance
                {
                    owner.NavComponent.isStopped = false;//move 
                    owner.covered = false;//not in cover
                    owner.NavComponent.speed = owner.sharedInfo.wanderSpeed; //set the speed to wander
                    owner.NavComponent.SetDestination(owner.wanderTarget);//move to the destination
                    Debug.Log("wandering");
                }
                else //once at stopping distance
                {
                    owner.NavComponent.isStopped = true;//stop moving

                }
            }
        }

    }

}
