using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManNoKey : MonoBehaviour
{
    private bool padLockA_primed = false;
    private bool padLockB_primed = false;
    
    public GameObject padlockA;
    public GameObject padlockB;
    public GameObject gate;

    void Update()
    {
        padLockA_primed = padlockA.GetComponent<pressurePad>().primed;
        padLockB_primed = padlockB.GetComponent<pressurePad>().primed;
        
        if (padLockA_primed && padLockB_primed)
        {
            gate.SetActive(false);
        }
    }
}
