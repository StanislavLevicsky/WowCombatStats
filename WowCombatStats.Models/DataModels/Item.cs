using WowCombatStats.Models.Enums;

namespace WowCombatStats.Models.DataModels;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int ItemTypeId { get; set; }
    public int RarityTypeId { get; set; }
    public string ItemCode { get; set; }
}