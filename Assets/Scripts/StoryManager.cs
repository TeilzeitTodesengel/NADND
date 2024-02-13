using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
/*
 * Der Story Manager ist dafür da aus den json Datein für das Spiel verwertbare
 * Objekte zu machen und diese zu verwalten.
 */

public class StoryManager : MonoBehaviour
{
    public Dictionary<String, StoryPart> StoryParts = new Dictionary<String, StoryPart>();
    public GameObject mainObject;
    
    // Hier wird der Story Manager Initialisiert 
    private void Awake()
    {
        // Als erstes wird die Story Manager Komponente als DontDestroy on load deklariert
        DontDestroyOnLoad(GetComponent<StoryManager>());
        // Als erstes wird das die Liste der Story Parts gelöscht
        StoryParts.Clear();
        // Hier wird der Pfad zu dem Streaming Assets Ordner in einen String gepackt
        string streamingAssetPath = Application.streamingAssetsPath;
        /* In dieser for-Schleife werden als erste alle json-Datein aus dem StoryParts Verzeichnis in dem StreamingAssets
         Ordner geladen. Dann wird ihr Inhalt ausgelesen und mit Hilfe der JsonUtility von Unity in ein Story Part
         Objekt umgewandelt. Dann wird das Objekt der StoryParts Liste hinzugefügt.*/ 
        foreach (string filePath in  Directory.GetFiles(streamingAssetPath + "/StoryParts").Where(filename => filename.EndsWith(".json")))
        {
            string fileContent = System.IO.File.ReadAllText(filePath);
            StoryPart part = JsonUtility.FromJson<StoryPart>(fileContent);
            StoryParts.Add(part.roomID, part);
        }
        Debug.Log("Story Manager Init finished");
    }

    // Diese Funktion ist dazu dar ein anderen Story Part zu laden und dann anzuzeigen
    public void LoadPart(string partToLoadID)
    {
        // Als erstes wird das beötigte Story Part aus der Liste abgefragt
        StoryPart partToLoad = StoryParts[partToLoadID];
        // Dann wird der currentPart Eigenschaft von RunManager der partToLoad zugewiesen   
        GetComponent<RunManager>().currentPart = partToLoad;
        // Hier wird der angezeigte Text verändert
        GetComponent<StageManager>().UpdateText(partToLoad);
    }

    // Diese Funktion macht essentiell das selbe wie LoadPart(). Allerdings gibt man hier als Argument die Nummer
    // des ausgewählten Pfades ein. Die Funktion lädt dann aus dem currentPart die ID des zu ladedenden Story Parts  
    public void LoadSelectedPath(int selectedPath)
    {
        string roomToLoad = GetComponent<RunManager>().currentPart.abzweigungen[selectedPath].targetID;
        LoadPart(roomToLoad);
    }
}
