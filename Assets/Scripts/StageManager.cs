using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        GameObject[] abzweigungen = GetComponent<RunManager>().optionButtons;
        mainText.SetText(newContent.description);
        abzweigungen[0].SetActive(true);
        abzweigungen[1].SetActive(true);
        abzweigungen[2].SetActive(true);
        abzweigungen[3].SetActive(true);
        switch (newContent.abzweigungen.Length)
        {
            case 0:
                abzweigungen[0].SetActive(false);
                abzweigungen[1].SetActive(false);
                abzweigungen[2].SetActive(false);
                abzweigungen[3].SetActive(false);
                break;
            case 1:
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                break;
            case 2:
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                break;
            case 3:
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                break;
            case 4:
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                abzweigungen[0].SetActive(false);
                break;
        }
    }
}
