using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject tutorialPage;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            tutorialPage.SetActive(true);
        }
    }
}
