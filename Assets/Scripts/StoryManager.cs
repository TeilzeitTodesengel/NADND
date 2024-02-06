using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public Dictionary<String, StoryPart> StoryParts = new Dictionary<String, StoryPart>();

    public GameObject mainObject;
    // Start is called before the first frame update
    void Start()
    {
        var start = Resources.LoadAll<TextAsset>("StoryParts");
        foreach (TextAsset asset in start)
        {
            StoryPart part = JsonUtility.FromJson<StoryPart>(asset.text);
            StoryParts.Add(part.roomID, part);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
