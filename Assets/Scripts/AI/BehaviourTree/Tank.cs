using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BT //within the bahaviour tree namespace
{
    public enum TankState //used to determine what action the tank is currently preforming
    {
        ATTACK,
        WANDER,
        RETREAT
    }

    public class Tank : MonoBehaviour //this is the specific class attached the tank, acts as a connection between the shared info and each tank, calls the bahaviour trees update
    {
        //static vars
        [HideInInspector]
        public static List<Tank> tankList = new List<Tank>(); //if multiple tanks
        [HideInInspector]
        public SharedInfo sharedInfo; //connection to the shared infomation 

        [HideInInspector]
        public bool covered = false; //public bool used by multiple nodes to set when the tank is in cover
        [HideInInspector]
        public Vector3 target; //public target, is used by multiple actions to move to as well as conditons where they set it to different posisitons
        [HideInInspector]
        public bool hasAttacked = false; //public bool which is used to determine if the tank should retreat or attack, stops it continuously retreating
        [HideInInspector]
        public bool alert = false; //used to trigger the UI of the alert sign (!) above the tanks head when enemy is detected

        private TankLogic bahaviourTree; //connection to the actual bahaviour tree

        public NavMeshAgent NavComponent { get; private set; } //getter / setter for the nav mesh agent on the tank
        public Renderer RenderComponent { get; private set; } //gettter / setter for the rendering component on this tank
        private void Start()
        {
            tankList.Add(this); //adds instance of this class to the list of tanks

            sharedInfo = this.GetComponent<SharedInfo>(); //gets the connection to shared info
            NavComponent = gameObject.GetComponent<NavMeshAgent>(); //gets the nav mesh agent
            bahaviourTree = new TankLogic(this); //gets the connection to Tank logic 
            RenderComponent = GetComponent<Renderer>(); //gets the renderer
        }

        private void Update()
        {
            bahaviourTree.Update(); //runs the update function within tank logic class

        }

    }
}

