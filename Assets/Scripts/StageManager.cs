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
        mainText.SetText(newContent.description);
    }
}
