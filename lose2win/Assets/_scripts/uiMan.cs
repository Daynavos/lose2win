using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class uiMan : MonoBehaviour
{

    public GameObject IntroTextPanel;
    public GameObject StartMissionPanel;
    public GameObject CharliePanel;
    
    public List<GameObject> CharlieList = new List<GameObject>();
    private int charlieIndex = 0;
    
    public void NEXT_button()
    {
        IntroTextPanel.SetActive(false);
        StartMissionPanel.SetActive(true);
    }
    
    public void STARTMISSION_button()
    {
        StartMissionPanel.SetActive(false);
        CharliePanel.SetActive(true);
    }

    public void charlieSpeaks()
    {
        charlieIndex++;
        CharlieList[charlieIndex].SetActive(true);
        CharlieList[charlieIndex-1].SetActive(false);
    }
}
