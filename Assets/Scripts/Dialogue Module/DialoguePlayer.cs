using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour
{
    [Header("Pre-Game Data")]
    [SerializeField] GameMaster gameMaster;
    [SerializeField] TextMeshProUGUI dialogueShower;
    [SerializeField] TextMeshProUGUI titleShower;
    [SerializeField] CanvasGroup textCanvas;
    [SerializeField] AudioSource[] customerSources;
    [SerializeField] Image personSprite;
    [SerializeField] float timeToNextSentence;

    [Header("In-Game Data, don't edit!")]
    public Customer currentCustomer;
    private bool talking,skipping;
    private Queue<DialoguePiece> dialoguesToShow = new Queue<DialoguePiece>();
    private DialoguePiece activeDialoguePiece;

    public void Input_SkipDialogue(InputAction.CallbackContext ctx){
        skipping = ctx.phase == InputActionPhase.Performed;
    }

    public void GetNewDialogue(Dialogue dialogue,bool showImmediately){
        
        foreach (DialoguePiece piece in dialogue.sentenceData)
        {
            dialoguesToShow.Enqueue(piece);
        }

        if(showImmediately)
            ShowDialogue();
    }

    public void GetNewDialogue(Dialogue dialogue){
        foreach (AudioSource clip in customerSources)
        {
            clip.clip = null;
        }
        GetNewDialogue(dialogue,false);
    }

    public void ShowDialogue(){
        activeDialoguePiece = dialoguesToShow.Dequeue();
        StartCoroutine(DialogueShower(activeDialoguePiece.sentence,activeDialoguePiece.customSprite));
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

    private IEnumerator DialogueShower(string sentence,Sprite customSprite){
        
        talking = true;
        StartCoroutine(PlaySoundWhileTalking());
        string currentString = "";
        SetText(currentString);
        titleShower.text = activeDialoguePiece.personTalking;

        if(customSprite != null){
            personSprite.sprite = customSprite;
            personSprite.color = new Color(1,1,1,1);
        }
        else{
            Sprite s = currentCustomer.bodySprites.GetSprite(activeDialoguePiece.personSprite);
            
            if(s == null){
                personSprite.sprite =  null;
                personSprite.color = new Color(1,1,1,0);
            }else{
                personSprite.color = new Color(1,1,1,1);
                personSprite.sprite = s;
            }
        }


        foreach (char character in sentence)
        {
            currentString += character;
            SetText(currentString);

            if(skipping)
                yield return new WaitForSeconds(1 / currentCustomer.charSpeedPerSecondInDialogueBox / 5);
            else
                yield return new WaitForSeconds(1 / currentCustomer.charSpeedPerSecondInDialogueBox);
        }
        
        talking = false;
        
        if(skipping == false)
            yield return new WaitForSeconds(timeToNextSentence);
        
        NextDialogue();
    }

    private void NextDialogue(){
        if(dialoguesToShow.Count > 0){
            activeDialoguePiece = dialoguesToShow.Dequeue();
            
            StartCoroutine(DialogueShower(activeDialoguePiece.sentence,activeDialoguePiece.customSprite));
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
