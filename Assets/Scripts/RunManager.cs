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
    public int startHealth = 20;
    public int healFactor = 1;
    public List<Item> items = new List<Item>();
    public GameObject mainObject;
    public Item equippedItem;

    [HideInInspector] public bool isCombat = false;

    public GameObject[] optionButtons = new GameObject[4];
    public Monster currentMonster;

    private int health;

    // Hier ist der erste Teil der Initialisierung des RunManagers.
    // Dazu werden die OptionButtons aus der Szene abgefragt und dann in ein Array gepackt. (getOptionButtons())
    private void Awake()
    {
        GetOptionButtons();
        Debug.Log("Run Manager Init Finished");
        health = startHealth;
    }

    // Im zweiten Teil der Initialisierung wird der RunManager als DontDestroyOnLoad geflagt und es wird
    // der Story Part mit der ID start geladen.
    void Start()
    {
        DontDestroyOnLoad(GetComponent<RunManager>());
        GetComponent<StoryManager>().LoadPart("start");
        Item startWeapon = new Item("Faust", "Deine Faust zu nichts gut", 0);
        AddItem(startWeapon);
        EquipItem(startWeapon);
    }

    private void Update()
    {
        // Es wird geprüft, wenn sich der Spieler im Kampf befindet, ob das Monster 0 oder weniger Leben hat.
        // Wenn das zutrifft ist der Kampf beendet, und der Sieg Button wird eingeblendet.
        if (isCombat)
        {
            if (currentMonster.Wiederstandskraft <= 0)
            {
                isCombat = false;
                GetComponent<StageManager>().SwitchCombatUI(true);
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
            delegate { GetComponent<StoryManager>().LoadSelectedPath(0); });
        optionButtons[1].GetComponent<Button>().onClick.AddListener(
            delegate { GetComponent<StoryManager>().LoadSelectedPath(1); });
        optionButtons[2].GetComponent<Button>().onClick.AddListener(
            delegate { GetComponent<StoryManager>().LoadSelectedPath(2); });
        optionButtons[3].GetComponent<Button>().onClick.AddListener(
            delegate { GetComponent<StoryManager>().LoadSelectedPath(3); });

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

    // Der normale Angriff mach den angegebenen Waffenschaden. Die Trefferchance ist 100%
    public void LightAttack()
    {
        currentMonster.Wiederstandskraft -= equippedItem.Damage;
        GetComponent<StageManager>().UpdateCombatLog("Du hast " + equippedItem.Damage + " Schaden gemacht.");
        MonsterAttack();
    }

    // Der schwere Angriff mach doppelten Schaden. Allerdings kann trifft diese nicht immer, die Chance wird momentan nur im Code
    // festgelegt. Wenn die ausgerüstete Waffe 0 Schaden macht, macht die Heavy Attack 1 Schaden.
    public void HeavyAttack()
    {
        Random rand = new Random();
        if (rand.NextDouble() < 0.5)
        {
            if (equippedItem.Damage == 0)
            {
                currentMonster.Wiederstandskraft -= 1;
                GetComponent<StageManager>().UpdateCombatLog("Du hast 1 Schaden gemacht.");
                MonsterAttack();

            }
            else
            {
                currentMonster.Wiederstandskraft -= equippedItem.Damage * 2;
                GetComponent<StageManager>()
                    .UpdateCombatLog("Du hast " + equippedItem.Damage * 2 + " Schaden gemacht.");
                MonsterAttack();
            }

        }
        else
        {
            GetComponent<StageManager>().UpdateCombatLog("Du hast daneben geschlagen.");
            MonsterAttack();
        }
    }

    // Macht den Schaden des Monsters an dem Spieler
    void MonsterAttack()
    {
        health -= currentMonster.Staerke;
        GetComponent<StageManager>()
            .UpdateCombatLog("Das Monster trifft dich für " + currentMonster.Staerke + " Schaden.");
        GetComponent<StageManager>().UpdateHealth();
        if (health <= 0)
        {
            Die();
        }
    }

    // Gibt das aktuelle Leben zurück
    public int GetHealth()
    {
        return health;
    }
    
    /* Heilt den Spieler um den Healfactor. Wenn das aktuelle Leben dadurch das maximale Leben übersteigt wird es 
       auf das Startleben gesetzt */
    public void Heal()
    {
        if (health < startHealth)
        {
            health += healFactor;    
        }

        if (health > startHealth)
        {
            health = startHealth;
        }
        
    }

    // Fügt der Item-Liste, welche das Inventar repräsentiert ein Item hinzu, wenn es noch nicht in der Liste vorhanden ist
    public void AddItem(Item itemToAdd)
    {
        if (!items.Contains(itemToAdd))
        {
            items.Add(itemToAdd);    
        }
    }

    // Rüstet ein Item aus, falls es im Inventar vorhanden ist
    public void EquipItem(Item itemToEquip)
    {
        if (items.Contains(itemToEquip))
        {
            equippedItem = itemToEquip;
        }
    }

    private void Die()
    {
        Debug.Log("You died");
    }
}