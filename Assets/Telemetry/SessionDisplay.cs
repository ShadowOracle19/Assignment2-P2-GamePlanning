using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void OnSuccess(int sessionIndex)
    {
        if(sessionIndex < 0)
        {
            text.text = $"Offline Session: {sessionIndex}";
        }
        else
        {
            text.text = $"Online Session: {sessionIndex}";
        }
    }

    public void OnFail(string error)
    {
        text.text = $"Error: {error}";
    }
}
