using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class InCover : Node //very simple condition which checks whether or not the tank is in cover
    {
        Tank info;
        public InCover(Tank owner) : base(owner)
        {
            info = owner;
        }

        public override NodeState Execute()
        {
            if (info.covered == true) //if it is in cover bail out of the sequence
            {
                return NodeState.FAILURE;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }
    }
}


