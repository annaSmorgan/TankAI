using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BT //within the bahaviour tree namespace
{
    public class MovementNode : Node //this node was planned to deal with all types of movement so it uses the global target 
    {//was going to use to it move the tank to certain locations while in the scout sequence (scout was removed)
        private float distance;
        public MovementNode(Tank a_owner) : base(a_owner)
        {
        }

        public override NodeState Execute()
        {

            if (owner.target != null) //if there is target
            {
                distance = Vector3.Distance(owner.target, owner.transform.position); //calculate dsiatnce to target

                if (distance > owner.sharedInfo.stoppingDistance) //if it is greater than stopping distance
                {
                    owner.NavComponent.isStopped = false; //move 
                    owner.covered = false; //not in cover
                    owner.NavComponent.speed = owner.sharedInfo.wanderSpeed; //set the speed to wander
                    owner.NavComponent.SetDestination(owner.target); //move to the destination
                    Debug.Log("wandering");
                    return NodeState.RUNNING; //return running as not at destination yet
                }
                else //once at stopping distance
                {
                    owner.NavComponent.isStopped = true; //stop moving
                    Debug.Log("dest made");
                    return NodeState.SUCCESS; //return succes and move on with sequence
                }
            }

            owner.NavComponent.isStopped = true; //stop moving
            Debug.Log("wander fail"); //bail out of the sequence 
            return NodeState.FAILURE;
        }


    }

}
