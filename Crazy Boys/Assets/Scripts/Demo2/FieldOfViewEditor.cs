/*
https://www.youtube.com/watch?v=rQG9aUWarwE
 */
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (EnemyFieldOfView))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
		EnemyFieldOfView fow = (EnemyFieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (fow.viewPoint.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);

		Handles.DrawLine (fow.viewPoint.position, fow.viewPoint.position + viewAngleA * fow.viewRadius);
		Handles.DrawLine (fow.viewPoint.position, fow.viewPoint.position + viewAngleB * fow.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets) {
			Handles.DrawLine (fow.viewPoint.position, visibleTarget.position);
		}
	}

}
