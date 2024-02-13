using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Der Stage Manager ist für die Verwaltung der Anzeigeelemente
public class StageManager : MonoBehaviour
{
    public GameObject mainObject;

    public TextMeshProUGUI mainText;

    // Hier wird der Story Manager Initialisiert 
    void Awake()
    {
        // Als erstes wird die Stage Manager Komponente als DontDestroy on load deklariert
        DontDestroyOnLoad(GetComponent<StageManager>());
        // Hier wird der Haupttext aus der Szene gesucht
        mainText = GameObject.Find("Placeholder").GetComponent<TextMeshProUGUI>();
        Debug.Log("Stage Manager Init finished");
    }

    // Diese Funktion aktualisiert den angezeigten Text
    public void UpdateText(StoryPart newContent)
    {
        // Als erstes  werden die Wegebuttons aus dem Runmanager abgefragt
        GameObject[] optionButtons = GetComponent<RunManager>().optionButtons;
        // Hier wird der Haupttext verändert
        mainText.SetText(newContent.description);
        // Der erste Schritt für die Option Buttons ist es alle auf aktiv zu setzen
        optionButtons[0].SetActive(true);
        optionButtons[1].SetActive(true);
        optionButtons[2].SetActive(true);
        optionButtons[3].SetActive(true);
        /*
         * In dem Switch wird dann anhand der Länge der Liste der Abzweigungen des anzuzeigenden Story Parts
         * die OptionButtons verändert. Dies läuft immer nach dem Gleichen Muster
         * es wird die Menge an vorhandenen Optionen angezeigt und dann der Rest der Knöpfe deativiert 
         */
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
