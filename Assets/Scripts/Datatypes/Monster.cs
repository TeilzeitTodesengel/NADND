using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster
{
    public string Name { get; set; }
    public int Staerke { get; set; }
    public int Wiederstandskraft { get; set; }

    public Monster(string name, int staerke, int wiederstandskraft)
    {
        Name = name;
        Staerke = staerke;
        Wiederstandskraft = wiederstandskraft;

    }
}
