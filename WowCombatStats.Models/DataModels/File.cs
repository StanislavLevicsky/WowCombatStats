namespace WowCombatStats.Models.DataModels;

public class File
{
    public int FileId { get; set; }
    public byte[] Data { get; set; }
    public int UserId { get; set; }
    public bool IsParse { get; set; }
    public DateTime UploadTime { get; set; }
}