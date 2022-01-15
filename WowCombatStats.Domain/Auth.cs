using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowCombatStats.Data;
using WowCombatStats.Models.VIewModels;
using System.Security.Cryptography;
using WowCombatStats.Models.DataModels;
using System.Security.Claims;

namespace WowCombatStats.Domain
{
    public class Auth
    {
        private readonly IUow _uow;

        public Auth(IUow uow)
        {
            _uow = uow;
        }

        public bool Login(LoginVM loginVM)
        {
            var user = _uow.UserRepository.Get(u => u.UserName == loginVM.Login);

            if (user != null)
            {
                if (VerifyHash(MD5.Create(), loginVM.Password, user.HashPassword))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Registration(UserRegistrationVM userRegistrationVM)
        {
            var userIsExist = _uow.UserRepository.Get(u => u.UserName == userRegistrationVM.Login);

            if (userIsExist == null)
            {
                _uow.UserRepository.Add(new User
                {
                    UserName = userRegistrationVM.Login,
                    HashPassword = GetHash(MD5.Create(), userRegistrationVM.Password),
                    Email = userRegistrationVM.Email
                });
                _uow.SaveChange();

                return true;
            }

            return false;
        }

        public ClaimsIdentity GetIdentity(string userName)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userName == "Admin" ? "Admin" : "User")
                };

            return new ClaimsIdentity(claims,
                                      "ApplicationCookie",
                                      ClaimsIdentity.DefaultNameClaimType,
                                      ClaimsIdentity.DefaultRoleClaimType);
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfInput = GetHash(hashAlgorithm, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
