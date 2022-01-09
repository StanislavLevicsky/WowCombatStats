namespace WowCombatStats.Models.DataModels;

public class Player
{
    public int PlayerId { get; set; }
    public PlayerAttr PlayerAttr { get; set; }
    public string PlayerName { get; set; }
    public int PlayerLvl { get; set; }
    public int ClassTypeId { get; set; }
    public int RaceTypeId { get; set; }
    public int GenderTypeId { get; set; }
    public int GuildId { get; set; }
    public string GuildRank { get; set; }
    public int LastGearId { get; set; }
    public int FactionId { get; set; }
}