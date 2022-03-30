using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class CoverNode : Node //preforms the action of getting the tank to the nearest cover point which is away from the enemy
    {
        private float closestDist; //the closest distance to cover spots
        private float currentDist; //current distance to cover
        private float furtherestDist; //within cover spot picked which point is furthest
        private float currentChild; //current child
        private float distance;
        private Transform target;
       // private float targetDist;
        public CoverNode(Tank owner) : base(owner)
        {

        }

        public override NodeState Execute()
        {
            target = FindFurthestChild(); //find the furthest point from the closest cover spot

            if (target != null) //if there is a target
            {
                //targetDist = Vector3.Distance(owner.sharedInfo.statePos.position, owner.transform.position);  thought this was a needed check
                //if(targetDist < owner.sharedInfo.detectionRadius)
                //{
                distance = Vector3.Distance(target.position, owner.transform.position); //calculate the distance

                if (distance > owner.sharedInfo.stoppingDistance) //if the target is bigger than the stopping distance
                {
                    owner.NavComponent.isStopped = false; //is moving
                    owner.covered = false; //set cover check to false
                    owner.NavComponent.speed = owner.sharedInfo.retreatSpeed; //set the speed to the retreat speed
                    owner.NavComponent.SetDestination(target.position); //move to the destination
                    Debug.Log("hiding");
                    return NodeState.RUNNING; //currently still running
                }
                else //if within stopping distance
                {
                    owner.NavComponent.isStopped = true; //stop moving
                    Debug.Log("hidden");
                    owner.covered = true; //set the cover check to true as the tank is now in cover
                    return NodeState.SUCCESS; //return success now that the tank has reached its cover spot
                }
                //}

            }

            owner.NavComponent.isStopped = true; //stop moving so that the tank can move onto the next sequence or node as it has failed to hide
            Debug.Log("hide fail");
            return NodeState.FAILURE;
        }

        private int FindClosestCover() //loops through all the cover spot locations/groups checking against the current one which one is the closest to this tank
        {
            closestDist = Vector3.Distance(owner.sharedInfo.coverSpots[0].transform.position, owner.transform.position);
            int targetCover = 0;

            for (int i = 0; i < owner.sharedInfo.coverSpots.Length; i++)
            {
                currentDist = Vector3.Distance(owner.sharedInfo.coverSpots[i].transform.position, owner.transform.position);
                if (currentDist < closestDist)
                {
                    closestDist = currentDist;
                    targetCover = i;
                }
            }
            return targetCover; //returns which cover spot location is the closest
        }

        //this function helps to make sure the tank will always go to a point which is the oppesite side of the object to that then target is on
        private Transform FindFurthestChild() //goes throught the closest cover spot location's children, the specific cover points and hides which is furthest from the target
        {
            //uses the closest cover spot from previous function to hunt through its chidlren
            furtherestDist = Vector3.Distance(owner.sharedInfo.coverSpots[FindClosestCover()].transform.GetChild(0).position, owner.sharedInfo.statePos.position);
            int targetChild = 0;

            for (int i = 0; i < owner.sharedInfo.coverSpots[FindClosestCover()].transform.childCount; i++)
            {
                currentChild = Vector3.Distance(owner.sharedInfo.coverSpots[FindClosestCover()].transform.GetChild(i).position, owner.sharedInfo.statePos.position);
                if (currentChild > furtherestDist)
                {
                    furtherestDist = currentChild;
                    targetChild = i;
                }
            }
            return owner.sharedInfo.coverSpots[FindClosestCover()].transform.GetChild(targetChild).transform;
        }
    }

}
