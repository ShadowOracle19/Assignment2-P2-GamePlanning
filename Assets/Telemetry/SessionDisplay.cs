using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void OnSuccess(int sessionIndex)
    {
        text.text = $"Session: {sessionIndex}";
    }

    public void OnFail(string error)
    {
        text.text = $"Error: {error}";
    }
}
