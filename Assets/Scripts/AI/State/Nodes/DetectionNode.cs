using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility  //within the Utility (now state) namespace
{
    public class DetectionNode //checks if the target is within its vision by checking the range and angle
    {
        private LayerMask objectLayer = LayerMask.GetMask("Object");
        private LayerMask tankLayer = LayerMask.GetMask("BT");
        private bool detected = false;

        public bool DetectTarget(Tank owner) //if a bool as it is used as a check within the state machine
        {
            //looks though all the colliders within the detection radius with the tag of the enemy 
            Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, owner.sharedInfo.detectionRadius, tankLayer);

            for (int i = 0; i < targetsInVR.Length; i++) //loop thrpugh the colliders
            {
                Transform target = targetsInVR[i].transform;//set the target to the transform of the detected collider
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized;//calculate the direction to the target
                if (Vector3.Angle(owner.transform.forward, dirToTarget) < owner.sharedInfo.detectionAngle)//check angle bewteen the forward and the target is within the visinon cone angle
                {
                    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer))
                    {
                        detected = true;//set to true
                        owner.alert = true; //show the alert sign
                    }

                }
            }
            if (detected) //if seen target
            {
                Debug.Log("Detected");
                detected = false; //reset bool
                return true; //return true
            }
            else
            {
                Debug.Log("detect fail");
                return false; //return false
            }

        }
    }

}
