using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    [Header("Pre-Game Data")]
    [SerializeField] GameMaster gameMaster;
    [SerializeField] TextMeshProUGUI dialogueShower;
    [SerializeField] TextMeshProUGUI titleShower;
    [SerializeField] CanvasGroup textCanvas;
    [SerializeField] AudioSource[] customerSources;
    [SerializeField] float timeToNextSentence;

    [Header("In-Game Data, don't edit!")]
    public Customer currentCustomer;
    private bool talking;
    private Queue<DialoguePiece> dialoguesToShow = new Queue<DialoguePiece>();
    private DialoguePiece activeDialoguePiece;

    public void GetNewDialogue(Dialogue dialogue,bool showImmediately){
        
        foreach (DialoguePiece piece in dialogue.sentenceData)
        {
            dialoguesToShow.Enqueue(piece);
        }

        if(showImmediately)
            ShowDialogue();
    }

    public void GetNewDialogue(Dialogue dialogue){
        GetNewDialogue(dialogue,false);
    }

    public void ShowDialogue(){
        activeDialoguePiece = dialoguesToShow.Dequeue();
        StartCoroutine(DialogueShower(activeDialoguePiece.sentence));
        SetActiveTextBox(true);
    }

    private void SetText(string input){
        dialogueShower.text = input;
    }

    private void SetActiveTextBox(bool value){
        
        if(value == false)
            textCanvas.alpha = 0;
        if(value == true)
            textCanvas.alpha = 1;

        textCanvas.interactable = value;
        textCanvas.blocksRaycasts = value;
    }

    private IEnumerator DialogueShower(string sentence){
        
        talking = true;
        StartCoroutine(PlaySoundWhileTalking());
        string currentString = "";
        SetText(currentString);
        titleShower.text = activeDialoguePiece.personTalking;

        foreach (char character in sentence)
        {
            currentString += character;
            SetText(currentString);

            yield return new WaitForSeconds(1 / currentCustomer.charSpeedPerSecondInDialogueBox);
        }
        
        talking = false;
        yield return new WaitForSeconds(timeToNextSentence);

        if(dialoguesToShow.Count > 0){
            activeDialoguePiece = dialoguesToShow.Dequeue();
            StartCoroutine(DialogueShower(activeDialoguePiece.sentence));
        }
        else{
            SetActiveTextBox(false);
            gameMaster.SetCutsceneMode(false);
        }
    }

    private IEnumerator PlaySoundWhileTalking(){
        
        int index = 0;
        
        foreach (AudioSource source in customerSources)
        {
            source.clip = activeDialoguePiece.personClip;
        }
        
        while (talking)
        {
            customerSources[index].Play();
            index++;

            if(index == customerSources.Length)
                index = 0;

            yield return new WaitForSeconds(currentCustomer.charSoundRepeatRate);
        }
    }
}
