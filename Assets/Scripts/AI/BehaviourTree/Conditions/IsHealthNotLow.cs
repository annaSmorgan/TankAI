using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class IsHealthNotLow : Node //simple conditon which checks if the health is at a good value
    {
        SharedInfo info;
        public IsHealthNotLow(Tank owner) : base(owner)
        {
            info = owner.GetComponent<SharedInfo>();
        }

        public override NodeState Execute() //if health is above 40 continue on with sequence
        {
            if (info.healthBT.m_CurrentHealth > 40.0f)
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


