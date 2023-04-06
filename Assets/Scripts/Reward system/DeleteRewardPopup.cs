using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRewardPopup : MonoBehaviour
{
    public Transform rewardParent;
    public bool rewardDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rewardDisplayed && Input.anyKeyDown)
        {
            foreach(Transform child in rewardParent)
            {
                Destroy(child.gameObject);
            }
            GameManager.Instance.EndEncounter();
            rewardDisplayed = false;
        }
    }
}
