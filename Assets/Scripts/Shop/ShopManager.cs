using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct ShopkeeperDialogue
{
    public List<string> entry;
    public List<string> switchToBuy;
    public List<string> switchToSell;
    public List<string> leave;
    public List<string> purchaseItem;
    public List<string> sellItem;
}

public class ShopManager : MonoBehaviour
{
    public int rationsPrice;
    public int medkitPrice;

    public bool isSelling = false;

    public ShopkeeperDialogue dialogue;

    [Header("UI Elements")]
    public Image buyingImage;
    public Image sellingImage;
    public TextMeshProUGUI rationsPriceText;
    public TextMeshProUGUI rationsOwned;
    public TextMeshProUGUI medkitsPriceText;
    public TextMeshProUGUI medkitsOwned;
    public TextMeshProUGUI playerCaps;
    public TextMeshProUGUI shopkeeperDialogueText;
    public TextMeshProUGUI shopkeeperDialogueTextCheckSize;

    


    // Update is called once per frame
    void Update()
    {
        playerCaps.text = $"Caps: {GameManager.Instance.caps}";
        rationsOwned.text = $"Own: {GameManager.Instance.amountOfRations}";
        medkitsOwned.text = $"Own: {GameManager.Instance.amountOfMedkits}";
        if(isSelling)
        {
            buyingImage.gameObject.SetActive(false);
            sellingImage.gameObject.SetActive(true);

            rationsPriceText.text = $"Rations \n${rationsPrice/2}";
            medkitsPriceText.text = $"Medkits \n${medkitPrice/2}";
        }
        else
        {
            buyingImage.gameObject.SetActive(true);
            sellingImage.gameObject.SetActive(false);

            rationsPriceText.text = $"Rations \n${rationsPrice}";
            medkitsPriceText.text = $"Medkits \n${medkitPrice}";
        }
    }

    public void Buy()
    {
        int randomInt = UnityEngine.Random.Range(1, 3);
        isSelling = false;
        StartCoroutine(TypeText(dialogue.switchToBuy[UnityEngine.Random.Range(0, dialogue.switchToBuy.Count)]));
    }

    public void Sell()
    {
        isSelling = true;
        StartCoroutine(TypeText(dialogue.switchToSell[UnityEngine.Random.Range(0, dialogue.switchToSell.Count)]));
    }

    public void BuySellRations()
    {
        if(isSelling)
        {
            StartCoroutine(TypeText(dialogue.sellItem[UnityEngine.Random.Range(0, dialogue.sellItem.Count)]));
            GameManager.Instance.amountOfRations -= 1;
            GameManager.Instance.caps += rationsPrice/2;
        }
        else
        {
            if (GameManager.Instance.caps < rationsPrice) return;

            StartCoroutine(TypeText(dialogue.purchaseItem[UnityEngine.Random.Range(0, dialogue.purchaseItem.Count)]));
            GameManager.Instance.amountOfRations += 1;
            GameManager.Instance.caps -= rationsPrice;
        }
    }

    public void BuySellMedkits()
    {
        if (isSelling)
        {
            StartCoroutine(TypeText(dialogue.sellItem[UnityEngine.Random.Range(0, dialogue.sellItem.Count)]));
            GameManager.Instance.amountOfMedkits -= 1;
            GameManager.Instance.caps += medkitPrice / 2;
        }
        else
        {
            if (GameManager.Instance.caps < medkitPrice) return;

            StartCoroutine(TypeText(dialogue.purchaseItem[UnityEngine.Random.Range(0, dialogue.purchaseItem.Count)]));
            GameManager.Instance.amountOfMedkits += 1;
            GameManager.Instance.caps -= medkitPrice;
        }
    }

    IEnumerator TypeText(string text)
    {
        shopkeeperDialogueText.text = string.Empty;
        shopkeeperDialogueTextCheckSize.text = string.Empty;

        shopkeeperDialogueTextCheckSize.text = text;
        yield return new WaitForSeconds(0.1f);

        //type each character 1 by 1 
        foreach (char c in text)
        {
            shopkeeperDialogueText.fontSize = shopkeeperDialogueTextCheckSize.fontSize;
            shopkeeperDialogueText.text += c;
            yield return new WaitForSeconds(0.005f);
        }

        yield return null;
    }

    public void EndEncounter()
    {

        StartCoroutine(TypeText(dialogue.leave[UnityEngine.Random.Range(0, dialogue.leave.Count)]));
    }
}