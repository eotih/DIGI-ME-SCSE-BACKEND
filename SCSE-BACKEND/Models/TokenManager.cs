using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class TokenManager
    {
        private SCSE_DBEntities db = new SCSE_DBEntities();
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        public static string GenerateToken(string Username, string Rolename, string Fullname, string Password, string Email, int IdState, int IDUser)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.NameIdentifier, IdState.ToString()),
                      new Claim(ClaimTypes.NameIdentifier, Fullname),
                      new Claim(ClaimTypes.NameIdentifier, Rolename),
                      new Claim(ClaimTypes.NameIdentifier, Username),
                      new Claim(ClaimTypes.NameIdentifier, Password),
                      new Claim(ClaimTypes.NameIdentifier, Email),
                      new Claim(ClaimTypes.NameIdentifier, IDUser.ToString())

                  }),
                Expires = DateTime.UtcNow.AddMinutes(240),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
        public static string ValidateCheck()
        {
            // Get Auth header
            var handler = new JwtSecurityTokenHandler();
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var claims = tokenS.Claims.ToList();
                // Dòng này để check trong database
                //string accountID = claims[6].Value;
                //var checkDatabase = db.Accounts.Where(x => x.IDUser == Convert.ToInt32(accountID)).FirstOrDefault();
                string getData = claims[2].Value;
                if(getData != "Admin")
                    return "Access Denied";
                else
                    return "OK";
            }   
            else
            {
                return "Access Denied";
            }
        }
    }
}