using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isHovered = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isHovered)
        {
            isHovered = true;
            GetComponent<AudioSource>().Play();
        }
        GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
