using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveAndLoadController : MonoBehaviour
{
    public GameObject waffle;
    public List<GameObject> npcs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SaveState createSaveState()
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

        return save;
    }

    public void loadSaveState(int saveSlot)
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveState save = (SaveState)bf.Deserialize(file);
            file.Close();

            waffle.transform.position = new Vector3(save.playerPos[0], save.playerPos[1], save.playerPos[2]); // Load the player position
            loadQuestList(save.startedQuests, false); // Load the started quest
            loadQuestList(save.finishedQuests, true); // Load the finished quests
            loadAchievementBanners(save.finishedAchievements);

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
}
