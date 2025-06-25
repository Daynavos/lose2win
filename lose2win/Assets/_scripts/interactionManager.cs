using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionManager : MonoBehaviour
{
    public bool hasKey = false;
    private bool canSwitch = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("padlock"))
        {
            canSwitch = false;
        }
    }
}
