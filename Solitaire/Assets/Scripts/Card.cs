using UnityEngine;
using System.Collections;
using System;

public enum Seeds { picche, cuori, fiori, quadri }
public enum BlackOrRed { black, red }
[Serializable]
public class Card {

    public Seeds mySeed;
    public byte myNumber;
    public BlackOrRed color = BlackOrRed.black;

    public Sprite numberSprite;
    private Sprite seedSprite;

    public byte MyNumber
    {
        get
        {
            return myNumber;
        }

        set
        {
            myNumber = value;
            numberSprite = Resources.Load<Sprite>("carte/numericarte/new/" + myNumber);
        }
    }

    internal Seeds MySeed
    {
        get
        {
            return mySeed;
        }

        set
        {
            mySeed = value;
            seedSprite = Resources.Load<Sprite>("carte/semi/new/"+mySeed.ToString());
        }
    }

    public Sprite NumberSprite
    {
        get
        {
            return numberSprite;
        }

        set
        {
            
            numberSprite = value;
        }
    }

    public Sprite SeedSprite
    {
        get
        {
            return seedSprite;
        }

        set
        {
            seedSprite = value;
        }
    }
}
