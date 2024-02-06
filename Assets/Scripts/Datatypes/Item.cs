using System;

[Serializable]
public class Item
{
    public String Name;
    public String Description;
    public int Damage;

    public Item(String name, String description, int damage)
    {
        Name = name;
        Description = description;
        Damage = damage;
    }
}
