using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Brain : MonoBehaviour {

    Card carta;
    
    public List<int> deck;

    public List<Card> deckProva;
    public List<Card> deckCard;

    public GameObject cardPrefab;

    public GameObject slot0;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;
    public GameObject slotDeck;
    public GameObject TextMoves;
    public GameObject TextPunteggio;

    int mosse;
    int punteggio;

    public int Mosse
    {
        get
        {
            return mosse;
        }

        set
        {
            mosse = value;
            TextMoves.GetComponent<Text>().text = mosse.ToString();
        }
    }

    public int Punteggio
    {
        get
        {
            return punteggio;
        }

        set
        {
            
            punteggio = value;
            if (punteggio < 0)
            {
                punteggio = 0;
            }
            else
            {
                punteggio = value;
            }
            TextPunteggio.GetComponent<Text>().text = punteggio.ToString();
        }
    }

    void Awake()
    {
        carta = new Card();
        deck = new List<int>();
        deckProva = new List<Card>();
        deckCard = new List<Card>();
        Initialize();
        
    }

    private void Initialize()
    {

     
        int counter = 0;
        BlackOrRed tempColor = BlackOrRed.red;

        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                tempColor = BlackOrRed.black;
            }
            else
            {
                tempColor = BlackOrRed.red;
            }
            for (int j = 1; j < 14; j++)
            {
                deckProva.Add(new Card());
                deckProva[counter].MyNumber = (byte)j;
                deckProva[counter].color = tempColor;
                deckProva[counter].MySeed = (Seeds)i;
                
                counter++;                

            }
        }
       
        //Shuffle deck
        while (deckProva.Count > 0)
        {
            int index = Random.Range(0, deckProva.Count);
            deckCard.Add(deckProva[index]);
            deckProva.RemoveAt(index);
        }

        InitializeSlot(1, slot0);
        InitializeSlot(2, slot1);
        InitializeSlot(3, slot2);
        InitializeSlot(4, slot3);
        InitializeSlot(5, slot4);
        InitializeSlot(6, slot5);
        InitializeSlot(7, slot6);
        InitializeDeck();

    }

    private void InitializeDeck()
    {
        
        foreach (var card in deckCard)
        {
            
            GameObject newGo = Instantiate(cardPrefab);
            newGo.transform.SetParent(slotDeck.transform);
            newGo.GetComponent<Draggable>().myCard = new Card();
            newGo.GetComponent<Draggable>().myCard = card;
        }
    }

    private void InitializeSlot(int n, GameObject slot)
    {
        Transform temp = slot.transform;
        for (int i = 0; i < n; i++)
        {            
            GameObject newGo = Instantiate(cardPrefab);
            if (temp.childCount > 1)
            {
                newGo.transform.SetParent(temp.GetChild(1));
            }
            else
            {
                newGo.transform.SetParent(temp);
            }
            newGo.GetComponent<Draggable>().myCard = new Card();
            newGo.GetComponent<Draggable>().myCard = deckCard[0];
            temp = newGo.transform;
            
            deckCard.RemoveAt(0);

            if (i >= n-1)
            {
                
                Sprite newSprite = Resources.Load<Sprite>("fronte");
                if (newSprite)
                {
                    newGo.GetComponent<Draggable>().CardImage.sprite = newSprite;
                }
                else
                {
                    Debug.LogError("Sprite not found", this);
                }

                newGo.GetComponent<Draggable>().NumberImage.enabled = true;
                newGo.GetComponent<Draggable>().TopSeedImage.enabled = true;
                newGo.GetComponent<Draggable>().BigSeedImage.enabled = true;
                newGo.GetComponent<Draggable>().isVisible = true;
            }  
            
        }
        slot.GetComponent<DropZone>().WhatIsMyChild();
        
    }
}
