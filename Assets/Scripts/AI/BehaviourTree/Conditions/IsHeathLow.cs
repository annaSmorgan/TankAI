using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class IsHeathLow : Node //simple condition which checks if the ehalth is too low
    {
        SharedInfo info;
        public IsHeathLow(Tank owner) : base(owner)
        {
            info = owner.GetComponent<SharedInfo>();
        }

        public override NodeState Execute() 
        {
            if (info.healthBT.m_CurrentHealth < 55.0f) //if health is low return sucess to continue with sequence 
            {
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}

