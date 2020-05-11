using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Canvas newGameUI;
    public Canvas loadGameUI;
    public Canvas controlsUI;

    public AudioSource mainMenuMusic;

    public Camera camera;
    public GameObject waffle;

    public GameObject saveAndLoadManager;

    private int mostRecentSaveSlot = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("saveSlot"))
        {
            mostRecentSaveSlot = PlayerPrefs.GetInt("saveSlot");
        }
        questUI.enabled = false;
        inventoryUI.enabled = false;
        pauseUI.enabled = false;
        questDescriptionUI.enabled = false;
        optionsUI.enabled = false;
        achievementsUI.enabled = false;
        creditsUI.enabled = false;
        newGameUI.enabled = false;
        loadGameUI.enabled = false;
        controlsUI.enabled = false;

        toggleScriptsOnStart(false);
    }

    private void gameSelected(bool controlsUIVisible)
    {
        mainMenuMusic.Stop();
        if (!controlsUIVisible)
        {
            Time.timeScale = 1;
            lockCursor();
        }
        else
        {
            Time.timeScale = 0.00001f;
        }
        mainMenuUI.enabled = false;
        newGameUI.enabled = false;
        loadGameUI.enabled = false;
        toggleScriptsOnStart(true);
    }

    public void handleContinueButtonHit()
    {
        if(mostRecentSaveSlot != 0 && File.Exists(Application.persistentDataPath + "/gamesave" + mostRecentSaveSlot + ".save"))
        {
            gameSelected(false);
            saveAndLoadManager.GetComponent<SaveAndLoadController>().load(mostRecentSaveSlot);
        }
    }

    // NEW GAME BUTTON HANDLING
    public void handleNewGameButtonHit()
    {
        newGameUI.enabled = true;
    }

    public void handleCloseNewGamePopup()
    {
        newGameUI.enabled = false;
    }

    public void handleNewGameSaveSlotOne()
    {
        mostRecentSaveSlot = 1;
        PlayerPrefs.SetInt("saveSlot", 1);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(1);
        controlsUI.enabled = true;
        mainMenuUI.enabled = false;
        newGameUI.enabled = false;
    }

    public void handleNewGameSaveSlotTwo()
    {
        mostRecentSaveSlot = 2;
        PlayerPrefs.SetInt("saveSlot", 2);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(2);
        controlsUI.enabled = true;
        mainMenuUI.enabled = false;
        newGameUI.enabled = false;
    }

    public void handleNewGameSaveSlotThree()
    {
        mostRecentSaveSlot = 3;
        PlayerPrefs.SetInt("saveSlot", 3);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(3);
        controlsUI.enabled = true;
        mainMenuUI.enabled = false;
        newGameUI.enabled = false;
    }

    public void handleCloseControlsPopup()
    {
        mainMenuMusic.Stop();
        lockCursor();
        gameSelected(true);
        controlsUI.enabled = false;
        Time.timeScale = 1;
    }

    // LOAD GAME BUTTON HANDLING
    public void handleLoadGameButtonHit()
    {
        loadGameUI.enabled = true;
        GameObject.Find("LoadGamePanel").transform.GetChild(1).GetComponent<Button>().gameObject.SetActive(File.Exists(Application.persistentDataPath + "/gamesave1.save"));
        GameObject.Find("LoadGamePanel").transform.GetChild(2).GetComponent<Button>().gameObject.SetActive(File.Exists(Application.persistentDataPath + "/gamesave2.save"));
        GameObject.Find("LoadGamePanel").transform.GetChild(3).GetComponent<Button>().gameObject.SetActive(File.Exists(Application.persistentDataPath + "/gamesave3.save"));
    }
    public void handleCloseLoadGamePopup()
    {
        loadGameUI.enabled = false;
    }

    public void handleLoadSlotOne()
    {
        mostRecentSaveSlot = 1;
        PlayerPrefs.SetInt("saveSlot", 1);
        gameSelected(false);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(1);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().load(1);
    }

    public void handleLoadSlotTwo()
    {
        mostRecentSaveSlot = 2;
        PlayerPrefs.SetInt("saveSlot", 2);
        gameSelected(false);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(2);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().load(2);
    }

    public void handleLoadSlotThree()
    {
        mostRecentSaveSlot = 3;
        PlayerPrefs.SetInt("saveSlot", 3);
        gameSelected(false);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().setSaveSlot(3);
        saveAndLoadManager.GetComponent<SaveAndLoadController>().load(3);
    }

    // MAIN MENU BUTTONS
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
    public void handleBackButtonHit()
    {
        optionsUI.enabled = false;
        achievementsUI.enabled = false;
        creditsUI.enabled = false;
    }

    // PAUSE MENU
    public void handleResumeButtonHit()
    {
        Time.timeScale = 1;
        pauseUI.enabled = false;
        camera.GetComponent<ThirdPersonCamera>().enabled = true;
        waffle.GetComponent<CharacterController>().enabled = true;
        lockCursor();
        PlayerController.setPauseUIVisible(false);
        optionsUI.enabled = false;
        achievementsUI.enabled = false;
    }

    public void handleSaveAndQuitButtonPushed()
    {
        mainMenuMusic.Play();
        saveAndLoadManager.GetComponent<SaveAndLoadController>().save();
        pauseUI.enabled = false;
        mainMenuUI.enabled = true;
        PlayerController.setPauseUIVisible(false);
        toggleScriptsOnStart(false);
        SceneManager.LoadScene("MainScene");
    }

    // HELPER METHODS FOR GAME START/END 
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
        waffle.GetComponent<WaffleInventoryManager>().enabled = true;
        waffle.GetComponent<WaffleQuestController>().enabled = true;
        GameObject.Find("Eugene").GetComponent<DogPatrolMovement>().enabled = toggle;
    }
}
