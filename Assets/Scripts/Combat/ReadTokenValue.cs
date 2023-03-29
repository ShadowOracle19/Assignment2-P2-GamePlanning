using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadTokenValue : MonoBehaviour
{
    public ActionTokens currentToken;
    public SpriteRenderer icon;
    public TextMeshProUGUI tooltipText;

    private void Start()
    {
        icon.sprite = currentToken.icon;
    }
}
