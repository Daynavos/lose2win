using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotArtifact : MonoBehaviour
{
    public GameObject winPanel;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }
}
