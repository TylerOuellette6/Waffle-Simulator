using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveAndLoadController : MonoBehaviour
{
    public GameObject waffle;
    public List<GameObject> npcs;
    private int saveSlotNum;

    public GameObject superJumpPowerup;
    public GameObject superSpeedPowerup;
    public GameObject superMiniPower;

    public void save()
    {
        SaveState save = createSaveState();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave" + saveSlotNum + ".save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game saved");
    }

    private SaveState createSaveState()
    {
        SaveState save = new SaveState();

        // PLAYER POSITION
        Vector3 playerPos = waffle.transform.position;
        save.playerPos.Add(playerPos.x);
        save.playerPos.Add(playerPos.y);
        save.playerPos.Add(playerPos.z);

        // QUESTS
        List<string> startedQuests = new List<string>();
        List<string> finishedQuests = new List<string>();

        foreach(Quest startedQuest in waffle.GetComponent<WaffleQuestController>().getStartedQuests())
        {
            startedQuests.Add(startedQuest.questName);
        }

        foreach (Quest finishedQuest in waffle.GetComponent<WaffleQuestController>().getFinishedQuests())
        {
            finishedQuests.Add(finishedQuest.questName);
        }

        save.startedQuests = startedQuests;
        save.finishedQuests = finishedQuests;

        // ACHIEVEMENTS
        save.finishedAchievements = AchievementsController.getFinishedAchievementsList();

        // INVENTORY ITEMS
        List<string> tempInventoryItems = new List<string>();
        List<string> permanentInventoryItems = new List<string>();

        foreach(GameObject tempItem in WaffleInventoryManager.getInventoryItemList())
        {
            tempInventoryItems.Add(tempItem.name);
        }
        foreach(GameObject permanentItem in WaffleInventoryManager.getPermanentItemList())
        {
            permanentInventoryItems.Add(permanentItem.name);
        }

        save.tempInventoryItems = tempInventoryItems;
        save.permanentInventoryItems = permanentInventoryItems;

        // COLLECTIBLES
        List<string> collectiblesFound = new List<string>();

        foreach(GameObject collectible in waffle.GetComponent<WaffleCollectibleManager>().getCollectiblesList())
        {
            collectiblesFound.Add(collectible.name);
        }

        save.collectiblesFound = collectiblesFound;

        return save;
    }

    public void load(int saveSlot)
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave" + saveSlot + ".save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave" + saveSlot + ".save", FileMode.Open);
            SaveState save = (SaveState)bf.Deserialize(file);
            file.Close();

            waffle.transform.position = new Vector3(save.playerPos[0], save.playerPos[1], save.playerPos[2]); // Load the player position
            loadInventoryItems(save.tempInventoryItems, save.permanentInventoryItems);
            loadQuestList(save, false); // Load the started quest
            loadQuestList(save, true); // Load the finished quests
            loadAchievementBanners(save.finishedAchievements);
            loadCollectibles(save);

            Debug.Log("Game Loaded");
        }
    }

    private void loadQuestList(SaveState save, bool finished)
    {
        List<string> questNames;
        if (finished)
        {
            questNames = save.finishedQuests;
        }
        else
        {
            questNames = save.startedQuests;
        }

        foreach(string questName in questNames)
        {
            foreach(GameObject npc in npcs)
            {
                foreach(Quest quest in npc.GetComponent<NPCQuestManager>().getQuests())
                {
                    if (questName.Equals(quest.questName))
                    {
                        if (!finished)
                        {
                            quest.setAccepted(true);
                            npc.GetComponent<NPCQuestManager>().setTempCurrentQuest(quest);
                            WaffleQuestController.addQuestToList(quest);
                        }
                        else
                        {
                            WaffleQuestController.completeQuest(quest);
                        }
                    }
                }
            }
        }

        if (!finished)
        {
            foreach (GameObject npc in npcs)
            {
                foreach (Quest quest in npc.GetComponent<NPCQuestManager>().getQuests())
                {
                    if (save.finishedQuests.Contains(quest.questName))
                    {
                        quest.setCompleted(true);
                    }
                    if (!quest.getCompleted())
                    {
                        npc.GetComponent<NPCQuestManager>().setTempCurrentQuest(quest);
                        break;
                    }
                }
            }
        }
    }

    private void loadAchievementBanners(Dictionary<string, string> finishedAchievements)
    {
        AchievementsController.setAchievementCount(finishedAchievements.Count);
        foreach(KeyValuePair<string, string> achievement in finishedAchievements)
        {
            AchievementsController.createAchievementBanner(achievement.Key, achievement.Value);

        }
    }

    private void loadInventoryItems(List<string> tempItems, List<string> permanentItems)
    {
        foreach(string tempItemName in tempItems)
        {
            //GameObject.Find(tempItemName).SetActive(false);
            waffle.GetComponent<WaffleInventoryManager>().addTempItemToInventory(GameObject.Find(tempItemName));
        }
        foreach(string permanentItemName in permanentItems)
        {
            if(permanentItemName.Equals("Super Jump Powerup"))
            {
                waffle.GetComponent<WaffleInventoryManager>().addPermanentItemToInventory(superJumpPowerup);
                waffle.GetComponent<PlayerController>().setRunSpeed(50);
            }
            else if(permanentItemName.Equals("Super Speed Powerup"))
            {
                waffle.GetComponent<WaffleInventoryManager>().addPermanentItemToInventory(superSpeedPowerup);
            }
            else if(permanentItemName.Equals("Super Mini Powerup"))
            {
                waffle.GetComponent<WaffleInventoryManager>().addPermanentItemToInventory(superMiniPower);
            }
            else
            {
                //GameObject.Find(permanentItemName).SetActive(false);
                waffle.GetComponent<WaffleInventoryManager>().addPermanentItemToInventory(GameObject.Find(permanentItemName));
            }
        }
    }

    private void loadCollectibles(SaveState save)
    {
        List<string> collectibles = save.collectiblesFound;
        foreach(string collectibleName in collectibles)
        {
            GameObject tempCollectible = GameObject.Find(collectibleName);
            tempCollectible.SetActive(false);
            waffle.GetComponent<WaffleCollectibleManager>().addCollectible(tempCollectible);
        }
    }

    public void setSaveSlot(int num)
    {
        saveSlotNum = num;
    }
}
