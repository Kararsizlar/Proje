using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    public bool clickable = false;

    [Header("Title")]
    [SerializeField] RectTransform title;
    [SerializeField] Vector3 titleStart,titleTarget;
    [SerializeField] float titleTime,titleHideTime;

    [Header("Levels")]
    [SerializeField] RectTransform level;
    [SerializeField] Vector3 levelStart,levelTarget;
    [SerializeField] float levelTime;

    [Header("Level Title")]
    [SerializeField] RectTransform levelTitle;
    [SerializeField] Vector3 levelTitleStart,levelTitleTarget;
    [SerializeField] float levelTitleTime;

    [Header("Credit Title")]
    [SerializeField] RectTransform creditTitle;
    [SerializeField] Vector3 creditTitleStart,creditTitleTarget;
    [SerializeField] float creditTitleTime;

    [Header("Names")]
    [SerializeField] RectTransform names;
    [SerializeField] Vector3 namesStart,namesTarget;
    [SerializeField] float namesTime;

    [Header("Customer")]
    [SerializeField] RectTransform customer;
    [SerializeField] Vector3 customerStart,customerTarget;
    [SerializeField] float customerTime;
    
    [Header("Buttons")]
    [SerializeField] MenuButtons playButton;
    [SerializeField] MenuButtons settingButton;
    [SerializeField] MenuButtons creditButton;

    [Header("Settings")]
    [SerializeField] RectTransform settings;
    [SerializeField] Vector3 settingsStart,settingsTarget;
    [SerializeField] float settingsTime;

    [Header("Settings Panel")]
    [SerializeField] RectTransform settingsPanel;
    [SerializeField] Vector3 settingsPanelStart,settingsPanelTarget;
    [SerializeField] float settingsPanelTime;

    [Header("Timings")]
    [SerializeField] float timeForButtonsBefore;
    [SerializeField] float timeBetweenEachButton;


    [SerializeField] UIItemMover itemMover;

    private void Start()
    {
          StartCoroutine(IShowMenuScreen());
    }

    private IEnumerator IShowMenuScreen(){
        StartCoroutine(itemMover.IMoveUI(title,titleTarget,titleTime));
        yield return new WaitForSeconds(timeForButtonsBefore);
        creditButton.MoveToScreen();
        yield return new WaitForSeconds(timeBetweenEachButton);
        playButton.MoveToScreen();
        yield return new WaitForSeconds(timeBetweenEachButton);
        settingButton.MoveToScreen();
        yield return new WaitForSeconds(settingButton.buttonTime);
        clickable = true;
    }

    private IEnumerator IShowLevelScreen(){
        
        StartCoroutine(itemMover.IMoveUI(levelTitle,levelTitleTarget,levelTitleTime));        
        yield return new WaitForSeconds(timeBetweenEachButton);
        StartCoroutine(itemMover.IMoveUI(level,levelTarget,levelTime));
        StartCoroutine(itemMover.IMoveUI(customer,customerTarget,customerTime));
        yield return new WaitForSeconds(customerTime);
        clickable = true;
    }

    private IEnumerator IShowCredits(){
        StartCoroutine(itemMover.IMoveUI(creditTitle,creditTitleTarget,creditTitleTime));
        StartCoroutine(itemMover.IMoveUI(names,namesTarget,namesTime));
        yield return new WaitForSeconds(namesTime);
        clickable = true;
    }

    private IEnumerator IShowSettings(){
        StartCoroutine(itemMover.IMoveUI(settings,settingsTarget,settingsTime));
        StartCoroutine(itemMover.IMoveUI(settingsPanel,settingsPanelTarget,settingsPanelTime));
        yield return new WaitForSeconds(settingsTime);
        clickable = true;
    }

    private void HideMenu(){
        StartCoroutine(itemMover.IMoveUI(title,titleStart,titleHideTime));
        creditButton.HideFromScreen();
        playButton.HideFromScreen();
        settingButton.HideFromScreen();
    }

    private void HideLevel(){
        StartCoroutine(itemMover.IMoveUI(level,levelStart,levelTime));
        StartCoroutine(itemMover.IMoveUI(levelTitle,levelTitleStart,levelTitleTime));
        StartCoroutine(itemMover.IMoveUI(customer,customerStart,customerTime));
    }

    private void HideCredits(){
        StartCoroutine(itemMover.IMoveUI(creditTitle,creditTitleStart,creditTitleTime));
        StartCoroutine(itemMover.IMoveUI(names,namesStart,namesTime));
    }

    private void HideSettings(){
        StartCoroutine(itemMover.IMoveUI(settings,settingsStart,settingsTime));
        StartCoroutine(itemMover.IMoveUI(settingsPanel,settingsPanelStart,settingsPanelTime));
    }


    public void ShowLevelChooseScreen(){

        if(clickable == false)
            return;
        
        clickable = false;
        StopAllCoroutines();
        HideMenu();
        StartCoroutine(IShowLevelScreen());
    }

    public void ShowMenuScreen(){

        if(clickable == false)
            return;
        
        clickable = false;

        StopAllCoroutines();
        HideLevel();
        HideCredits();
        HideSettings();
        StartCoroutine(IShowMenuScreen());
    }

    public void ShowCreditScreen(){
        if(clickable == false)
            return;
        
        clickable = false;
        StopAllCoroutines();
        StartCoroutine(IShowCredits());
        HideMenu();

    }

    public void ShowSettingsScreen(){
        if(clickable == false)
            return;
        
        clickable = false;
        StopAllCoroutines();
        StartCoroutine(IShowSettings());
        HideMenu();
    }
}
