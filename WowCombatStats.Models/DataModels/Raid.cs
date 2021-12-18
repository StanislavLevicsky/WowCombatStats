using WowCombatStats.Models.Enums;

namespace WowCombatStats.Models.DataModels;

public class Raid
{
    public int RaidId { get; set; }
    public int RaidTypeId { get; set; }
    public DateTime StartRaid { get; set; }
    public string ServerName { get; set; }
}