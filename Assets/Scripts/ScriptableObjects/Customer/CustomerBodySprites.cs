using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerBodySprites
{
    public Sprite defaultSprite;
    public Sprite happySprite;
    public Sprite angrySprite;
    public Sprite sadSprite;
    public Sprite shockedSprite;

    public Sprite GetSprite(BodySprite bodySprite){
        switch (bodySprite)
        {
            case BodySprite.casual:
                return defaultSprite;
            case BodySprite.happy:
                return happySprite;
            case BodySprite.angry:
                return angrySprite;
            case BodySprite.sad:
                return sadSprite;
            case BodySprite.shocked:
                return shockedSprite;
            case BodySprite.none:
                return null;
            default:
                Debug.Log("default case?");
                return defaultSprite;
        }
    }
}

public enum BodySprite{
    none = 0,
    casual = 1,
    happy = 2,
    angry = 3,
    sad = 4,
    shocked = 5,
    custom = 6
}
