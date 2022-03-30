using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class Selector : BaseComp //inherits from base comp, will go through node list, if the node state is failure it will move to next node
    {
        public Selector(Tank owner) : base(owner)
        {

        }
        public override NodeState Execute()
        {
            NodeState _nodeState = NodeState.FAILURE; //as a base state set to failure
            Node currentNode = nodes[currentNodeIndex]; //current node is set using the current node index and the node list

            if (currentNode != null) //if its not null
            {
                NodeState currentNodeState = currentNode.Execute(); //run the execute function for the node to get its state

                if (currentNodeState == NodeState.FAILURE) //if it fails
                {
                    if (currentNodeIndex == nodes.Count - 1) //if out of nodes
                    {
                        _nodeState = NodeState.FAILURE; //fail out of the selector
                        Debug.Log("sel fail");
                    }
                    else //if more nodes
                    {
                        ++currentNodeIndex; //increase index to the next node in list
                        _nodeState = NodeState.RUNNING; //running as still nodes to run
                        Debug.Log("sel running");
                    }
                }
                else //if it succeeds/running
                {
                    _nodeState = currentNodeState; //current node state is still the node state
                }
            }

            if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE) //if after all the nodes it succeed or failed
            {
                ResetIndex(); //reset the current node index
            }

            return _nodeState; //return the state of the selector
        }
    }
}


