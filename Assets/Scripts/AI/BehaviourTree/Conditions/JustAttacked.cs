using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class JustAttacked : Node //checks if the tank has recently attacked
    {
        public JustAttacked(Tank owner) : base(owner)
        {
        }

        public override NodeState Execute()
        {
            if (owner.hasAttacked == true) //if it has attacked recently return fail so it bails out of the sequence
            {
                owner.hasAttacked = false;
                return NodeState.FAILURE;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }
    }

}

