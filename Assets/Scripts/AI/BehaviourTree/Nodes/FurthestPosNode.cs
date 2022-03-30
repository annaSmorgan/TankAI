using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BT //within the bahaviour tree namespace
{
    public class FurthestPosNode : Node
    {
        private float closestDist;
        private float currentDist;
        public FurthestPosNode(Tank owner) : base(owner) //used within retreat and finds the closest of the 4 corners, as way to get a far away point from the attacking enemy 
        { 

        }

        public override NodeState Execute()
        {
            closestDist = Vector3.Distance(owner.sharedInfo.fourCorners[0].transform.position, owner.transform.position); //as a base set the first corner as closest
            int targetCover = 0; 

            for (int i = 0; i < owner.sharedInfo.fourCorners.Length; i++) //loop through all the corners
            {
                currentDist = Vector3.Distance(owner.sharedInfo.fourCorners[i].transform.position, owner.transform.position); //calculate the distance from tank to corner
                if (currentDist < closestDist) //compare previously closest and the current distance to see which is closer
                {
                    closestDist = currentDist; //if it is closer set it 
                    targetCover = i; //the target is set to this corner
                }
            }

            if (owner.sharedInfo.fourCorners[targetCover].position != Vector3.zero) //as long as the targeted corner is not null
            {
                owner.target = owner.sharedInfo.fourCorners[targetCover].position; //set the overall tank target to the closest corner
                return NodeState.SUCCESS; //continue with sequence
            }
            else
            {
                return NodeState.FAILURE;
            }

        }
    }

}

