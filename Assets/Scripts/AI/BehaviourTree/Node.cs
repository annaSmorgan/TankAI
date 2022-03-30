using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public enum NodeState //states in which the nodes can be in, moves on the sequences and selectors
    {
        RUNNING,
        SUCCESS,
        FAILURE,
        NONE
    }

    public abstract class Node //abstract class inherited by all then action nodes
    {

        public Tank owner { get; private set; } //tank class getter / setter

        public Node(Tank owner) //sets connection to tank class
        {
            this.owner = owner;
        }

        public virtual NodeState Execute() //acts as a update style function, is used by nodes and forces them to return a node state
        {
            return NodeState.NONE;
        }
    }


}
