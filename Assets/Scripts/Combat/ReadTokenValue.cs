using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTokenValue : MonoBehaviour
{
    public ActionTokens currentToken;
    public SpriteRenderer icon;

    private void Start()
    {
        icon.sprite = currentToken.icon;
    }
}
