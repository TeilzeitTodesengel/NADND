using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject[] optionButtons = new GameObject[4] ;

    // Hier ist der erste Teil der Initialisierung des RunManagers.
    // Dazu werden die OptionButtons aus der Szene abgefragt und dann in ein Array gepackt. (getOptionButtons())
    private void Awake()
    {
        getOptionButtons();
        Debug.Log("Run Manager Init Finished");
    }

    // Im zweiten Teil der Initialisierung wird der RunManager als DontDestroyOnLoad geflagt und es wird
    // der Story Part mit der ID start geladen.
    void Start()
    {
        DontDestroyOnLoad(GetComponent<RunManager>());
        GetComponent<StoryManager>().LoadPart("start");
    }

    // Diese Funktion lädt die Option Buttons aus der Szene
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
