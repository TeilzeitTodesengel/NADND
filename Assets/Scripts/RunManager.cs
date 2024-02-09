using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunManager : MonoBehaviour
{
    public StoryPart currentPart;
    public int health = 20;
    public List<Item> items = new List<Item>();
    public GameObject mainObject;

    public GameObject[] optionButtons = new GameObject[4] ;
    void Start()
    {
    }

    public void getOptionButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            optionButtons[i] = GameObject.Find("Option" + (i + 1));
            optionButtons[i].GetComponent<Button>().onClick.AddListener(LoadFirstPart);
        }
        
    }   

    public void LoadFirstPart()
    {
        currentPart = GetComponent<StoryManager>().StoryParts[currentPart.abzweigungen[0].targetID];
    }
}
