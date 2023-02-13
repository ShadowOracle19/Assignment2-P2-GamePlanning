using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void OnSuccess(int sessionIndex)
    {
        var field = GetComponent<TextMeshProUGUI>();

        text.text = $"Session: {sessionIndex}";
    }

    public void OnFail(string error)
    {
        var field = GetComponent<TextMeshProUGUI>();

        text.text = $"Error: {error}";
    }
}
