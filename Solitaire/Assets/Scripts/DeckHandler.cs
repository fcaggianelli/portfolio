using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DeckHandler : MonoBehaviour, IPointerClickHandler
{
    public Transform cemeteryTR;
    public float duration = 0.5f;
    public AnimationCurve scaleCurve;
    private Transform canvasTR;
    private bool flipInExecution = false;
    Brain brainRef;
    void Awake()
    {
        brainRef = FindObjectOfType<Brain>();
        canvasTR = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        brainRef.Mosse++;
        if (eventData.pointerDrag == null && this.transform.childCount == 0)
        {
            int cemeteryChildCount = cemeteryTR.childCount;


            for (int i = 0; i < cemeteryChildCount; i++)
            {
                int rightPosition = cemeteryTR.childCount - 1;
                
                cemeteryTR.GetChild(rightPosition).gameObject.GetComponent<Draggable>().CardImage.sprite = Resources.Load<Sprite>("retro");
                cemeteryTR.GetChild(rightPosition).gameObject.GetComponent<Draggable>().NumberImage.enabled = false;
                cemeteryTR.GetChild(rightPosition).gameObject.GetComponent<Draggable>().TopSeedImage.enabled = false;
                cemeteryTR.GetChild(rightPosition).gameObject.GetComponent<Draggable>().BigSeedImage.enabled = false;
                cemeteryTR.GetChild(rightPosition).gameObject.GetComponent<Draggable>().isVisible = false;
                cemeteryTR.GetChild(rightPosition).SetParent(this.transform);
            }
            brainRef.Punteggio -= 100;
        }
        else if (eventData.pointerDrag != null && !flipInExecution)
        {
            eventData.pointerDrag.transform.SetParent(canvasTR);
            //eventData.pointerDrag.gameObject.GetComponent<Draggable>().CardImage.sprite = Resources.Load<Sprite>("fronte");
            

            //Debug.Log(eventData.pointerDrag.gameObject.GetComponent<Draggable>().myCard.MyNumber);
            StartCoroutine(Flip(eventData.pointerDrag.gameObject.GetComponent<Draggable>().CardImage.sprite, Resources.Load<Sprite>("fronte"),eventData));
        }
        
        
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage,PointerEventData eventData)
    {
        //spriteRenderer.sprite = startImage;
        GameObject cardGO = eventData.pointerDrag.gameObject;
        Vector3 cardGOPos = cardGO.transform.position;
        Vector3 rightScale = cardGO.transform.localScale;
        float maxScaleOnX = cardGO.transform.localScale.x;
        float time = 0f;
        flipInExecution = true;

        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;
            
            if (scale <= maxScaleOnX)
            {
                Vector3 localScale = cardGO.transform.localScale;
                localScale.x = scale;
                cardGO.GetComponent<RectTransform>().localScale = localScale;                
            }
            

            
            float newX = Mathf.Lerp(cardGOPos.x, cemeteryTR.position.x, time);
            Vector3 newPosition = new Vector3(newX, cardGOPos.y);
            cardGO.transform.position = newPosition;

            if (time >= 0.5f)
            {
                cardGO.GetComponent<Draggable>().CardImage.sprite = endImage;
                cardGO.GetComponent<Draggable>().NumberImage.enabled = true;
                cardGO.GetComponent<Draggable>().TopSeedImage.enabled = true;
                cardGO.GetComponent<Draggable>().BigSeedImage.enabled = true;
                

            }
            
            yield return null;
        }
        cardGO.transform.localScale = rightScale;
        flipInExecution = false;
        cardGO.GetComponent<Draggable>().isVisible = true;
        cardGO.transform.SetParent(cemeteryTR);
    }


}
