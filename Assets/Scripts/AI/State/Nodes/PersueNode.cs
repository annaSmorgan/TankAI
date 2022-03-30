using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility  //within the Utility (now state) namespace
{
    public class PersueNode  //handles chasing the target until its at the attacking range
    {
        private Transform target;//holds the target which is inputted when node is called
        private float distance;

        public void Chase(Tank owner, Transform a_target)
        {
            target = a_target;

            owner.NavComponent.isStopped = false;//moving

            distance = Vector3.Distance(target.position, owner.transform.position); //calculate the distance to target
            if (distance > owner.sharedInfo.attackRange) //if its more than the attack range
            {
                owner.NavComponent.speed = owner.sharedInfo.tankSpeed; //change the speed to the standard/ chase speed
                owner.NavComponent.SetDestination(target.position); //set the destination to the target
                owner.covered = false; //not in cover

            }
            else if (distance <= owner.sharedInfo.attackRange) //if its within or at the attacking range
            {
                owner.NavComponent.isStopped = true; //stop moving
            }

        }
    }

}
