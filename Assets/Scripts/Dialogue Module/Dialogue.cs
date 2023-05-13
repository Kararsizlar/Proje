using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public List<DialoguePiece> sentenceData;
}

[System.Serializable]
public class DialoguePiece{
    [Multiline] public string sentence;
    public string personTalking;
    public AudioClip personClip;
    public Sprite personSprite;
}
