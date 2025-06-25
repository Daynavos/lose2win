using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearInputs : MonoBehaviour
{
    //public GhostInputRecording recordingSO;
    public GhostTransformRecording recordingSO;

    // Start is called before the first frame update
    void Start()
    {
        recordingSO.ClearRecording();
    }
}
