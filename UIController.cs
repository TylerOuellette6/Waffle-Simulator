using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas mainMenuUI;
    public Canvas questUI;
    public Canvas inventoryUI;
    public Canvas pauseUI;
    public Canvas questDescriptionUI;
    public Canvas optionsUI;
    public Canvas achievementsUI;
    public Canvas creditsUI;

    public Camera camera;
    public GameObject waffle;

    public GameObject saveAndLoadManager;

    void Start()
    {
        questUI.enabled = false;
        inventoryUI.enabled = false;
        pauseUI.enabled = false;
        questDescriptionUI.enabled = false;
        optionsUI.enabled = false;
        achievementsUI.enabled = false;
        creditsUI.enabled = false;

        toggleScriptsOnStart(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleContinueButtonHit()
    {
        // TODO: Pass in save num
        saveAndLoadManager.GetComponent<SaveAndLoadController>().loadSaveState(0);
        Time.timeScale = 1;
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
        optionsUI.enabled = true;
    }

    public void handleAchievementsButtonHit()
    {
        achievementsUI.enabled = true;
    }

    public void handleCreditsButtonHit()
    {
        creditsUI.enabled = true;
    }

    public void handleQuitButtonHit()
    {
        Application.Quit();
    }

    public void handleResumeButtonHit()
    {
        Time.timeScale = 1;
        pauseUI.enabled = false;
        camera.GetComponent<ThirdPersonCamera>().enabled = true;
        waffle.GetComponent<CharacterController>().enabled = true;
        lockCursor();
        PlayerController.setPauseUIVisible(false);
    }

    public void handleBackButtonHit()
    {
        optionsUI.enabled = false;
        achievementsUI.enabled = false;
        creditsUI.enabled = false;
    }

    /*
     * This handles serializing the save data to a file
     */
    public void handleSaveAndQuitButtonPushed()
    {
        // Handle save eventually
        pauseUI.enabled = false;
        mainMenuUI.enabled = true;
        PlayerController.setPauseUIVisible(false);
        toggleScriptsOnStart(false);

        SaveState save = saveAndLoadManager.GetComponent<SaveAndLoadController>().createSaveState();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game saved");
    }

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void toggleScriptsOnStart(bool toggle)
    {
        camera.GetComponent<ThirdPersonCamera>().enabled = toggle;
        waffle.GetComponent<PlayerController>().enabled = toggle;
        waffle.GetComponent<CharacterController>().enabled = true;
        GameObject.Find("Eugene").GetComponent<DogPatrolMovement>().enabled = toggle;
    }
}
