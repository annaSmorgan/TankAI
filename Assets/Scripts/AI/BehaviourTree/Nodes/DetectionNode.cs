using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class DetectionNode : Node //checks if the target is within its vision by checking the range and angle
    {
        private bool detected = false;
        private LayerMask objectLayer = LayerMask.GetMask("Object");
        private LayerMask tankLayer = LayerMask.GetMask("States");
        public DetectionNode(Tank owner) : base(owner)
        {
        }

        public override NodeState Execute()
        {
            //looks though all the colliders within the detection radius with the tag of the enemy 
            Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, owner.sharedInfo.detectionRadius, tankLayer);

            for (int i = 0; i < targetsInVR.Length; i++) //loop thrpugh the colliders
            {
                Transform target = targetsInVR[i].transform; //set the target to the transform of the detected collider
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized; //calculate the direction to the target
                if (Vector3.Angle(owner.transform.forward, dirToTarget) < owner.sharedInfo.detectionAngle) //check angle bewteen the forward and the target is within the visinon cone angle
                {
                    float dstToTarget = Vector3.Distance(target.position, owner.transform.position); //calculate the distance
                    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer)) //if there isnt a object in the way
                    {
                        detected = true; //set to true
                        owner.alert = true; //show the alert sign
                    }

                }
            }
            if (detected) //if it was seen return success to continue with the sequence 
            {
                Debug.Log("Detected");
                detected = false; // reset the detected bool
                return NodeState.SUCCESS;
            }
            else //if its not seen then bail out of the current sequence
            {
                Debug.Log("detect fail");
                return NodeState.FAILURE;
            }

        }
    }

}
