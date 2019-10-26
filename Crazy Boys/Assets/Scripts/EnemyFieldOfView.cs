/*
https://www.youtube.com/watch?v=rQG9aUWarwE
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Enemy))]
public class EnemyFieldOfView : MonoBehaviour {
	public Transform viewPoint;
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	[SerializeField] private float responseTime = 0.2f;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public Transform visibleTargets = null;
	private Enemy enemy;

	void Start() {
		enemy = GetComponent<Enemy>();
		StartCoroutine ("FindTargetsWithDelay", responseTime);
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets = null;
		Collider[] targetsInViewRadius = Physics.OverlapSphere (viewPoint.position, viewRadius, targetMask);
		bool isGetTarget = false;

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - viewPoint.position).normalized;
			if (Vector3.Angle (viewPoint.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (viewPoint.position, target.position);

				if (!Physics.Raycast (viewPoint.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets = target;
					isGetTarget = true;
				}
			}
		}

		if (isGetTarget && enemy.autoAttack) {
			enemy.setIsShooting(true);
		} else {
			enemy.setIsShooting(false);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += viewPoint.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}