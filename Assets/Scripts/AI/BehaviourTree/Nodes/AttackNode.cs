using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class AttackNode : Node //deals with the action of firing 
    {
        SharedInfo info;
        private Transform target;
        private float distance;

        public AttackNode(Tank owner) : base(owner)
        {
            info = owner.sharedInfo;
            target = info.statePos;
        }

        public override NodeState Execute()
        {
            distance = Vector3.Distance(target.position, owner.transform.position); //calculate the distance to target
            if (distance < info.attackRange) //if the target is within attack range
            {
                owner.NavComponent.isStopped = true; //stop moving
                if (!info.healthState.m_Dead) //if the target is not dead
                {
                    if (info.shootBT.m_Fired == false) //if this tank hasnt fire too recently
                    {
                        owner.transform.LookAt(target); //face the target
                        info.shootBT.Fire(); //run the fire function from tank shooting
                        owner.hasAttacked = true; //set that the tank has just attacked
                        Debug.Log("fired"); 
                        return NodeState.SUCCESS; //continue with sequence
                    }
                }

                return NodeState.RUNNING; //if still doing checks send running
            }
            Debug.Log("failed fire");
            return NodeState.FAILURE; //if complete the checks yet didtn fire return fail
        }

    }

}
