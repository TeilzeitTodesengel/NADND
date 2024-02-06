using System;

[Serializable]
public class Monster
{
    public string Name;
    public int Staerke;
    public int Wiederstandskraft;

    public Monster(string name, int staerke, int wiederstandskraft)
    {
        Name = name;
        Staerke = staerke;
        Wiederstandskraft = wiederstandskraft;

    }
}
