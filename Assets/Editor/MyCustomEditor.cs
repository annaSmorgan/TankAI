using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (SharedInfo))] //custom editor using shared info
public class MyCustomEditor : Editor
{
    private void OnSceneGUI()
    {
        SharedInfo info = (SharedInfo)target; //connections to shared info

        //detect
        //creates a yellow circle
        Handles.color = Color.yellow;
        Handles.DrawWireArc(info.transform.position, Vector3.up, Vector3.forward, 360, info.detectionRadius);

        //uses function from sahred info to create 2 lines within detection circle which is the vision cone
        Vector3 viewAngleA = info.DirFromAngle(-info.detectionAngle / 2, false);
        Vector3 viewAngleB = info.DirFromAngle(info.detectionAngle / 2, false);
        Handles.DrawLine(info.transform.position, info.transform.position + viewAngleA * info.detectionRadius);
        Handles.DrawLine(info.transform.position, info.transform.position + viewAngleB * info.detectionRadius);

        //backwards noise not longer used
        Vector3 noiseBAngleA = info.DirFromAngle(-info.noiseAngle1 / 2, false);
        Vector3 noiseBAngleB = info.DirFromAngle(info.noiseAngle1 / 2, false);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseBAngleA * info.detectionRadius);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseBAngleB * info.detectionRadius);

        //right noise no longer used
        Vector3 noiseLAngleA = info.DirFromAngle(info.noiseAngle1 / 2, false);
        Vector3 noiseLAngleB = info.DirFromAngle(info.noiseAngle2 / 2, false);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseLAngleA * info.detectionRadius);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseLAngleB * info.detectionRadius);

        //left noise no longer used
        Vector3 noiseRAngleA = info.DirFromAngle(-info.noiseAngle1 / 2, false);
        Vector3 noiseRAngleB = info.DirFromAngle(-info.noiseAngle2 / 2, false);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseRAngleA * info.detectionRadius);
        Handles.color = Color.magenta;
        Handles.DrawLine(info.transform.position, info.transform.position + noiseRAngleB * info.detectionRadius);

        //attack - creates a red circle
        Handles.color = Color.red;
        Handles.DrawWireArc(info.transform.position, Vector3.up, Vector3.forward, 360, info.attackRange);

        //retreat - creates a green circle
        Handles.color = Color.green;
        Handles.DrawWireArc(info.transform.position, Vector3.up, Vector3.forward, 360, info.stopRetreatRange);

        //wander - creates a white circle
        Handles.color = Color.white;
        Handles.DrawWireArc(Vector3.zero, Vector3.up, Vector3.forward, 360, info.wanderDistance);
    }

    public override void OnInspectorGUI() //used to create the custom inspector within shared info
    {
        //these are all styles used for the text within the inspector
        //they set the font size, whether it wraps and the font style (bold, underlined wtc)
        GUIStyle attackStyle = new GUIStyle
        {
            fontSize = 12,
            wordWrap = true
        };
        GUIStyle attackTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };


        GUIStyle retreatStyle = new GUIStyle
        {
            fontSize = 12,
            wordWrap = true
        };
        GUIStyle retreatTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };


        GUIStyle detectionStyle = new GUIStyle
        {
            fontSize = 12,
            wordWrap = true
        };
        GUIStyle detectTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };


        GUIStyle wanderStyle = new GUIStyle
        {
            fontSize = 12,
            wordWrap = true
        };
        GUIStyle wanderTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };


        GUIStyle normTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        GUIStyle normStyle = new GUIStyle
        {
            fontSize = 12,
            wordWrap = true
        };

        //setting font colours
        attackStyle.normal.textColor = Color.red;
        attackTitle.normal.textColor = Color.red;
        retreatStyle.normal.textColor = Color.green;
        retreatTitle.normal.textColor = Color.green;
        detectionStyle.normal.textColor = Color.yellow;
        detectTitle.normal.textColor = Color.yellow;
        wanderStyle.normal.textColor = Color.white;
        wanderTitle.normal.textColor = Color.white;
        normTitle.normal.textColor = Color.black;
        normStyle.normal.textColor = Color.black;

        //using labels to have custom text which describes each input section and how it will effect the ai
        EditorGUI.LabelField((new Rect(20, 30, 400, 10)), "Attack Settings", attackTitle);
        EditorGUI.LabelField((new Rect(20, 50, 400, 10)), "Attack range is the range in which the tank begins its attack, it also stops at this distance.", attackStyle);

        EditorGUI.LabelField((new Rect(20, 105, 400, 10)), "Detection Settings", detectTitle);
        EditorGUI.LabelField((new Rect(20, 125, 400, 10)), "Detection range is how far the tanks can hear as well as how far the FOV goes, the FOV is then calculated with the detection angle.", detectionStyle);

        EditorGUI.LabelField((new Rect(20, 215, 400, 10)), "Wander Settings", wanderTitle);
        EditorGUI.LabelField((new Rect(20, 235, 400, 10)), "Wander range is the area in which the tanks can wander within, the stopping distance is how close it needs to be to the target so it stops moving.", wanderStyle);

        EditorGUI.LabelField((new Rect(20, 340, 400, 10)), "Retreat Settings", retreatTitle);
        EditorGUI.LabelField((new Rect(20, 360, 400, 10)), "Retreat Stopping range is the range, which after it leaves it it will stop retreating, it also holds the 4 corners which are the retreating points as well as all the possible cover spots.", retreatStyle);

        EditorGUI.LabelField((new Rect(20, 495, 400, 10)), "Tank Details", normTitle);
        EditorGUI.LabelField((new Rect(20, 520, 400, 10)), "The game objects of the tanks in play as well as the normal speed", normStyle);

        EditorGUI.LabelField((new Rect(20, 610, 400, 10)), "Text Field", normTitle);
        EditorGUI.LabelField((new Rect(20, 630, 400, 10)), "Where the winner is shown", normStyle);

        base.OnInspectorGUI(); //this will then continue to use the normal inspector stuff

        
    }
}
