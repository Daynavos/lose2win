using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionManager : MonoBehaviour
{
    public bool hasKey = false;
    private bool canSwitch = false;
    public GameObject canvas;
    private uiMan uiManScript;
    private bool benched;
    private bool keyed;
    
    public float interactRange = 3f;
    public LayerMask interactLayer;
    public Transform cameraTransform;

    public GameObject E_image;
    
    public GameObject openCase;
    public GameObject Scematic;
    
    public GhostTransformRecording recordingSO;
    

    void Start()
    {
        uiManScript = canvas.GetComponent<uiMan>();
        benched = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("padA"))
        {
            if (benched)
            {
                uiManScript.charlieSpeaks();
                recordingSO.canRecord = true;
            }
        }
        
        if (other.CompareTag("padB"))
        {
            if (keyed)
            {
                uiManScript.charlieSpeaks();
                recordingSO.canRecord = true;
            }
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("padlock"))
        {
            canSwitch = false;
        }

        if (other.CompareTag("padA"))
        {
            recordingSO.canRecord = false;
        }
        
        if (other.CompareTag("padB"))
        {
            recordingSO.canRecord = false;
        }
    }

    private void Update()
    {
        // --- INTERACTION RAYCAST ---
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.green); // ← This is the debug line

        bool didHit = false;

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            GameObject target = hit.collider.gameObject;

            // Check if it's something we can interact with
            if (target.CompareTag("case") || target.CompareTag("folder") || target.CompareTag("Key") || target.CompareTag("RRR"))
            {
                didHit = true;
                E_image.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log($"Interacted with {target.name}");

                    if (target.CompareTag("Key"))
                    {
                        // Example: disable the keycard
                        target.SetActive(false);
                    }

                    if (target.CompareTag("case"))
                    {
                        target.SetActive(false);
                        openCase.SetActive(true);
                        uiManScript.charlieSpeaks();
                    }

                    if (target.CompareTag("folder"))
                    {
                        recordingSO.canMove = false;
                        Scematic.SetActive(true);
                        target.SetActive(false);
                        uiManScript.charlieSpeaks();
                    }
                    
                    if (target.CompareTag("RRR"))
                    {
                        target.SetActive(false);
                        uiManScript.charlieSpeaks();
                        benched = true;
                    }
                }
            }
        }

        // Hide the interaction prompt if nothing is in sight
        if (!didHit)
        {
            E_image.SetActive(false);
        }
    }


}
