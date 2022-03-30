using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BT //within the bahaviour tree namespace
{
    public class RandomPosNode : Node //creates a random positon on the nav mesh within the given wander distance then sets the target to it
    {
        public RandomPosNode(Tank owner) : base(owner)
        {

        }

        public override NodeState Execute()
        {
            Vector3 randomDirection = Vector3.zero + UnityEngine.Random.insideUnitSphere * owner.sharedInfo.wanderDistance; //creating a sphere from centre of map the size of wander area, 
            //then picking a point in it

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, owner.sharedInfo.wanderDistance, -1); //finds a point on the nav mesh using the random point from the sphere 

            if (hit.position != Vector3.zero) //as long as its not null (0,0,0)
            {
                owner.target = hit.position; //set the target to the found position
                return NodeState.SUCCESS; //return success to move on with sequence
            }
            else
            {
                return NodeState.FAILURE;
            }


        }
    }
}


