using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility  //within the Utility (now state) namespace
{
    public class AttackState //the state of attacking, checks if enemy in view then chases then fires
    {
        DetectionNode detectNode; //connects to the detection node
        PersueNode persueNode; //connections to the persue node
        AttackNode fireNode; //connections to attack node
        public void Attack(Tank owner)
        {
            detectNode = new DetectionNode();
            persueNode = new PersueNode();
            fireNode = new AttackNode();

            if (detectNode.DetectTarget(owner) == true) //if it can see enemy
            {
                owner.NavComponent.speed = owner.sharedInfo.tankSpeed; //set to standard / chasing speed
                persueNode.Chase(owner, owner.sharedInfo.BTPos); //run persue with given target
                fireNode.Fire(owner); //run attack node
            }
            owner.currentState = TankState.WANDER; //move onto the wander state if cant see enemy
        }
    }
}
