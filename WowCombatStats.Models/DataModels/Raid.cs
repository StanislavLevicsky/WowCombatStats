using WowCombatStats.Models.Enums;

namespace WowCombatStats.Models.DataModels;

public class Raid
{
    public int RaidId { get; set; }
    public int RaidTypeId { get; set; }
    public DateTime StartRaid { get; set; }
    public DateTime EndRaid { get; set; }
    public int ServerId { get; set; }
}