using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility //within the Utility (now state) namespace
{

    public class AttackNode //deals with the action of firing 
    {
        private float distance;

        public void Fire(Tank owner)
        {
            Transform target = owner.sharedInfo.BTPos; //sets the target to the enemy tank
            distance = Vector3.Distance(target.position, owner.transform.position); //calculate the distance to target

            if (distance < owner.sharedInfo.attackRange) //if distance is within attack range
            {
                owner.NavComponent.isStopped = true; //stop moving
                if (!owner.sharedInfo.healthBT.m_Dead) //if the target is not dead
                {
                    if (owner.sharedInfo.shootState.m_Fired == false)//if this tank hasnt fire too recently
                    {
                        owner.transform.LookAt(target); //face the target
                        owner.sharedInfo.shootState.Fire(); //run the fire function from tank shooting
                        owner.hasAttacked = true; //set that the tank has just attacked
                        Debug.Log("fired");
                    }
                }
            }
        }

    }

}
