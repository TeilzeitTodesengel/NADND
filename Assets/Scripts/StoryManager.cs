using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string streamingAssetPath = Application.streamingAssetsPath;
        foreach (string filePath in  Directory.GetFiles(streamingAssetPath + "/StoryParts").Where(filename => filename.EndsWith(".json")))
        {
            string fileContent = System.IO.File.ReadAllText(filePath);
            StoryPart part = JsonUtility.FromJson<StoryPart>(fileContent);
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
