using System;

[Serializable]
public class StoryPart
{
    public String roomID;
    public String roomName;
    public String description;
    public bool isFight;
    public Fight fight;
    public Abzweigung[] abzweigungen;

    public StoryPart(string roomID, string roomName, string description, bool isFight, Fight fight, Abzweigung[] abzweigungen)
    {
        this.roomID = roomID;
        this.roomName = roomName;
        this.description = description;
        this.isFight = isFight;
        this.fight = fight;
        this.abzweigungen = abzweigungen;
    }
}
