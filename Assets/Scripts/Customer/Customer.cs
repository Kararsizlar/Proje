using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Customer",fileName = "New Customer")]
public class Customer : ScriptableObject
{
    [Header("Müşteri genel bilgileri")]
    public string customerName;
    public Sprite levelSelectImage;
    public Potion targetPotion;

    [Header("Müşteri başlangıç diyalogu")]
    public Dialogue customerDialogueAtStart;
    [Header("Müşteri son diyalogları")]
    public Dialogue customerDialogueAtEndFail;
    public Dialogue customerDialogueAtEndMid;
    public Dialogue customerDialogueAtEndSuccess;
    
    public float charSpeedPerSecondInDialogueBox;
    public float charSoundRepeatRate;
}
