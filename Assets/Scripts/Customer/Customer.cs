using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Customer",fileName = "New Customer")]
public class Customer : ScriptableObject
{
    [Header("Müşteri genel bilgileri")]
    public string customerName;
    [Multiline] public string levelSelectDialogue;
    public Potion targetPotion;

    [Header("Müşteri diyalog bilgileri")]
    public Dialogue customerDialogueAtStart;
    public float charSpeedPerSecondInDialogueBox;
    public AudioClip charSound;
    public float charSoundRepeatRate;
}
