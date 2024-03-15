using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// Der Stage Manager ist für die Verwaltung der Anzeigeelemente
public class StageManager : MonoBehaviour
{
    public GameObject mainObject;

    public TextMeshProUGUI mainText;

    public Canvas combat;
    public Canvas story;
    public TextMeshProUGUI combatLog;
    public TextMeshProUGUI equippedItemText;
    public TextMeshProUGUI monsterNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TMP_Dropdown itemSelector;
    public GameObject invDisplay;
    public Button lightATK;
    public Button heavyATK;
    public Button combatWon;
    public Button openINVButton;
    public Image monsterImage;
    private int combatLogLength = 0;

    private List<String> combatLogList = new List<string>();
    // Hier wird der Story Manager Initialisiert 
    void Awake()
    {
        // Als erstes wird die Stage Manager Komponente als DontDestroy on load deklariert
        DontDestroyOnLoad(GetComponent<StageManager>());
        // Hier wird der Haupttext aus der Szene gesucht
        mainText = GameObject.Find("Placeholder").GetComponent<TextMeshProUGUI>();
        Debug.Log("Stage Manager Init finished");
        // Die Funktionszuweisungen für die einzelnen Buttons im KampfUI
        combatWon.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                SwitchUI();
                UpdateText(GetComponent<RunManager>().currentPart);
            });
        lightATK.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<RunManager>().LightAttack();
            });
        heavyATK.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                GetComponent<RunManager>().HeavyAttack();
            });
        
        openINVButton.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                ToggleInv();
            }
            );
        itemSelector.onValueChanged.AddListener( call: indexSelected =>
        {
            GetComponent<RunManager>().equippedItem = GetComponent<RunManager>().items[indexSelected];
            DisplayEquippedItem();
        });
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

    // Diese Funktion hängt an den Combat Log den angegeben Text an, wenn die maximalen Länge von 9 dadurch überschritten
    // wird, wird der älteste Eintrag ersetzt
    public void UpdateCombatLog(string textToAdd)
    {
        StringBuilder combatLogStringBuilder = new StringBuilder();

        if (combatLogLength < 9)
        {
            combatLogList.Add(textToAdd);
            combatLogLength = combatLogLength + 1;
        }
        else
        {
            combatLogList.RemoveAt(0);
            combatLogList.Add(textToAdd);
        }

        foreach (string s in combatLogList)
        {
            combatLogStringBuilder.Append(s + "\n");
        }
        combatLog.text = combatLogStringBuilder.ToString();
    }

    // Wechselt zwischen dem normalen und dem Kampf UI
    public void SwitchUI()
    {
        if (GetComponent<RunManager>().isCombat)
        {
            story.gameObject.SetActive(false);
            combat.gameObject.SetActive(true);
        }
        else
        {
            combat.gameObject.SetActive(false);
            story.gameObject.SetActive(true);
        }
    }

    // Verändert die Buttons im Combat so, dass der Sieg Button angezeigt wird.
    public void SwitchCombatUI(bool isWon)
    {
        if (isWon)
        {
            lightATK.gameObject.SetActive(false);
            heavyATK.gameObject.SetActive(false);
            combatWon.gameObject.SetActive(true);
        }
        else
        {
            lightATK.gameObject.SetActive(true);
            heavyATK.gameObject.SetActive(true);
            combatWon.gameObject.SetActive(false);
        }
        
    }

    // Initialisiert das UI für den Kampf, indem der Combat Log geleert wird, das ausgerüstete Item angezeigt wird, der
    // Monstername angezeigt wird und das Monsterbild angezeigt wird
    public void InitializeCombatUI(string equippedItem, string monsterName)
    {
        equippedItemText.text = equippedItem;
        monsterNameText.text = monsterName;
        combatLogList.Clear();
        combatLog.text = "";
        combatLogLength = 0;
        SwitchCombatUI(false);
        UpdateHealth();
        StartCoroutine(LoadMonsterImage(monsterName));
    }

    // Updatet den Lebenstext im Kampf
    public void UpdateHealth()
    {
        healthText.text = "Leben: " + GetComponent<RunManager>().GetHealth();
    }

    // Diese Coroutine lädt das Bild des Monsters. Der Dateiname muss gleich dem Monsternamen sein. Es wird nur png unterstützt.
    private IEnumerator LoadMonsterImage(string monsterToLoad)
    {
        byte[] imageData = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + "/MonsterImages/" + monsterToLoad + ".png");
        yield return 0;
        Texture2D monsterTex = new Texture2D(2, 2);
        monsterTex.LoadImage(imageData);
        Sprite monsterSprite = Sprite.Create(
            monsterTex, // Textur, die aus der Datei geladen wurde
            new Rect(0.0f, 0.0f, monsterTex.width, monsterTex.height), // Grenzrechteck der Textur. Hat die Höhe und Weite der geladenen Textur
            new Vector2(0.5f, 0.5f), // Ankerpunkt der Textur relativ zur Ecke oben Links
            100.0f); // Ist für Unity für das Texturscaling
        monsterImage.sprite = monsterSprite;
    }


    private void OnItemSelected()
    {
        
    }

    private void ToggleInv()
    {
        if (invDisplay.activeSelf)
        {
            invDisplay.SetActive(false);
        } else {
            invDisplay.SetActive(true);
            UpdateItemSelector();
            DisplayEquippedItem();
        }
    }

    private void UpdateItemSelector()
    {
        int loopCounter = 0;
        itemSelector.options.Clear();

        foreach (Item item in GetComponent<RunManager>().items)
        {
            itemSelector.options.Add(new TMP_Dropdown.OptionData(item.Name));
            if (GetComponent<RunManager>().equippedItem.Name == item.Name)
            {
                itemSelector.value = loopCounter;
            }

            loopCounter++;
        }
    }

    private void DisplayEquippedItem()
    {
        itemName.text = GetComponent<RunManager>().equippedItem.Name;
        itemDescription.text = "Description: " + GetComponent<RunManager>().equippedItem.Description;
    }
}
