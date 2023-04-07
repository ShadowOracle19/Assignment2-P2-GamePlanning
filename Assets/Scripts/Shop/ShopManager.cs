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

[System.Serializable]
public struct CurrentShoppingSession
{
    public int numberOfRationsBought;
    public int numberOfMedkitsBought;
    public int numberOfRationsSold;
    public int numberOfMedkitsSold;
}

public class ShopManager : MonoBehaviour
{

    public MapNode currentNode;

    public int rationsPrice;
    public int medkitPrice;

    public bool isSelling = false;

    public ShopkeeperDialogue dialogue;

    public CurrentShoppingSession currentShoppingSession;

    public GameObject shop;

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


    private void OnEnable()
    {
        shop.SetActive(true);
        StartCoroutine(TypeText(dialogue.entry[UnityEngine.Random.Range(0, dialogue.entry.Count)]));
    }

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
        StopAllCoroutines();
        isSelling = false;
        StartCoroutine(TypeText(dialogue.switchToBuy[UnityEngine.Random.Range(0, dialogue.switchToBuy.Count)]));
    }

    public void Sell()
    {
        StopAllCoroutines();
        isSelling = true;
        StartCoroutine(TypeText(dialogue.switchToSell[UnityEngine.Random.Range(0, dialogue.switchToSell.Count)]));
    }

    public void BuySellRations()
    {
        StopAllCoroutines();
        if (isSelling)
        {
            if (GameManager.Instance.amountOfMedkits == 0) return;
            StartCoroutine(TypeText(dialogue.sellItem[UnityEngine.Random.Range(0, dialogue.sellItem.Count)]));
            GameManager.Instance.amountOfRations -= 1;
            GameManager.Instance.caps += rationsPrice/2;
            currentShoppingSession.numberOfRationsSold += 1;
        }
        else
        {
            if (GameManager.Instance.caps < rationsPrice) return;

            StartCoroutine(TypeText(dialogue.purchaseItem[UnityEngine.Random.Range(0, dialogue.purchaseItem.Count)]));
            GameManager.Instance.amountOfRations += 1;
            GameManager.Instance.caps -= rationsPrice;
            currentShoppingSession.numberOfRationsBought += 1;
        }
    }

    public void BuySellMedkits()
    {
        StopAllCoroutines();
        if (isSelling)
        {
            if (GameManager.Instance.amountOfMedkits == 0) return;

            StartCoroutine(TypeText(dialogue.sellItem[UnityEngine.Random.Range(0, dialogue.sellItem.Count)]));
            GameManager.Instance.amountOfMedkits -= 1;
            GameManager.Instance.caps += medkitPrice / 2;
            currentShoppingSession.numberOfMedkitsSold += 1;
        }
        else
        {
            if (GameManager.Instance.caps < medkitPrice) return;

            StartCoroutine(TypeText(dialogue.purchaseItem[UnityEngine.Random.Range(0, dialogue.purchaseItem.Count)]));
            GameManager.Instance.amountOfMedkits += 1;
            GameManager.Instance.caps -= medkitPrice;
            currentShoppingSession.numberOfMedkitsBought += 1;
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

    IEnumerator LeavingText(string text)
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

        yield return new WaitForSeconds(2.0f);

        currentNode.finishedEncounter = true;
        GameManager.Instance.map.SetActive(true);
        GameManager.Instance.shopMenuUI.SetActive(false);
        yield return null;
    }

    public void EndEncounter()
    {
        shop.SetActive(false);
        TelemetryLogger.Log(this, "Items bought", currentShoppingSession);

        StopAllCoroutines();
        StartCoroutine(LeavingText(dialogue.leave[UnityEngine.Random.Range(0, dialogue.leave.Count)]));
        


        SoundEffectManager.Instance.mapSFX.Play();
        SoundEffectManager.Instance.shopSFX.Pause();
    }
}
