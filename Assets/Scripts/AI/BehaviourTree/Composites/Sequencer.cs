using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT //within the bahaviour tree namespace
{
    public class Sequencer : BaseComp //inherits from base comp, will go through the node list and can only suceed if all the nodes do too
    {
        public Sequencer(Tank owner) : base(owner)
        {

        }
        public override NodeState Execute()
        {
            NodeState _nodeState = NodeState.FAILURE;//as a base state set to failure
            Node currentNode = nodes[currentNodeIndex];//current node is set using the current node index and the node list

            if (currentNode != null)  //if its not null
            {
                NodeState currentNodeState = currentNode.Execute();  //run the execute function for the node to get its state

                if (currentNodeState == NodeState.SUCCESS) //if success
                {
                    if (currentNodeIndex == nodes.Count - 1) //if out of nodes
                    {
                        _nodeState = NodeState.SUCCESS; //sequence succeed as out of nodes and they all succeed
                        Debug.Log("seq success");
                    }
                    else //if more nodes
                    {
                        ++currentNodeIndex;//increase index to the next node in list
                        _nodeState = NodeState.RUNNING;//running as still nodes to run
                        Debug.Log("seq running");
                    }
                }
                else //if it fails or running
                {
                    _nodeState = currentNodeState;//current node state is still the node state
                }
            }

            if (_nodeState == NodeState.SUCCESS || _nodeState == NodeState.FAILURE)//if after all the nodes it succeeded or failed
            {
                ResetIndex();//reset the current node index
            }

            return _nodeState; //return the state of the sequence
        }
    }
}

