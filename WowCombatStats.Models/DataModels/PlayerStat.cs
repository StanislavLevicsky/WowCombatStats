namespace WowCombatStats.Models.DataModels;

public class PlayerStat
{
    public int PlayerStatId { get; set; }
    public int RaidId { get; set; }
    public int PlayerId { get; set; }
    public string SpellName { get; set; }
    public int PlayerStatTypeId { get; set; }
    public string Value { get; set; }
    public DateTime Time { get; set; }
}