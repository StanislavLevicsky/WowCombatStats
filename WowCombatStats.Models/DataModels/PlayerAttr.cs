namespace WowCombatStats.Models.DataModels;

public class PlayerAttr
{
    public int PlayerAttrId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public int MeleeHit { get; set; }
    public int SpellHit { get; set; }
    public int MeleeCrit { get; set; }
    public int SpellCrit { get; set; }
    public int Armor { get; set; }
    public int Healt { get; set; }
    public int Stamina { get; set; }
    public int Agility { get; set; }
    public int Intellect { get; set; }
    public int Spirit { get; set; }
    public int Strength { get; set; }
    public int Mana { get; set; }
    public int ArcaneResistance { get; set; }
    public int FireResistance { get; set; }
    public int NatureResistance { get; set; }
    public int FrostResistance { get; set; }
    public int ShadowResistance { get; set; }
}