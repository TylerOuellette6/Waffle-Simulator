using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    // Thread safe singleton: https://csharpindepth.com/articles/singleton
    //private static SaveState instance = null;
    //private static readonly object padlock = new object();

    public List<float> playerPos = new List<float>();
    public List<string> startedQuests = new List<string>();
    public List<string> finishedQuests = new List<string>();
    public Dictionary<string, string> finishedAchievements = new Dictionary<string, string>();
    public List<string> permanentInventoryItems = new List<string>();
    public List<string> tempInventoryItems = new List<string>();


    //SaveState()
    //{

    //}

    //public static SaveState Instance
    //{
    //    get
    //    {
    //        lock (padlock)
    //        {
    //            if(instance == null)
    //            {
    //                instance = new SaveState();
    //            }
    //            return instance;
    //        }
    //    }
    //}


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
