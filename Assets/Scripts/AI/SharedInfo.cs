using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SharedInfo : MonoBehaviour //holds all the sahred info between tanks, also handles game manager tasks as i removed the given one
{
    [HideInInspector]
    public Complete.TankHealth healthBT; //connection to tank health script
    [HideInInspector]
    public Complete.TankHealth healthState; //connection to tank health script
    [HideInInspector]
    public Complete.TankShooting shootBT;//connection to tank shooting script
    [HideInInspector]
    public Complete.TankShooting shootState;//connection to tank shooting script
    [HideInInspector]
    public Transform BTPos; //creates an empty transform
    [HideInInspector]
    public Transform statePos;  //creates an empty transform
    [HideInInspector]
    public float noiseAngle1 = 270.0f; //was used for hearing but was remove (see HearExplosiveNode) but is used in custom editor still
    [HideInInspector]
    public float noiseAngle2 = 90.0f; //was used for hearing but was remove (see HearExplosiveNode) but is used in custom editor still

    //these are all the public variables used within many classes, theyre all editble within the inspector
    //there are explanations for these within the inspector
    [Space(60)]
    [Range(5, 15)]
    public float attackRange = 0.0f;

    [Space(55)]
    public float detectionRadius = 0.0f; 
    [Range(0,360)]
    public float detectionAngle = 0.0f;

    [Space(80)]
    [Range(20, 60)]
    public float wanderDistance = 0.0f; 
    public float stoppingDistance = 0.0f;
    public float wanderSpeed = 3.0f;

    [Space(75)]
    [Range(15, 40)]
    public float stopRetreatRange = 0.0f;
    public float retreatSpeed = 18.0f;
    public Transform[] fourCorners;
    public GameObject[] coverSpots;

    [Space(50)]
    public GameObject[] tanks;
    public GameObject[] alerts;
    public float tankSpeed = 5.0f;

    [Space(50)]
    public Text gameText;

    private void Start() 
    {
        SpawnRandomPos(); //places both tanks at a random point on the map

        healthBT = tanks[0].GetComponent<Complete.TankHealth>(); //connections to the bahaviour tree's tank health script
        healthState = tanks[1].GetComponent<Complete.TankHealth>(); //connections to the state machine's tank health script

        shootBT = tanks[0].GetComponent<Complete.TankShooting>(); //connections to the bahaviour tree's tank shooting script
        shootState = tanks[1].GetComponent<Complete.TankShooting>();  //connections to the state machine's tank shoooting script

        BTPos = tanks[0].transform; //sets bahaviour tree tank's position
        statePos = tanks[1].transform;//sets state machine tank's position

        StartCoroutine(RoundStart()); //runs ienumarator which displays fight text
    }

    private void Update()
    {
        BTPos = tanks[0].transform; //updates bahaviour tree tank's position
        statePos = tanks[1].transform;//updates state machine tank's position

        if (FindWinner() == true) //if a winner is found
        {
            Winner(); //display winner text
            StartCoroutine(Restart()); //reset the level
        }

        if(tanks[0].GetComponent<BT.Tank>().alert == true) //if bahaviour tree tank is alert
        {
            StartCoroutine(ShowAlert(alerts[0])); //run ienumrator with the bahaviour tree tank's alert sign (!)
            tanks[0].GetComponent<BT.Tank>().alert = false; //sets alert back to false
        }

        if (tanks[1].GetComponent<Utility.Tank>().alert == true) //if state machine tank is alert
        {
            StartCoroutine(ShowAlert(alerts[1])); //run ienumrator with the state machine tank's alert sign (!)
            tanks[1].GetComponent<Utility.Tank>().alert = false; //set alert back to false
        }

        if (Input.GetKey("escape")) //on esc quit application
        {
            Application.Quit();
        }
    }

    private void SpawnRandomPos() //gets two random points within a sphere created within wander distance then sets both tanks position to them
    {
        Vector3 randomPoint = Vector3.zero + UnityEngine.Random.insideUnitSphere * wanderDistance;
        Vector3 randomPoint2 = Vector3.zero + UnityEngine.Random.insideUnitSphere * wanderDistance;

        tanks[0].transform.position = randomPoint;
        tanks[1].transform.position = randomPoint2;
    }

    private void Winner() //checks what tank is left active then display text
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if(tanks[i].activeSelf)
            {
                gameText.text = "The Winner is Tank " + (i + 1);
            }
        }
    }
    private bool FindWinner() //loop through the tanks checking if their active, if there is only 1 left then there is a winner
    {
        int tankCount = 0;
        for(int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].activeSelf == false)
            {
                tankCount++;
            }
        }
        
        if(tankCount == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator ShowAlert(GameObject alert) //displays the given alert sign for only a second
    {
        alert.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        alert.SetActive(false);
    }

    private IEnumerator Restart() //resets the level by reloading the scene, has a 3 second delay 
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }

    private IEnumerator RoundStart() //displays the starting text 
    {
        gameText.text = "Fight !";
        gameText.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        yield return new WaitForSeconds(1.5f);
        gameText.text = null;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) //calculates the direction from the given angle, if its not global it will convert it 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
