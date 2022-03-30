using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility  //within the Utility (now state) namespace
{
    public class RetreatNode //uses the furthest postion to run away from the enemy tank until it is out of range
    {
        private float distance;

        public void Retreat(Tank owner)
        {
            owner.NavComponent.isStopped = false;//is moving

            distance = Vector3.Distance(owner.sharedInfo.BTPos.position, owner.transform.position);//calculate the distance from enemy 
            if (distance < owner.sharedInfo.stopRetreatRange)//if within retreating radius
            {
                owner.NavComponent.speed = owner.sharedInfo.retreatSpeed;//change the speed to reatreat speed
                owner.NavComponent.SetDestination(owner.target);//set the destination to the nearest corner
                owner.covered = false;//not in cover
                owner.hasAttacked = true; //hasnt attacked recently, used so if retreating again it will attack before retreating 
                Debug.Log("retreating");

            }
            else
            {
                owner.NavComponent.isStopped = true; //stop moving
            }

        }
    }

}

