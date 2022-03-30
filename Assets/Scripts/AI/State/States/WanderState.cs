using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility //within the Utility (now state) namespace
{

    public class WanderState //when the tank is safe to wander, cant see enemy, if wander if run for long enough tank will go into sleep
    {
        DetectionNode detectNode; //connection to detection node
        FindingPoints ranPos; //connection to finding points 
        MovementNode wanderNode; //connection to movement node

        public void Wander(Tank owner)
        {
            owner.timer = 0.0f; //set the timer to 0
            owner.timer += Time.deltaTime; //run timer

            detectNode = new DetectionNode(); 
            ranPos = new FindingPoints();
            wanderNode = new MovementNode();

            if(owner.timer > 5.0f) //if timer is at 5 seconds
            {
                owner.currentState = TankState.SLEEP; //move onto the sleep state
            }

            if (detectNode.DetectTarget(owner) == false) //if it cant see the enemy
            {
                    owner.NavComponent.speed = owner.sharedInfo.tankSpeed; //set the tank speed to standard / chase speed
                    if (owner.wanderTarget == null) //if the target is null
                    {
                        ranPos.RandomPos(owner); //create a random target on the map
                    }

                    float distance; 
                    distance = Vector3.Distance(owner.transform.position, owner.wanderTarget); //calculate the distance to target

                    if (distance > owner.sharedInfo.stoppingDistance) //if bigger than stopping distance
                    {
                        wanderNode.Wandering(owner); //run wandering node
                    }
                    else if (distance < owner.sharedInfo.stoppingDistance) //if within stopping distance 
                    {
                        ranPos.RandomPos(owner); //create a new random target on map
                    }

            }
            else if (detectNode.DetectTarget(owner) == true) //if enemy is detected
            {
                owner.currentState = TankState.RETREAT; //move onto retreat state
            }
        }
    }
}
