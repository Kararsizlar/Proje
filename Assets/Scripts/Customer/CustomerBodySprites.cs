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
            default:
                Debug.Log("default case?");
                return defaultSprite;
        }
    }
}

public enum BodySprite{
    casual = 0,
    happy = 1,
    angry = 2,
    sad = 3,
    shocked = 4
}
