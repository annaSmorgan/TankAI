using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BT //within the bahaviour tree namespace
{
    public class TankLogic : BahaviourTree //this is the main class for the bahaviour tree, it connects and runs all the nodes together
    {
        //composities
        private Selector rootSelector; //retreat, attack, wander
        private Sequencer attackSequence; //detect, persue, fire
        private Sequencer retreatSequence; //cover, retreat
        private Sequencer wanderSequence;//wander
        private Sequencer wanderCheck; // can see or hear enemy 
        //private Sequencer hideSequence; //retreat, cover, scout was going to involve the tank moving far away, finding a cover spot, then checking around itself, 
        //if detected the enemy it would find anther cover spot. was going to be used as an unlockable section that was enable if a team mate died

        //conditions 
        private IsHeathLow isHealthLow; //checks if the health is low
        private IsHealthNotLow isHealthNotLow; //checks if the health is not low - didnt end up needing
        private InCover inCover; //  checks if within cover
        private IfCantSeeEnemy seeEnemy; //checks if the enemy is not in view
        private JustAttacked attacked; //checks if the tank has recently attacked

        //nodes
        private PersueNode persueNode; //chases the target
        private RetreatNode retreatNode; //flees from the target
        private MovementNode wanderNode; //wanders around map
        private DetectionNode detectNode; //sees if the target is in view
        private HearExplosionNode noiseNode; //sees if the tank can hear the other (was orignally just for the sound of a shell being fired)
        private AttackNode fireNode; //attacks the target
        private CoverNode coverNode; //goes to a set cover spot
        private RandomPosNode ranPos; //creates a random map position
        private FurthestPosNode farPos; //finds the farthest position from enemy
        //private ScoutNode scoutNode;

        public TankLogic(Tank owner) : base(owner)
        {
            //composities 
            rootSelector = new Selector(owner); 
            attackSequence = new Sequencer(owner); 
            retreatSequence = new Sequencer(owner);
            wanderSequence = new Sequencer(owner);
            wanderCheck = new Sequencer(owner);

            //conditions
            isHealthLow = new IsHeathLow(owner);
            isHealthNotLow = new IsHealthNotLow(owner);
            inCover = new InCover(owner);
            seeEnemy = new IfCantSeeEnemy(owner);
            attacked = new JustAttacked(owner);


            //node
            fireNode = new AttackNode(owner);
            persueNode = new PersueNode(owner, owner.sharedInfo.statePos); //also sends through the needed target
            wanderNode = new MovementNode(owner);
            ranPos = new RandomPosNode(owner);
            detectNode = new DetectionNode(owner);
            coverNode = new CoverNode(owner);
            farPos = new FurthestPosNode(owner);
            retreatNode = new RetreatNode(owner);
            noiseNode = new HearExplosionNode(owner);

            //linking nodes
            //root
            Root = rootSelector; //root node is the root selector

            rootSelector.AddNode(retreatSequence); //will retreat if it can
            rootSelector.AddNode(attackSequence); //then if cant retreat it will attack
            rootSelector.AddNode(wanderSequence); //if it cant attack then it will wander

            //retreat
            retreatSequence.AddNode(attacked); //check if it has attacked recently, only continue if it hasnt
            retreatSequence.AddNode(isHealthLow); //check if the health is low, if its not dont retreat
            retreatSequence.AddNode(detectNode); //check if it can see the enemy
            retreatSequence.AddNode(inCover);  //make sure its not already in cover
            retreatSequence.AddNode(coverNode); //preform taking cover
            retreatSequence.AddNode(farPos); //find the farthest point
            retreatSequence.AddNode(retreatNode); //move towards the farthest point until enemy out of range

            //attack
            attackSequence.AddNode(detectNode); //is the enemy in view
            attackSequence.AddNode(persueNode); //chase the enemy 
            attackSequence.AddNode(fireNode); //fire at the enemy

            //wander 
            wanderSequence.AddNode(wanderCheck); //needs to be successful to move on
            wanderSequence.AddNode(ranPos); //finds a random postion on the map
            wanderSequence.AddNode(wanderCheck); //does another check for the enemy
            wanderSequence.AddNode(wanderNode); //moves to the random positon
            wanderSequence.AddNode(wanderCheck); //last check for the enemy

            wanderCheck.AddNode(noiseNode); //can the enemy be heard, if not then continue to wander
            wanderCheck.AddNode(seeEnemy); //can it be seen, if not then wander

            ////hide 
            //hideSequence.AddNode(retreatNode);
            //hideSequence.AddNode(coverNode);
            //hideSequence.AddNode(scoutNode);
        }
    }


}
