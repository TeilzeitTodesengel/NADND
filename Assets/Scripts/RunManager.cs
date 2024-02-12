using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RunManager : MonoBehaviour
{
    
    public StoryPart currentPart;
    public int health = 20;
    public List<Item> items = new List<Item>();
    public GameObject mainObject;

    public GameObject[] optionButtons = new GameObject[4] ;

    private void Awake()
    {
        getOptionButtons();
        Debug.Log("Run Manager Init Finished");
    }

    void Start()
    {
        DontDestroyOnLoad(GetComponent<RunManager>());
        GetComponent<StoryManager>().LoadPart("start");
    }

    void getOptionButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            optionButtons[i] = GameObject.Find("Option" + (i + 1));
        }
        
        optionButtons[0].GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<StoryManager>().LoadSelectedPath(0);
            });
        optionButtons[1].GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<StoryManager>().LoadSelectedPath(1);
            });
        optionButtons[2].GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<StoryManager>().LoadSelectedPath(2);
            });
        optionButtons[3].GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<StoryManager>().LoadSelectedPath(3);
            });
        
    }   

    
}
