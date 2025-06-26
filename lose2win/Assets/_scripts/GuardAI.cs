using UnityEngine;
using System.Collections;

public class GuardAI : MonoBehaviour {
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float visionRange = 10f;
    public float visionAngle = 45f;
    public LayerMask detectionMask;
    
    public GameObject gameManager;
    
    private int currentPoint = 0;
    
    private float detectionTimer = 0f;
    public float timeToLose = 5f; 

    private bool hasBeenSpotted = false;

    
    void Update() {
        Patrol();
        // Start countdown once a target is spotted
        if (!hasBeenSpotted && (CanSeeTarget("Player") || CanSeeTarget("Ghost"))) {
            hasBeenSpotted = true;
            Debug.Log("Spotted! Countdown started.");
        }

        // Once spotted, tick the timer
        if (hasBeenSpotted) {
            detectionTimer += Time.deltaTime;

            if (detectionTimer >= timeToLose) {
                Debug.Log("Level lost!");
                gameManager.GetComponent<LoopManager>().LoseLevel();
            }
        }
    }

     void Patrol() {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        // Move
        transform.position += dir * speed * Time.deltaTime;

        // Smoothly rotate toward movement direction
        if (dir != Vector3.zero) {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Switch to next point if close enough
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
     
    bool CanSeeTarget(string tag) {
        GameObject target = GameObject.FindWithTag(tag);
        if (!target) return false;

        // Visualize FOV edges
        Vector3 leftEdge = Quaternion.Euler(0, -visionAngle, 0) * transform.forward * visionRange;
        Vector3 rightEdge = Quaternion.Euler(0, visionAngle, 0) * transform.forward * visionRange;

        Debug.DrawRay(transform.position, leftEdge, Color.yellow); // left edge of vision
        Debug.DrawRay(transform.position, rightEdge, Color.yellow); // right edge of vision
        Debug.DrawRay(transform.position, transform.forward * visionRange, Color.cyan); // center
        
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToTarget);

        if (angle < visionAngle) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < visionRange) {
                // Draw the vision line
                Debug.DrawLine(transform.position, target.transform.position, Color.green);

                // Check line of sight
                if (!Physics.Linecast(transform.position, target.transform.position, detectionMask)) {
                    return true;
                } else {
                    Debug.DrawLine(transform.position, target.transform.position, Color.red); // blocked line
                }
            }
        }
        return false;
    }
    
    
}