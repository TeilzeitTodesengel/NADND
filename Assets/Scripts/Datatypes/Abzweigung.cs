using System;

[Serializable]
public class Abzweigung
{
    public string description;
    public string targetID;
    public Item loot;

    public Abzweigung(string description, string targetID, Item loot)
    {
        this.description = description;
        this.targetID = targetID;
        this.loot = loot;
    }
}
