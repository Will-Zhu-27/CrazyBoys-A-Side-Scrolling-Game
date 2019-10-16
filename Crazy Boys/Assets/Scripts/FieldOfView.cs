using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius = 10;
    [Range(0f,360f)]
    public float viewAngle = 360;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public Transform targetPlayer;
    [HideInInspector]
    public Vector3 enemyEyesPosition;
    [HideInInspector]
    public Vector3 playerHeadPosition;
    [HideInInspector]
    public float charactoerHeight = 1.7f;

    bool IsPlayerVisibale()
    {
        enemyEyesPosition = transform.position;
        enemyEyesPosition.y += charactoerHeight;

        Collider[] playerInViewRadius = Physics.OverlapSphere(enemyEyesPosition, viewRadius, playerMask);

        for (int i = 0; i < playerInViewRadius.Length; ++i)
        {
            Transform player = playerInViewRadius[i].transform;
            playerHeadPosition = player.transform.position;
            playerHeadPosition.y += charactoerHeight;

            Vector3 dirToPlayer = (playerHeadPosition - enemyEyesPosition).normalized;

            
            float dstToPlayer = Vector3.Distance(enemyEyesPosition, playerHeadPosition);

            if (!Physics.Raycast(enemyEyesPosition, dirToPlayer, dstToPlayer, obstacleMask))
            {
                targetPlayer = playerInViewRadius[i].transform;
                return true;
            }
            
        }

        targetPlayer = null;
        return false;
    }

    IEnumerator FindPlayerWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (IsPlayerVisibale())
            {
                Debug.Log("PLAYER IS IN THE VIEW!!!");
            }
            else
            {
                // Debug.Log("~~~~~~~~");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindPlayerWithDelay", .2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
