using UnityEngine;
using System.Collections;

public class GuardAI : MonoBehaviour {
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float visionRange = 10f;
    public float visionAngle = 45f;
    public LayerMask detectionMask;

    private int currentPoint = 0;

    void Update() {
        Patrol();

        if (CanSeeTarget("Player") || CanSeeTarget("Ghost")) {
            Debug.Log($"{name} saw a target!");
            // TODO: Raise alarm or chase
        }
    }

    void Patrol() {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    bool CanSeeTarget(string tag) {
        GameObject target = GameObject.FindWithTag(tag);
        if (!target) return false;

        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToTarget);

        if (angle < visionAngle) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < visionRange) {
                if (!Physics.Linecast(transform.position, target.transform.position, detectionMask)) {
                    return true;
                }
            }
        }

        return false;
    }
}