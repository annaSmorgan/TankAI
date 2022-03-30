using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class HearExplosionNode : Node //used to hear the nearing tank and turn to face it, triggering the detection node, if no object is in the way
    {
        private bool detected = false;
        private Transform target;
        private float timer = 0;
        private LayerMask tankLayer = LayerMask.GetMask("States");
        private LayerMask objectLayer = LayerMask.GetMask("Object");
        //private float angleLeft;
        //private float angleRight;
        //private float angleBack;

        public HearExplosionNode(Tank owner) : base(owner)
        {

        }

        public override NodeState Execute()
        {
            //look through colliders in the attack range for ones under the enemies tag
            Collider[] targetsInNoiseCones = Physics.OverlapSphere(owner.transform.position, owner.sharedInfo.attackRange, tankLayer);

            for (int i = 0; i < targetsInNoiseCones.Length; i++) //loop through the colliders
            {
                timer ++; //start the timer

                target = targetsInNoiseCones[i].transform; //set the target to the detected transform

                float dist = Vector3.Distance(owner.transform.position, target.position); //caculate teh distance
                
                Vector3 dirToTarget = (target.position - owner.transform.position).normalized; //find the direction to the target

                if (!Physics.Raycast(owner.transform.position, dirToTarget, dist, objectLayer)) //if there isnt a object in the way 
                {
                    if (timer >= 2.0f) //if the enemy has been there for long enough that the timer has reached 2
                    {
                        detected = true; //set detected to true
                        owner.alert = true; //show alert sign
                    }
                }

                //attempted to have a varied hearing based on which side the enemy tank was on 
                //but just couldnt get the angles to work

                //AngleCalc();

                ////left
                //if (Vector3.Angle(-owner.transform.right, dirToTarget) < angleLeft)
                //{
                //    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                //    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer))
                //    {
                //        timer += Time.deltaTime;
                //        if(timer > 2.0f)
                //        {
                //            detected = true;
                //            owner.alert = true;
                //            Debug.Log("heard left");
                //        }
                //    }
                //}
                ////right
                //if (Vector3.Angle(owner.transform.right, dirToTarget) < angleRight)
                //{
                //    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                //    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer))
                //    {
                //        timer += Time.deltaTime;
                //        if (timer > 2.0f)
                //        {
                //            detected = true;
                //            owner.alert = true;
                //            Debug.Log("heard right");
                //        }
                //    }
                //}
                ////behind
                //float targetBehind = Vector3.Angle(-owner.transform.forward, dirToTarget);
                //if (targetBehind > -owner.sharedInfo.noiseAngle1 && targetBehind < owner.sharedInfo.noiseAngle1)
                //{
                //    float dstToTarget = Vector3.Distance(target.position, owner.transform.position);
                //    if (!Physics.Raycast(owner.transform.position, dirToTarget, dstToTarget, objectLayer))
                //    {
                //        timer += Time.deltaTime;
                //        if (timer > 1.0f)
                //        {
                //            detected = true;
                //            owner.alert = true;
                //            Debug.Log("heard behind");
                //        }
                //    }
                //}
            }

            if (detected)
            {
                Debug.Log("heard");
                owner.transform.LookAt(target); //turn to the target
                detected = false; //reset detected and timer
                timer = 0;
                return NodeState.FAILURE; //as its used during the wander sequence, return failure so it bails out of that sequence 
            }
            else
            {
                Debug.Log("hearing fail"); //as its in the wander sequence if the hearing failed we want to continue with the sequence
                return NodeState.SUCCESS;
            }

        }

    }

}
