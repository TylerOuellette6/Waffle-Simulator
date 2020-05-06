using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsController : MonoBehaviour
{
    private static int achievementsCount = 0;

    private static Canvas achievementUnlockedPopup;
    private static GameObject achievementUnlockedPanel;

    private static GameObject singleAchievementBannerPrefab;
    private static GameObject achievementBannersPanel;

    private static Dictionary<string, string> finishedAchievementsList = new Dictionary<string, string>();

    private static bool ellfawExpeditionsUnlocked = false;
    private static bool eugenesErrandsUnlocked = false;
    private static bool frenchToastsTasksUnlocked = false;
    private static bool mousesMissionsUnlocked = false;
    private static bool pancakesProjectsUnlocked = false;
    private static bool syrupsSearchesUnlocked = false;
    private static bool someButterUnlocked = false;
    private static bool someMoreButterUnlocked = false;
    private static bool someMoreMoreButterUnlocked = false;
    private static bool theMostButterUnlocked = false;
    private static bool quoteCleaningUnlocked = false;
    private static bool cleaningUnlocked = false;
    private static bool meetTheDeveloperUnlocked = false;
    private static bool goalUnlocked = false;
    private static bool soupUnlocked = false;
    private static bool veggieTalesTapeUnlocked = false;
    private static bool thanksForPlayingUnlocked = false;

    private static int countertopsItemCounter = 0;
    private static int blocksCleanedUp = 0;

    private static int achievementBannerXPos = -500;
    private static int achievementsBannerYPos = 200;

    private static Canvas achievementDescriptionUI;
    private static Text achievementNameText;
    private static Text achievementDescriptionText;

    private static AudioSource unlockedSound;

    public GameObject pictureFrame;
    public GameObject waffle;
    private void Start()
    {
        achievementUnlockedPopup = GameObject.Find("AchievementUnlockedUI").GetComponent<Canvas>();
        achievementUnlockedPopup.enabled = false;
        achievementUnlockedPanel = GameObject.Find("AchievementUnlockedPanel");

        singleAchievementBannerPrefab = (GameObject)Resources.Load(
            "prefabs/AchievementBannerPrefab", typeof(GameObject));
        achievementBannersPanel = GameObject.Find("AchievementBannersPanel");

        achievementDescriptionUI = GameObject.Find("QuestDescriptionUI").GetComponent<Canvas>();
        achievementDescriptionText = GameObject.Find("QuestDescriptionText").GetComponent<Text>();
        achievementNameText = GameObject.Find("QuestDescriptionTitleText").GetComponent<Text>();

        unlockedSound = GameObject.Find("Achievement").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(achievementsCount >= 16)
        {
            unlockThanksForPlaying();
        }

        if ((waffle.transform.position - pictureFrame.transform.position).magnitude < 50.0f)
        {
            unlockMeetTheDeveloper();
        }
    }
    public static void unlockEllfawExpeditions()
    {
        if (!eugenesErrandsUnlocked)
        {
            eugenesErrandsUnlocked = true;
            showAchievementPopup("Ellfaw's Expeditions");
            createAchievementBanner("Ellfaw's Expeditions", "Complete all of Ellfaw’s quests.");
        }
    }

    public static void unlockEugenesErrands()
    {
        if (!ellfawExpeditionsUnlocked)
        {
            ellfawExpeditionsUnlocked = true;
            showAchievementPopup("Eugene's Errands");
            createAchievementBanner("Eugene's Errands", "Complete all of Eugene’s quests.");
        }
    }

    public static void unlockFrenchToastsTasks()
    {
        if (!frenchToastsTasksUnlocked)
        {
            frenchToastsTasksUnlocked = true;
            showAchievementPopup("French Toast’s Tasks");
            createAchievementBanner("French Toast’s Tasks", "Complete all of French Toast’s quests.");
        }
    }

    public static void unlockMousesMissions()
    {
        if (!mousesMissionsUnlocked)
        {
            mousesMissionsUnlocked = true;
            showAchievementPopup("Mouse’s Missions");
            createAchievementBanner("Mouse’s Missions", "Complete all of Mouse’s quests.");
        }
    }

    public static void unlockPancakesProjects()
    {
        if (!pancakesProjectsUnlocked)
        {
            pancakesProjectsUnlocked = true;
            showAchievementPopup("Pancake’s Projects");
            createAchievementBanner("Pancake’s Projects", "Complete all of Pancake’s quests.");
        }
    }

    public static void unlockSyrupsSearches()
    {
        if (!syrupsSearchesUnlocked)
        {
            syrupsSearchesUnlocked = true;
            showAchievementPopup("Syrup’s Searches");
            createAchievementBanner("Syrup’s Searches", "Complete all of Syrup’s quests.");
        }
    }

    public static void unlockSomeButter()
    {
        if (!someButterUnlocked)
        {
            someButterUnlocked = true;
            showAchievementPopup("Some Butter");
            createAchievementBanner("Some Butter", "Find 5 hidden sticks of butter.");
        }
    }

    public static void unlockSomeMoreButter()
    {
        if (!someMoreButterUnlocked)
        {
            someButterUnlocked = true;
            showAchievementPopup("Some More Butter");
            createAchievementBanner("Some More Butter", "Find 10 hidden sticks of butter.");
        }
    }

    public static void unlockSomeMoreMoreButter()
    {
        if (!someMoreMoreButterUnlocked)
        {
            someButterUnlocked = true;
            showAchievementPopup("Some More More Butter");
            createAchievementBanner("Some More More Butter", "Find 15 hidden sticks of butter.");
        }
    }

    public static void unlockTheMostButter()
    {
        if (!theMostButterUnlocked)
        {
            someButterUnlocked = true;
            showAchievementPopup("The Most Butter");
            createAchievementBanner("The Most Butter", "Find all of hidden sticks of butter.");
        }
    }

    public static void checkIfCountertopsCleaned()
    {
        countertopsItemCounter++;
        //Debug.Log(countertopsItemCounter);
        if(countertopsItemCounter >= 18 && !quoteCleaningUnlocked)
        {
            unlockQuoteCleaning();
        }
    }

    public static void unlockQuoteCleaning()
    {
        quoteCleaningUnlocked = true;
        showAchievementPopup("\"Cleaning\"");
        createAchievementBanner("\"Cleaning\"", "\"Clean\" everything off the counter by knocking it all off.");
    }

    public static void checkIfBlocksCleanedUp()
    {
        blocksCleanedUp++;
        if(blocksCleanedUp >= 8)
        {
            unlockCleaning();
        }
    }

    public static void unlockCleaning()
    {
        if (!cleaningUnlocked)
        {
            cleaningUnlocked = true;
            showAchievementPopup("Cleaning");
            createAchievementBanner("Cleaning", "Clean up all of the small human’s blocks and return them to their box.");
        }
    }

    public void unlockMeetTheDeveloper()
    {
        if (!meetTheDeveloperUnlocked)
        {
            meetTheDeveloperUnlocked = true;
            showAchievementPopup("Meet the Developer");
            createAchievementBanner("Meet the Developer", "Find the picture of The Developer.");
        }
    }

    public static void unlockGoal()
    {

        if (!goalUnlocked)
        {
            goalUnlocked = true;
            showAchievementPopup("GOAL!");
            createAchievementBanner("GOAL!", "Push the soccer ball through the goal.");
        }
    }

    public static void unlockSoup()
    {
        if (!soupUnlocked)
        {
            soupUnlocked = true;
            showAchievementPopup("S O U P");
            createAchievementBanner("S O U P", "You madman! You filled your entire inventory with soup! What’s wrong with you?!");
        }
    }

    public static void unlockVeggieTalesTape()
    {
        if (!veggieTalesTapeUnlocked)
        {
            veggieTalesTapeUnlocked = true;
            showAchievementPopup("Veggie Tales Tape");
            createAchievementBanner("Veggie Tales Tape", "You found the one Veggie Tales VHS tape amongst the other VHS tapes! Congrats?");
        }

    }

    public static void unlockThanksForPlaying()
    {
        if (!thanksForPlayingUnlocked)
        {
            thanksForPlayingUnlocked = true;
            showAchievementPopup("Thanks for Playing!");
            createAchievementBanner("Thanks for Playing!", "Unlock all of the achievements.");
        }
    }


    private async static void showAchievementPopup(String achievementName)
    {
        unlockedSound.Play();
        achievementUnlockedPanel.GetComponentInChildren<Text>().text = achievementName;
        achievementUnlockedPopup.enabled = true;
        await Task.Delay(TimeSpan.FromSeconds(3));
        achievementUnlockedPopup.enabled = false;
    }

    public static void createAchievementBanner(String achievementName, String description)
    {
        GameObject newAchievementBanner = (GameObject)Instantiate(singleAchievementBannerPrefab);
        Button achievementButton = newAchievementBanner.GetComponent<Button>();
        achievementButton.onClick.AddListener(delegate { handleAchievementClicked(achievementName, description); });
        newAchievementBanner.transform.SetParent(achievementBannersPanel.transform);
        newAchievementBanner.transform.localPosition = new Vector3(achievementBannerXPos, achievementsBannerYPos, 0);
        newAchievementBanner.transform.GetChild(0).GetComponent<Text>().text = achievementName;

        achievementBannerXPos += 240;

        achievementsCount++;
        if(achievementsCount % 5 == 0)
        {
            achievementsBannerYPos += 120;
            achievementBannerXPos = -500;
        }
    }

    private static void handleAchievementClicked(String achievementName, String description)
    {
        finishedAchievementsList.Add(achievementName, description);
        achievementDescriptionUI.enabled = true;
        achievementNameText.text = achievementName;
        achievementDescriptionText.text = description;
    }

    public static Dictionary<string, string> getFinishedAchievementsList()
    {
        return finishedAchievementsList;
    }

    public static void setAchievementCount(int count)
    {
        achievementsCount = count;
    }


}
