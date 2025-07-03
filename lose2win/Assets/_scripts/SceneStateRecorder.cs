using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameSnapshot {
    public Vector3 position;
    public Quaternion rotation;
    public bool isActive;

    public void ApplyTo(Transform t) {
        if (t == null) return;

        t.gameObject.SetActive(isActive); // Reactivate BEFORE applying transform
        t.position = position;
        t.rotation = rotation;
    }

    public static GameSnapshot From(Transform t) {
        return new GameSnapshot {
            isActive = t.gameObject.activeSelf,
            position = t.position,
            rotation = t.rotation
        };
    }
}

public class SceneStateRecorder : MonoBehaviour {
    [System.Serializable]
    public class TrackedObject {
        public string name;
        public Transform transform;
    }

    public List<TrackedObject> trackedObjects = new List<TrackedObject>();
    private Dictionary<string, GameSnapshot> savedSnapshots = new Dictionary<string, GameSnapshot>();

    public void TakeSnapshot() {
        savedSnapshots.Clear();

        foreach (var obj in trackedObjects) {
            if (obj.transform == null) continue;

            obj.name = obj.transform.name; // Ensure name is synced
            savedSnapshots[obj.name] = GameSnapshot.From(obj.transform);
        }

        Debug.Log("📸 Snapshot taken.");
    }

    public void RestoreSnapshot() {
        foreach (var obj in trackedObjects) {
            if (obj.transform == null) continue;

            if (savedSnapshots.TryGetValue(obj.name, out GameSnapshot snapshot)) {
                snapshot.ApplyTo(obj.transform);
            } else {
                Debug.LogWarning($"❗ No snapshot found for {obj.name}");
            }
        }

        Debug.Log("🔁 Snapshot restored.");
    }
}