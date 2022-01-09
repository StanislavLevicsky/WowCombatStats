using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WowCombatStats.Data;
using WowCombatStats.Models.DataModels;
using WowCombatStats.Models.Enums;

namespace WowCombatStats.Domain
{
    public static class DataSeeder
    {
        public static void InitData(DataContext context)
        {
            var classTypeInfo = Enum.GetValues(typeof(ClassTypeInfo)).Cast<ClassTypeInfo>().ToList();
            List<ClassType> classTypes = classTypeInfo.Select(ct => new ClassType { ClassTypeId = (int)ct, ClassName = ct.ToString()}).ToList();            
            
            var factionTypeInfo = Enum.GetValues(typeof(FactionTypeInfo)).Cast<FactionTypeInfo>().ToList();
            List<FactionType> factionTypes = factionTypeInfo.Select(ft => new FactionType { FactionTypeId = (int)ft, FactionName = ft.ToString() }).ToList();   
            
            var genderTypeInfo = Enum.GetValues(typeof(GenderTypeInfo)).Cast<GenderTypeInfo>().ToList();
            List<GenderType> genderTypes = genderTypeInfo.Select(gt => new GenderType { GenderTypeId = (int)gt, GenderName = gt.ToString() }).ToList();
            
            var itemTypeInfo = Enum.GetValues(typeof(ItemTypeInfo)).Cast<ItemTypeInfo>().ToList();
            List<ItemType> itemTypes = itemTypeInfo.Select(it => new ItemType { ItemTypeId = (int)it, ItemName = it.ToString() }).ToList();    
            
            var playerStatTypeInfo = Enum.GetValues(typeof(PlayerStatTypeInfo)).Cast<PlayerStatTypeInfo>().ToList();
            List<PlayerStatType> playerStatTypes = playerStatTypeInfo.Select(pst => new PlayerStatType { PlayerStatTypeId = (int)pst, PlayerStatName = pst.ToString() }).ToList();  
            
            var raceTypeInfo = Enum.GetValues(typeof(RaceTypeInfo)).Cast<RaceTypeInfo>().ToList();
            List<RaceType> raceTypes = raceTypeInfo.Select(rt => new RaceType { RaceTypeId = (int)rt, RaceName = rt.ToString() }).ToList(); 
            
            var raidTypeInfo = Enum.GetValues(typeof(RaidTypeInfo)).Cast<RaidTypeInfo>().ToList();
            List<RaidType> raidTypes = raidTypeInfo.Select(rt => new RaidType { RaidTypeId = (int)rt, RaidName = rt.ToString() }).ToList();  
            
            var rarityTypeInfo = Enum.GetValues(typeof(RarityTypeInfo)).Cast<RarityTypeInfo>().ToList();
            List<RarityType> rarityTypes = rarityTypeInfo.Select(rt => new RarityType { RarityTypeId = (int)rt, RarityName = rt.ToString() }).ToList();  
            
            var versionTypeInfo = Enum.GetValues(typeof(VersionTypeInfo)).Cast<VersionTypeInfo>().ToList();
            List<VersionType> versionTypes = versionTypeInfo.Select(vt => new VersionType { VersionTypeId = (int)vt, VersionName = vt.ToString() }).ToList();

            foreach (var version in versionTypes)
            {
                switch (version.VersionName)
                {
                    case "Unknown":
                        version.VersionNumber = "Unknown";
                        break;
                    case "Vanilla":
                        version.VersionNumber = "1.12.1";
                        break;
                    default:
                        break;
                }
            }

            using (var tran = context.Database.BeginTransaction())
            {
                context.AddRange(classTypes);
                context.AddRange(factionTypes);
                context.AddRange(genderTypes);
                context.AddRange(itemTypes);
                context.AddRange(playerStatTypes);
                context.AddRange(raidTypes);
                context.AddRange(rarityTypes);
                context.AddRange(versionTypes);

                context.SaveChanges();
                tran.Commit();
            }
        }
    }
}
