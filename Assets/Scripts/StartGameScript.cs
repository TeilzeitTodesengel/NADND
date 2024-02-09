using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCo());
    }

    IEnumerator StartGameCo()
    {
        SceneManager.LoadScene("Save1");
        yield return 0;
        Debug.Log("After Wait");
        GetComponent<RunManager>().currentPart = GetComponent<StoryManager>().StoryParts["start"];
        GameObject displayText = GameObject.Find("Placeholder");
        GetComponent<StageManager>().mainText = displayText.GetComponent<TextMeshProUGUI>();
        GetComponent<StageManager>().UpdateText(GetComponent<RunManager>().currentPart);
        GetComponent<RunManager>().getOptionButtons();
    }
}
