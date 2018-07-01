using MessagePack;

[MessagePackObject]
public class MonsterEntity
{
    [Key("monsterId")]
    public int monsterId { get; set; }
    [Key("masterId")]
    public int masterId { get; set; }
    [Key("nickname")]
    public string nickname { get; set; }
    [Key("element")]
    public string element { get; set; }
    [Key("hp")]
    public int hp { get; set; }
    [Key("attack")]
    public int attack { get; set; }
    [Key("defence")]
    public int defence { get; set; }
    [Key("dodge")]
    public int dodge { get; set; }
    [Key("critical")]
    public int critical { get; set; }
}
