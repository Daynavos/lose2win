using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FOVRenderer : MonoBehaviour {
    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public int resolution = 50;

    public LayerMask obstacleMask;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
    }
    void LateUpdate() {
        DrawFieldOfView();
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    void DrawFieldOfView() {
        List<Vector3> points = new List<Vector3>();
        float step = viewAngle / resolution;

        for (int i = 0; i <= resolution; i++) {
            float angle = -viewAngle / 2 + step * i;
            Vector3 dir = DirFromAngle(angle, false); 
            Vector3 point = transform.position + dir * viewRadius;

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewRadius, obstacleMask))
                point = hit.point;

            points.Add(transform.InverseTransformPoint(point));
        }


        // Build mesh
        vertices = new Vector3[points.Count + 1];
        triangles = new int[(points.Count - 1) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < points.Count; i++)
            vertices[i + 1] = points[i];

        for (int i = 0; i < points.Count - 1; i++) {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool global) {
        if (!global && transform.parent != null)
            angleInDegrees += transform.parent.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}