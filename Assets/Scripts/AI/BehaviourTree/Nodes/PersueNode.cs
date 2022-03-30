using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT  //within the bahaviour tree namespace
{
    public class PersueNode : Node //handles chasing the target until its at the attacking range
    {
        private Transform target; //holds the target which is inputted when node is called
        private float distance;
        public PersueNode(Tank a_owner, Transform a_target) : base(a_owner)
        {
            target = a_target;
        }

        public override NodeState Execute()
        {
            owner.NavComponent.isStopped = false; //moving

            distance = Vector3.Distance(target.position, owner.transform.position); //calculate the distance to target
            if (distance > owner.sharedInfo.attackRange) //if its more than the attack range
            {
                owner.NavComponent.speed = owner.sharedInfo.tankSpeed; //change the speed to the standard/ chase speed
                owner.NavComponent.SetDestination(target.position); //set the destination to the target
                owner.covered = false; //not in cover
                return NodeState.RUNNING; //return running as not at the destination yet

            }
            else if (distance <= owner.sharedInfo.attackRange) //if its within or at the attacking range
            {
                return NodeState.SUCCESS; //return succes so it can move onto attacking the target
            }

            return NodeState.FAILURE;
        }
    }

}
