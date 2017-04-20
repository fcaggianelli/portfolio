using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler,IPointerUpHandler
{
    public Transform myChild = null;

    Brain brainRef;

    void Awake()
    {
        brainRef = FindObjectOfType<Brain>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();

        if (card != null)
        {
            if (myChild == null)
            {
                if (card.myCard.MyNumber == 13)
                {
                    card.parentToReturn = this.transform;
                    myChild = card.transform;
                    brainRef.Mosse++;
                }
                
                
            }
            else
            {

                if (myChild.parent.gameObject.GetComponent<Draggable>())
                {
                    if (myChild.parent.gameObject.GetComponent<Draggable>().myCard.MyNumber == eventData.pointerDrag.GetComponent<Draggable>().myCard.MyNumber + 1)
                    {
                        if (myChild.parent.gameObject.GetComponent<Draggable>().myCard.color != eventData.pointerDrag.GetComponent<Draggable>().myCard.color)
                        {
                            card.parentToReturn = myChild.transform;
                            myChild = card.transform.GetChild(1);
                            brainRef.Mosse++;
                        }

                    }
                }
                
            }

            
            
        }

    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.transform.childCount == 0)
        {
            myChild = null;
        }
        else
        {
            WhatIsMyChild();
        }

    }

    public void WhatIsMyChild()
    {
        myChild = this.transform;
        while (myChild.childCount > 0)
        {
            if (myChild.childCount == 1)
            {
                myChild = myChild.GetChild(0);
            }
            else
            {
                myChild = myChild.GetChild(1);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WhatIsMyChild();
        if (!myChild.parent.gameObject.GetComponent<Draggable>().isVisible)
        {
            myChild.parent.gameObject.GetComponent<Draggable>().CardImage.sprite = Resources.Load<Sprite>("fronte");
            myChild.parent.gameObject.GetComponent<Draggable>().NumberImage.enabled = true;
            myChild.parent.gameObject.GetComponent<Draggable>().TopSeedImage.enabled = true;
            myChild.parent.gameObject.GetComponent<Draggable>().BigSeedImage.enabled = true;
            myChild.parent.gameObject.GetComponent<Draggable>().isVisible = true;
            brainRef.Punteggio += 5;
        }

        if (eventData.clickCount == 2)
        {
            //logica doppio click
            Debug.Log("asd");
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        WhatIsMyChild();
    }

    

}
