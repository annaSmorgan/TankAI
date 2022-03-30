using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    //this is the same as the detection node (see detection node for full comments)
    //only difference if this is used during the wander sequence so if the target is detected failure is returned so it ends the sequence 
    public class IfCantSeeEnemy : Node
    {
        private bool detected;
        LayerMask objectLayer = LayerMask.GetMask("Object");
        LayerMask tankLayer = LayerMask.GetMask("States");

        public IfCantSeeEnemy(Tank owner) : base(owner)
        {
        }

        public override NodeState Execute()
        {
            //uses the same as detection node to look through colliders with the state tag
            Collider[] targetsInVR = Physics.OverlapSphere(owner.transform.position, owner.sharedInfo.detectionRadius, tankLayer);

            for (int i = 0; i < targetsInVR.Length; i++)
            {
                Transform target = targetsInVR[i].transform;
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized;
                if (Vector3.Angle(owner.transform.forward, dirToTarget) < owner.sharedInfo.detectionAngle)
                {
                    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer)) //if there are no objects in the way
                    {
                        detected = true;
                        owner.alert = true;
                    }
                }
            }
            if (detected) //if seen return failure so bails out of the sequence
            {
                Debug.Log("seen");
                detected = false;
                return NodeState.FAILURE;
            }
            else //if not detetected return success so the sequence can continue
            {
                Debug.Log("not seen");
                return NodeState.SUCCESS;
            }

        }
    }
}


