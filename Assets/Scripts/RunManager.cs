using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/* In dem Run Manager werden die für den aktuellen Run wichtigen Informationen
 * gespeichert und verwaltet. Hier muss noch viel Programmiert werden. Wie z.B. die Kampffunktionalität oder
 * das Inventar.
 */
public class RunManager : MonoBehaviour
{
    
    public StoryPart currentPart;
    public int health = 20;
    public List<Item> items = new List<Item>();
    public GameObject mainObject;
    public Item equippedItem = new Item("Faust", "Deine Faust zu nichts gut", 0);

    [HideInInspector]
    public bool isCombat = false;
    
    public GameObject[] optionButtons = new GameObject[4] ;

    
    public Monster currentMonster;

    // Hier ist der erste Teil der Initialisierung des RunManagers.
    // Dazu werden die OptionButtons aus der Szene abgefragt und dann in ein Array gepackt. (getOptionButtons())
    private void Awake()
    {
        GetOptionButtons();
        Debug.Log("Run Manager Init Finished");
    }

    // Im zweiten Teil der Initialisierung wird der RunManager als DontDestroyOnLoad geflagt und es wird
    // der Story Part mit der ID start geladen.
    void Start()
    {
        DontDestroyOnLoad(GetComponent<RunManager>());
        GetComponent<StoryManager>().LoadPart("start");
    }

    private void Update()
    {
        if (isCombat)
        {
            if (currentMonster.Wiederstandskraft <= 0)
            {
                isCombat = false;
                GetComponent<StageManager>().SwitchCombatUI();
            }
        }
    }

    // Diese Funktion lädt die Option Buttons aus der Szene
    void GetOptionButtons()
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

    // Startet einen Kampf
    public void StartCombat()
    {
        isCombat = true;
        // "Normales" gegen KampfUI wechseln
        GetComponent<StageManager>().SwitchUI();
        currentMonster = currentPart.fight.Monster;
        GetComponent<StageManager>().InitializeCombatUI(equippedItem.Name, currentMonster.Name);
    }

    public void LightAttack()
    {
        currentMonster.Wiederstandskraft -= equippedItem.Damage;
        GetComponent<StageManager>().UpdateCombatLog("Du hast " + equippedItem.Damage + " Schaden gemacht.");
        MonsterAttack();
    }
    
    public void HeavyAttack()
    {
        Random rand = new Random();
        if (rand.NextDouble() < 0.5)
        {
            if (equippedItem.Damage == 0)
            {
                currentMonster.Wiederstandskraft -= 1;
                GetComponent<StageManager>().UpdateCombatLog("Du hast 1 Schaden gemacht.");

            }
            else
            {
                currentMonster.Wiederstandskraft -= equippedItem.Damage * 2;
                GetComponent<StageManager>().UpdateCombatLog("Du hast " + equippedItem.Damage * 2 + " Schaden gemacht.");
                MonsterAttack();
            }
            
        }
        else
        {
            GetComponent<StageManager>().UpdateCombatLog("Du hast daneben geschlagen.");
            MonsterAttack();
        }
    }

    void MonsterAttack()
    {
        health -= currentMonster.Staerke;
        GetComponent<StageManager>().UpdateCombatLog("Das Monster trifft dich für " + currentMonster.Staerke + " Schaden.");
        GetComponent<StageManager>().UpdateHealth();
    }
}
