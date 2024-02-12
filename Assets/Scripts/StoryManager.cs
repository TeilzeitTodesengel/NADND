using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class StoryManager : MonoBehaviour
{
    public Dictionary<String, StoryPart> StoryParts = new Dictionary<String, StoryPart>();
    public GameObject mainObject;
    
    private void Awake()
    {
        DontDestroyOnLoad(GetComponent<StoryManager>());
        StoryParts.Clear();
        var start = Resources.LoadAll<TextAsset>("StoryParts");
        foreach (TextAsset asset in start)
        {
            StoryPart part = JsonUtility.FromJson<StoryPart>(asset.text);
            StoryParts.Add(part.roomID, part);
        }
        Debug.Log("Story Manager Init finished");
    }

    public void LoadPart(string partToLoadID)
    {
        StoryPart partToLoad = StoryParts[partToLoadID];
        GetComponent<RunManager>().currentPart = partToLoad;
        GetComponent<StageManager>().UpdateText(partToLoad);
        Debug.Log(partToLoad.roomName);
    }

    public void LoadSelectedPath(int selectedPath)
    {
        string roomToLoad = GetComponent<RunManager>().currentPart.abzweigungen[selectedPath].targetID;
        LoadPart(roomToLoad);
    }
}
