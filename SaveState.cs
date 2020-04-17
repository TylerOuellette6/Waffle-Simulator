using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public List<float> playerPos = new List<float>();
    public List<string> startedQuests = new List<string>();
    public List<string> finishedQuests = new List<string>();
    public Dictionary<string, string> finishedAchievements = new Dictionary<string, string>();
    public List<string> permanentInventoryItems = new List<string>();
    public List<string> tempInventoryItems = new List<string>();
    public List<string> collectiblesFound = new List<string>();
}
