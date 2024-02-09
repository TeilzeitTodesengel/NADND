using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject mainObject;
    public TextMeshProUGUI mainText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateText(StoryPart newContent)
    { 
        GameObject[] optionButtons = GetComponent<RunManager>().optionButtons;
        mainText.SetText(newContent.description);
        optionButtons[0].SetActive(true);
        optionButtons[1].SetActive(true);
        optionButtons[2].SetActive(true);
        optionButtons[3].SetActive(true);
        switch (newContent.abzweigungen.Length)
        {
            case 1:
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[0].description;
                optionButtons[0].SetActive(false);
                optionButtons[0].SetActive(false);
                optionButtons[0].SetActive(false);
                break;
            case 2:
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[0].description;
                optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[1].description;
                optionButtons[2].SetActive(false);
                optionButtons[3].SetActive(false);
                break;
            case 3:
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[0].description;
                optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[1].description;
                optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[2].description;
                optionButtons[3].SetActive(false);
                break;
            case 4:
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[0].description;
                optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[1].description;
                optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[2].description;
                optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = newContent.abzweigungen[3].description;
                break;
            default:
                optionButtons[0].SetActive(false);
                optionButtons[1].SetActive(false);
                optionButtons[2].SetActive(false);
                optionButtons[3].SetActive(false);
                break;
        }
    }
}
