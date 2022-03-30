using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public abstract class BaseComp : Node //used to store / create needed info that is used by both selector and sequencer
    {
        public List<Node> nodes { get; private set; } //list of the nodes, will be added to within tank logic, adding the action nodes to be used by selectors and sequences
        protected int currentNodeIndex = 0; //current node

        protected BaseComp(Tank owner) : base(owner)
        {
            currentNodeIndex = 0; //sets current node to 0;
            nodes = new List<Node>(); //sets the nodes list to be a new list (set up)
        }

        public void AddNode(Node newNode) //function that can be called to easily add nodes to the list
        {
            nodes.Add(newNode);
        }

        protected void ResetIndex() //function that can be called to reset the current node
        {
            currentNodeIndex = 0;
        }
    }
}

