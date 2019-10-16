using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.enemyEyesPosition, Vector3.forward, Vector3.left, 360f, fow.viewRadius);

        if (fow.targetPlayer != null)
        {
            Handles.color = Color.red;


            Handles.DrawLine(fow.enemyEyesPosition, fow.playerHeadPosition);
        }
        
    }
    
}
