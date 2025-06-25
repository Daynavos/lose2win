using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class padlock : MonoBehaviour
{
    public bool primed = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            if (other.GetComponent<interactionManager>().hasKey)
            {
                primed = true;
                Debug.Log("primed");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        primed = false;
    }
}
