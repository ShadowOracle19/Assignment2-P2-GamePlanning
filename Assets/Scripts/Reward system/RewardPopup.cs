using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : MonoBehaviour
{
    public Image rewardIcon;
    public TextMeshProUGUI rewardText;

    public string whatRewardTextSays;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rewardText.text = whatRewardTextSays;
    }
}
