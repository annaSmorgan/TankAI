using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Utility  //within the Utility (now state) namespace
{
    public enum TankState //the states in which the tank can be in
    {
        ATTACK,
        WANDER,
        RETREAT,
        SLEEP
    }

    public class Tank : MonoBehaviour //this is the specific class attached the tank, acts as a connection between the shared info and each tank, calls the state machine
    {
        //static vars
        [HideInInspector]
        public static List<Tank> tankList = new List<Tank>(); //if multiple tanks
        [HideInInspector]
        public SharedInfo sharedInfo; //connection to the shared infomation 

        [HideInInspector]
        public bool covered = false; //public bool used by multiple nodes to set when the tank is in cover
        [HideInInspector]
        public float timer; //public int used as a timer which is triggered while wandering
        [HideInInspector]
        public bool alert = false; //used to trigger the UI of the alert sign (!) above the tanks head when enemy is detected
        [HideInInspector]
        public Vector3 target;  //public target, is used by multiple actions to move to as well as conditons where they set it to different posisitons
        [HideInInspector]
        public bool hasAttacked = false; //public bool which is used to determine if the tank should retreat or attack, stops it continuously retreating
        [HideInInspector]
        public Vector3 wanderTarget; //public vec3 of the target for the tank to wander too
        [HideInInspector]
        public TankState currentState = TankState.WANDER; //current tank state

        //states
        private AttackState attackState; //connection to attack state
        private RetreatState retreatState; //connection to retreat state
        private WanderState wanderState; //connection to wander state
        private SleepState sleepState; //connection to sleep state

        public NavMeshAgent NavComponent { get; private set; }//getter / setter for the nav mesh agent on the tank
        public Renderer RenderComponent { get; private set; }//gettter / setter for the rendering component on this tank
        private void Start()
        {
            tankList.Add(this); //adds instance of this class to the list of tanks

            sharedInfo = this.GetComponent<SharedInfo>(); //gets the connection to shared info
            NavComponent = gameObject.GetComponent<NavMeshAgent>(); //gets the nav mesh agent
            RenderComponent = GetComponent<Renderer>(); //gets the renderer

            //states connections
            attackState = new AttackState();
            retreatState = new RetreatState();
            wanderState = new WanderState();
            sleepState = new SleepState();

        }

        private void Update()
        {
            switch (currentState) //switches on the current tank state
            {
                case TankState.RETREAT: //if the current state is set to retreat
                {
                        retreatState.Retreat(this); //run retreat state class
                        break;
                }
                case TankState.ATTACK: //if the current state is set to attack
                    {
                        attackState.Attack(this);//run attack state class
                        break;
                    }
                case TankState.WANDER: //if the current state is set to wander
                    {
                        wanderState.Wander(this);//run wander state class
                        break;
                }
                case TankState.SLEEP: //if the current state is set to sleep
                    {
                        sleepState.Rest(this); //run sleep state class
                        break;
                }
            }

        }
    }
}

