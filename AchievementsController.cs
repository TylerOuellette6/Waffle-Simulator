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

    private static bool quoteCleaningUnlocked = false;
    private static int countertopsItemCounter = 0;

    private static int achievementBannerXPos = -500;
    private static int achievementsBannerYPos = 200;
    private void Start()
    {
        achievementUnlockedPopup = GameObject.Find("AchievementUnlockedUI").GetComponent<Canvas>();
        achievementUnlockedPopup.enabled = false;
        achievementUnlockedPanel = GameObject.Find("AchievementUnlockedPanel");

        singleAchievementBannerPrefab = (GameObject)Resources.Load(
            "prefabs/AchievementBannerPrefab", typeof(GameObject));
        achievementBannersPanel = GameObject.Find("AchievementBannersPanel");
    }

    public static void checkIfCountertopsCleaned()
    {
        countertopsItemCounter++;
        //Debug.Log(countertopsItemCounter);
        if(countertopsItemCounter >= 7)
        {
            unlockQuoteCleaning();
        }
    }

    public async static void unlockQuoteCleaning()
    {
        achievementUnlockedPanel.GetComponentInChildren<Text>().text = "\"Cleaning\"";
        achievementUnlockedPopup.enabled = true;
        await Task.Delay(TimeSpan.FromSeconds(3));
        achievementUnlockedPopup.enabled = false;
        createAchievementBanner("\"Cleaning\"");
    }

    private static void createAchievementBanner(String achievementName)
    {
        GameObject newAchievementBanner = (GameObject)Instantiate(singleAchievementBannerPrefab);
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


}
