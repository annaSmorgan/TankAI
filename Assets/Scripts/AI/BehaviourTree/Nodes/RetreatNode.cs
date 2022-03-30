using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class RetreatNode : Node //uses the furthest postion to run away from the enemy tank until it is out of range
    {
        private float distance;
        public RetreatNode(Tank a_owner) : base(a_owner)
        {

        }

        public override NodeState Execute()
        {
            owner.NavComponent.isStopped = false; //is moving

            distance = Vector3.Distance(owner.sharedInfo.statePos.position, owner.transform.position); //calculate the distance from enemy 
            if (distance < owner.sharedInfo.stopRetreatRange)//if within retreating radius
            {
                owner.NavComponent.speed = owner.sharedInfo.retreatSpeed; //change the speed to reatreat speed
                owner.NavComponent.SetDestination(owner.target); //set the destination to the nearest corner
                owner.covered = false; //not in cover
                owner.hasAttacked = true; //hasnt attacked recently, used so if retreating again it will attack before retreating 
                Debug.Log("retreating");
                return NodeState.RUNNING; //running as not a destination yet

            }
            else if (distance >= owner.sharedInfo.stopRetreatRange) //once the enemy is outside of range stop retreating
            {
                Debug.Log("retreated"); //success so move onto next sequence
                return NodeState.SUCCESS;
            }
            Debug.Log("fail retreat");
            return NodeState.FAILURE;
        }
    }

}

