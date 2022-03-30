using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Utility  //within the Utility (now state) namespace
{
    public class FindingPoints //the combined classes of furthestPos and RandomPos, does all the calulations for finding a target postion on the map,
        //also has the needed functions for finding the cover spots from within the cover node
    {
        private float closestDist;
        private float currentDist;
        private float furtherestDist;
        private float currentChild;

        public void FartherstPoint(Tank owner)
        {
            closestDist = Vector3.Distance(owner.sharedInfo.fourCorners[0].transform.position, owner.transform.position);//as a base set the first corner as closest
            int targetCover = 0;

            for (int i = 0; i < owner.sharedInfo.fourCorners.Length; i++) //loop through all the corners
            {
                currentDist = Vector3.Distance(owner.sharedInfo.fourCorners[i].transform.position, owner.transform.position);//calculate the distance from tank to corner
                if (currentDist < closestDist)//compare previously closest and the current distance to see which is closer
                {
                    closestDist = currentDist;//if it is closer set it 
                    targetCover = i; //the target is set to this corner
                }
            }

            if (owner.sharedInfo.fourCorners[targetCover].position != Vector3.zero) //as long as the targeted corner is not null
            {
                owner.target = owner.sharedInfo.fourCorners[targetCover].position; //set the overall tank target to the closest corner
            }
        }

        public void RandomPos(Tank owner)
        {
            Vector3 randomDirection = Vector3.zero + UnityEngine.Random.insideUnitSphere * owner.sharedInfo.wanderDistance;//creating a sphere from centre of map the size of wander area, 
            //then picking a point in it

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, owner.sharedInfo.wanderDistance, -1);//finds a point on the nav mesh using the random point from the sphere 

            if (hit.position != Vector3.zero)//as long as its not null (0,0,0)
            {
                owner.wanderTarget = hit.position; //set the target to the found position
            }

        }

        private int FindClosestCover(Tank owner)  //loops through all the cover spot locations/groups checking against the current one which one is the closest to this tank
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
            return targetCover;  //returns which cover spot location is the closest
        }

        //this function helps to make sure the tank will always go to a point which is the oppesite side of the object to that then target is on
        public Transform FindFurthestChild(Tank owner)//goes throught the closest cover spot location's children, the specific cover points and hides which is furthest from the target
        {
            //uses the closest cover spot from previous function to hunt through its chidlren
            furtherestDist = Vector3.Distance(owner.sharedInfo.coverSpots[FindClosestCover(owner)].transform.GetChild(0).position, owner.sharedInfo.BTPos.position);
            int targetChild = 0;

            for (int i = 0; i < owner.sharedInfo.coverSpots[FindClosestCover(owner)].transform.childCount; i++)
            {
                currentChild = Vector3.Distance(owner.sharedInfo.coverSpots[FindClosestCover(owner)].transform.GetChild(i).position, owner.sharedInfo.BTPos.position);
                if (currentChild > furtherestDist)
                {
                    furtherestDist = currentChild;
                    targetChild = i;
                }
            }
            return owner.sharedInfo.coverSpots[FindClosestCover(owner)].transform.GetChild(targetChild).transform;
        }
    }
}