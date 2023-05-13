using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Pre-Game Data")]
    [SerializeField] LevelLoader loader;
    [SerializeField] DialoguePlayer dialoguePlayer;
    [SerializeField] GameObject potionCamera,potionUI;
    [SerializeField] GameObject customerCamera,customerUI;
    [SerializeField] PotionSuccessCalculator calculator;
    [SerializeField] float failRate,successRate;
    [SerializeField] float afterGameTime;

    [Header("In-Game Data, Don't Edit!")]
    public Customer customer;
    public bool cutscene;
    public bool potionComplete;

    private void StartBeginDialogue(){
        SetCutsceneMode(true);
        dialoguePlayer.GetNewDialogue(dialoguePlayer.currentCustomer.customerDialogueAtStart,true);
    }

    private void StartGame(){
        ShowPotionTable();
    }

    private IEnumerator IEndGame(){
        ShowCustomer();
        yield return new WaitForSeconds(afterGameTime);
        float rate = calculator.successRate;

        if(rate < failRate)
            dialoguePlayer.GetNewDialogue(dialoguePlayer.currentCustomer.customerDialogueAtEndFail,true);
        else if(rate > successRate)
            dialoguePlayer.GetNewDialogue(dialoguePlayer.currentCustomer.customerDialogueAtEndSuccess,true);
        else
            dialoguePlayer.GetNewDialogue(dialoguePlayer.currentCustomer.customerDialogueAtEndMid,true);
        
        yield return new WaitForSeconds(3f);
        ReturnToLobby();
    }

    public void EndGame(){
        StartCoroutine(IEndGame());
    }

    private void ReturnToLobby(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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
        potionUI.SetActive(true);
        customerCamera.SetActive(false);
        customerUI.SetActive(false);
    }

    public void ShowCustomer(){
        potionCamera.SetActive(false);
        potionUI.SetActive(false);
        customerCamera.SetActive(true);
        customerUI.SetActive(true);
    }
}
