using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//using System;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    public Transform parentToReturn = null;

    Transform canvasTransform;

    public Card myCard = null;
    public Image NumberImage;
    public Image TopSeedImage;
    public Image BigSeedImage;
    public Image CardImage;

    public bool isVisible = false;

    
    void Start()
    {
        canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
        //GetComponent<Image>().color = Random.ColorHSV();
        if (myCard.color == BlackOrRed.black)
        {
            NumberImage.color = Color.black;
        }
        if (myCard.color == BlackOrRed.red)
        {
            NumberImage.color = Color.red;
        }

        NumberImage.sprite = myCard.NumberSprite;
        TopSeedImage.sprite = myCard.SeedSprite;
        BigSeedImage.sprite = myCard.SeedSprite;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isVisible)
        {
            parentToReturn = this.transform.parent;
            this.transform.SetParent(canvasTransform);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isVisible)
        {
            this.transform.SetParent(parentToReturn);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isVisible)
        {
            this.transform.position = eventData.position;
        }
        
       
    }
}
