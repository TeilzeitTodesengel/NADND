using System;

[Serializable]
public class Fight
{
    public Monster Monster;

    public Fight(Monster monster)
    {
        Monster = monster;
    }
}
