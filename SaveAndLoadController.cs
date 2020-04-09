using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveAndLoadController : MonoBehaviour
{
    public GameObject waffle;
    public List<GameObject> npcs;
    public int saveSlotNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            loadQuestList(save.startedQuests, false); // Load the started quest
            loadQuestList(save.finishedQuests, true); // Load the finished quests
            loadAchievementBanners(save.finishedAchievements);
            loadInventoryItems(save.tempInventoryItems, save.permanentInventoryItems);

            Debug.Log("Game Loaded");
        }
    }

    private void loadQuestList(List<string> questNames, bool finished)
    {
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
    }

    private void loadAchievementBanners(Dictionary<string, string> finishedAchievements)
    {
        foreach(KeyValuePair<string, string> achievement in finishedAchievements)
        {
            AchievementsController.createAchievementBanner(achievement.Key, achievement.Value);

        }
    }

    private void loadInventoryItems(List<string> tempItems, List<string> permanentItems)
    {
        foreach(string tempItemName in tempItems)
        {
            waffle.GetComponent<WaffleInventoryManager>().addTempItemToInventory(GameObject.Find(tempItemName));
        }
        foreach(string permanentItemName in permanentItems)
        {
            waffle.GetComponent<WaffleInventoryManager>().addPermanentItemToInventory(GameObject.Find(permanentItemName));
        }
    }

    public void setSaveSlot(int num)
    {
        saveSlotNum = num;
    }
}
