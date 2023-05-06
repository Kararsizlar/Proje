using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Pre-Game Data")]
    [SerializeField] LevelLoader loader;
    [SerializeField] DialoguePlayer dialoguePlayer;
    [SerializeField] GameObject potionCamera;
    [SerializeField] GameObject customerCamera;

    [Header("In-Game Data, Don't Edit!")]
    public bool cutscene;
    public bool potionComplete;

    private void StartBeginDialogue(){
        SetCutsceneMode(true);
        dialoguePlayer.GetNewDialogue(dialoguePlayer.currentCustomer.customerDialogueAtStart,true);
    }

    private void StartGame(){
        ShowPotionTable();
    }

    private void EndGame(){

    }

    private IEnumerator WaitUntilCutsceneComplete(string functionToRun){
        while(cutscene)
            yield return Time.deltaTime;
        
        Invoke(functionToRun,Time.deltaTime);
    }

    private IEnumerator WaitUntilPotionComplete(){
        while(potionComplete == false)
            yield return Time.deltaTime;

        EndGame();
    }

    public void OnLoadComplete(){
        StartBeginDialogue();
        StartCoroutine(WaitUntilCutsceneComplete("StartGame"));
    }

    public void SetCutsceneMode(bool value){
        cutscene = value;
    }

    public void ShowPotionTable(){
        potionCamera.SetActive(true);
        customerCamera.SetActive(false);
    }

    public void ShowCustomer(){
        potionCamera.SetActive(false);
        customerCamera.SetActive(true);
    }
}
