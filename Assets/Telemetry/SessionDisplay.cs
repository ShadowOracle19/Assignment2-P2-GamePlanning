using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;

public class SessionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void OnSuccess(int sessionIndex)
    {
        if(sessionIndex < 0)
        {
            text.text = $"Offline Session: <color=red>{sessionIndex}</color>";
        }
        else
        {
            text.text = $"Online Session: <color=green>{sessionIndex}</color>  (Make note of this number)";
        }
    }

    public void OnFail(string error)
    {
        text.text = $"Error: {error}";
    }
}
