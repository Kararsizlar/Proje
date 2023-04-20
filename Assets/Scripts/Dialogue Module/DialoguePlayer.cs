using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Dialogue demoText;
    [SerializeField] bool debug;

    [Header("Values")]
    [SerializeField] TextMeshProUGUI dialogueShower;
    [SerializeField] CanvasGroup textCanvas;
    [SerializeField] float charAddTime;
    [SerializeField] float timeToNextSentence;
    private Queue<string> dialoguesToShow = new Queue<string>();

    public void GetNewDialogue(Dialogue dialogue,bool showImmediately){
        
        foreach (string sentence in dialogue.sentences)
        {
            dialoguesToShow.Enqueue(sentence);
        }

        if(showImmediately)
            ShowDialogue();
    }

    public void GetNewDialogue(Dialogue dialogue){
        GetNewDialogue(dialogue,false);
    }

    public void ShowDialogue(){
        //Some checks will be made here. Not yet, i will just redirect it at the moment.

        StartCoroutine(DialogueShower(dialoguesToShow.Dequeue()));
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

        string currentString = "";
        SetText(currentString);

        foreach (char character in sentence)
        {
            currentString += character;
            SetText(currentString);

            yield return new WaitForSeconds(charAddTime);
        }

        yield return new WaitForSeconds(timeToNextSentence);
        if(dialoguesToShow.Count > 0)
            StartCoroutine(DialogueShower(dialoguesToShow.Dequeue()));
        else
            SetActiveTextBox(false);
    }

    private IEnumerator Start(){

        yield return new WaitForSeconds(3);

        if(debug)
            GetNewDialogue(demoText,true);
    }
}
