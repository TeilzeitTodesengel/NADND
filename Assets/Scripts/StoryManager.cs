using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Der Story Manager ist dafür da aus den json Datein für das Spiel verwertbare
 * Objekte zu machen und diese zu verwalten.
 */

public class StoryManager : MonoBehaviour
{
    public Dictionary<String, StoryPart> StoryParts = new Dictionary<String, StoryPart>();
    public GameObject mainObject;
    public Dictionary<String, AudioClip> audioClips =  new Dictionary<String, AudioClip>();

    public AudioClip startSound;
    // Hier wird der Story Manager Initialisiert 
    private void Awake()
    {
        // Als erstes wird die Story Manager Komponente als DontDestroy on load deklariert
        DontDestroyOnLoad(GetComponent<StoryManager>());
        
        // Als erstes wird das die Liste der Story Parts & audioClips gelöscht
        StoryParts.Clear();
        audioClips.Clear();
        
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
        /* Das gesammmte Laden von Audiodatein wird über Subroutinen gehandhabt. Diese lädt
         über das UnityWebRequests Modul die Datein über eine Anfrage an das lokale Datei-
         system die Datei und verarbeitet sie zu einem AudioClip Objekt*/
        
        // Schleife um MP3 Dateien von der Festplatte zu laden
        foreach (string filePath in  Directory.GetFiles(streamingAssetPath + "/Music").Where(filename => filename.EndsWith(".mp3")))
        {
            string fileName = filePath.Split(".")[0].Split("\\").Last();
            StartCoroutine(LoadMP3File(filePath, fileName));
        }
        
        // Schleife um WAV Dateien von der Festplatte zu laden
        foreach (string filePath in  Directory.GetFiles(streamingAssetPath + "/Music").Where(filename => filename.EndsWith(".wav")))
        {
            string fileName = filePath.Split(".")[0].Split("\\").Last();
            StartCoroutine(LoadWAVFile(filePath, fileName));
        }
        
        // Schleife um WAV Dateien von der Festplatte zu laden
        foreach (string filePath in  Directory.GetFiles(streamingAssetPath + "/Music").Where(filename => filename.EndsWith(".ogg")))
        {
            string fileName = filePath.Split(".")[0].Split("\\").Last();
            StartCoroutine(LoadOGGFile(filePath, fileName));
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
        // Hier wird die passende Musik geladen und abgespielt
        if (partToLoad.roomName == "start")
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = startSound;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = audioClips[partToLoad.musicID];
            GetComponent<AudioSource>().Play();
        }
        

    }

    // Diese Funktion macht essentiell das selbe wie LoadPart(). Allerdings gibt man hier als Argument die Nummer
    // des ausgewählten Pfades ein. Die Funktion lädt dann aus dem currentPart die ID des zu ladedenden Story Parts  
    public void LoadSelectedPath(int selectedPath)
    {
        string roomToLoad = GetComponent<RunManager>().currentPart.abzweigungen[selectedPath].targetID;
        LoadPart(roomToLoad);
    }

    private IEnumerator LoadMP3File(string path, string fileName)
    {
        // Macht aus dem Dateipafd eine URI an das lokale Dateisystem
        string fullPath = "file://" + path;
        // Hier wird die WebRequest erstellt. Diese lädt ein AudioClip
        UnityWebRequest url = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.MPEG);
        // Es wird gewartet, bis die Datei ganz geladen ist
        yield return url.SendWebRequest();
        // Aus dem Request Content wird das AudioClip Objekt erzeugt
        AudioClip clip = DownloadHandlerAudioClip.GetContent(url);
        // Der Clip wird dem audioClips Dictionary hinzugefügt
        audioClips.Add(fileName, clip);
    }
    
    private IEnumerator LoadWAVFile(string path, string fileName)
    {
        // Macht aus dem Dateipafd eine URI an das lokale Dateisystem
        string fullPath = "file://" + path;
        // Hier wird die WebRequest erstellt. Diese lädt ein AudioClip
        UnityWebRequest url = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.WAV);
        // Es wird gewartet, bis die Datei ganz geladen ist
        yield return url.SendWebRequest();
        // Aus dem Request Content wird das AudioClip Objekt erzeugt
        AudioClip clip = DownloadHandlerAudioClip.GetContent(url);
        // Der Clip wird dem audioClips Dictionary hinzugefügt
        audioClips.Add(fileName, clip);
    }
    
    private IEnumerator LoadOGGFile(string path, string fileName)
    {
        // Macht aus dem Dateipafd eine URI an das lokale Dateisystem
        string fullPath = "file://" + path;
        // Hier wird die WebRequest erstellt. Diese lädt ein AudioClip
        UnityWebRequest url = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.OGGVORBIS);
        // Es wird gewartet, bis die Datei ganz geladen ist
        yield return url.SendWebRequest();
        // Aus dem Request Content wird das AudioClip Objekt erzeugt
        AudioClip clip = DownloadHandlerAudioClip.GetContent(url);
        // Der Clip wird dem audioClips Dictionary hinzugefügt
        audioClips.Add(fileName, clip);
    }
}
