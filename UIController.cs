using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas mainMenuUI;
    public Canvas questUI;
    public Canvas inventoryUI;
    public Canvas pauseUI;
    public Canvas questDescriptionUI;

    void Start()
    {
        questUI.enabled = false;
        inventoryUI.enabled = false;
        pauseUI.enabled = false;
        questDescriptionUI.enabled = false;

        toggleScriptsOnStart(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleContinueButtonHit()
    {
        mainMenuUI.enabled = false;
        lockCursor();
        toggleScriptsOnStart(true);
    }

    public void handleNewGameButtonHit()
    {

    }

    public void handleLoadGameButtonHit()
    {

    }

    public void handleOptionsButtonHit()
    {

    }

    public void handleAchievementsButtonHit()
    {

    }

    public void handleQuitButtonHit()
    {
        Application.Quit();
    }

    public void handleResumeButtonHit()
    {
        pauseUI.enabled = false;
        Camera.main.GetComponent<ThirdPersonCamera>().enabled = true;
        GameObject.Find("Waffle").GetComponent<PlayerController>().enabled = true;
        lockCursor();
        PlayerController.setPauseUIVisible(false);
    }

    public void handleSaveAndQuitButtonPushed()
    {
        // Handle save eventually
        pauseUI.enabled = false;
        mainMenuUI.enabled = true;
        toggleScriptsOnStart(false);
    }

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void toggleScriptsOnStart(bool toggle)
    {
        Camera.main.GetComponent<ThirdPersonCamera>().enabled = toggle;
        GameObject.Find("Waffle").GetComponent<PlayerController>().enabled = toggle;
        GameObject.Find("Eugene").GetComponent<DogPatrolMovement>().enabled = toggle;
    }
}
