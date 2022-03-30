using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility //within the Utility (now state) namespace
{
    public class RetreatState //checks if the conditons are correct for the tank to retreat to then move onto attack state
    {
        DetectionNode detectNode; //connection to detection node
        RetreatNode retreatNode; //connection to retreat node
        CoverNode coverNode; //connection to cover node
        FindingPoints farPos; //connection to finding points

        public void Retreat(Tank owner)
        {
            detectNode = new DetectionNode();
            retreatNode = new RetreatNode();
            coverNode = new CoverNode();
            farPos = new FindingPoints();

            if (owner.hasAttacked == false) //if it has attacked before
            {
                if (owner.sharedInfo.healthState.m_CurrentHealth < 55) //if the health is low
                {
                    if (detectNode.DetectTarget(owner) == true) //if it can see the enemy
                    {
                        if (owner.covered == false) //if not in cover
                        {
                            farPos.FartherstPoint(owner); //find the farthest point from enemy
                            coverNode.Hide(owner, farPos.FindFurthestChild(owner)); //run the cover node with the furthest child of the cover spots (within finding points class)
                            retreatNode.Retreat(owner); //run retreat node
                        }
                    }
                }
            }
            owner.currentState = TankState.ATTACK; //move onto the attack state

        }
    }
}
