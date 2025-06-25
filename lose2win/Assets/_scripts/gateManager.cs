using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    private bool padLockA_primed = false;
    private bool padLockB_primed = false;
    
    public GameObject padlockA;
    public GameObject padlockB;

    void Update()
    {
        padLockA_primed = padlockA.GetComponent<padlock>().primed;
        padLockB_primed = padlockB.GetComponent<padlock>().primed;
        
        if (padLockA_primed && padLockB_primed)
        {
            Destroy(gameObject);
        }
    }
}
