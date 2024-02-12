using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class StoryManager : MonoBehaviour
{
    
    private Dictionary<String, StoryPart> StoryParts = new Dictionary<String, StoryPart>();
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
        Debug.Log("Story Part Key Check: " + StoryParts.ContainsKey("r2"));
        Debug.Log("Story Manager Init finished");
    }


    private void Update()
    {
        Debug.Log(StoryParts.ContainsKey("r2"));
    }

    public void LoadPart(string partToLoadID)
    {
        /*
        StoryPart partToLoad = StoryParts[partToLoadID];
        GetComponent<RunManager>().currentPart = partToLoad;
        GetComponent<StageManager>().UpdateText(partToLoad);
        */
        Debug.Log(StoryParts.ContainsKey(partToLoadID));
    }
}
