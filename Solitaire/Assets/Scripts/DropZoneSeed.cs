using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropZoneSeed : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Seeds seedZone;
    
    private Transform seedChildTR;
    Brain brainRef;

    void Awake()
    {
        brainRef = FindObjectOfType<Brain>();
        seedChildTR = this.transform.GetChild(1);
    }
    public void OnDrop(PointerEventData eventData)
    {
        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();

        
        if (card != null)
        {
            if (card.myCard.MySeed == seedZone)
            {
                if (seedChildTR.childCount + 1 == card.myCard.MyNumber)
                {
                    
                    card.parentToReturn = seedChildTR;
                    brainRef.Mosse++;
                    brainRef.Punteggio += 15;
                }
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
}
