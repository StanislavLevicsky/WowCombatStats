using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowCombatStats.Data;
using WowCombatStats.Models.DataModels;

namespace WowCombatStats.Domain
{
    public class AccountBuilder
    {
        private readonly IUow _uow;

        public AccountBuilder(IUow uow)
        {
            _uow = uow;
        }

        public SelectList GetServerList()
        {
            var servers = _uow.ServerRepository.GetAll().ToDictionary(k => k.ServerId, v => v.ServerName);
            return new SelectList(servers, "Key", "Value");
        }

        public async Task<bool> SaveFile(byte[] data, Guid userToken, Guid fileName)
        {
            using (var tran = _uow.BeginTransaction())
            {
                try
                {
                    var user = _uow.UserRepository.Get(u => u.Token == userToken);
                    await _uow.FileRepository.AddAsync(new Models.DataModels.File
                    {
                        Data = data,
                        FileName = fileName,
                        UserId = user.UserId,
                        IsParse = false,
                        UploadTime = DateTime.UtcNow,
                    });

                    await _uow.SaveChangeAsync();
                    await tran.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public string GetPath(Guid fileName)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UploadFile"));
            //var fileData = _uow.FileRepository.Get(f => f.FileId == 2).Data;
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UploadFile", fileName.ToString());
            //System.IO.File.WriteAllBytes(path, fileData);
        }
    }
}
