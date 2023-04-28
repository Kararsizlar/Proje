using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Customer customer;
    [SerializeField] bool debug;

    [Header("Values")]
    [SerializeField] TextMeshProUGUI dialogueShower;
    [SerializeField] CanvasGroup textCanvas;
    [SerializeField] AudioSource[] customerSources;
    [SerializeField] float timeToNextSentence;
    private Queue<string> dialoguesToShow = new Queue<string>();

    private bool talking;

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
        
        talking = true;
        StartCoroutine(PlaySoundWhileTalking());
        string currentString = "";
        SetText(currentString);

        foreach (char character in sentence)
        {
            currentString += character;
            SetText(currentString);

            yield return new WaitForSeconds(1 / customer.charSpeedPerSecondInDialogueBox);
        }
        
        talking = false;
        yield return new WaitForSeconds(timeToNextSentence);

        if(dialoguesToShow.Count > 0)
            StartCoroutine(DialogueShower(dialoguesToShow.Dequeue()));
        else
            SetActiveTextBox(false);
    }

    private IEnumerator PlaySoundWhileTalking(){
        
        int index = 0;
        
        foreach (AudioSource source in customerSources)
        {
            source.clip = customer.charSound;        
        }
        
        while (talking)
        {
            customerSources[index].Play();
            index++;

            if(index == customerSources.Length)
                index = 0;

            yield return new WaitForSeconds(customer.charSoundRepeatRate);
        }
    }

    private IEnumerator Start(){

        yield return new WaitForSeconds(3);

        if(debug)
            GetNewDialogue(customer.customerDialogueAtStart,true);
    }
}
