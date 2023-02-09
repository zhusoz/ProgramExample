using ProgramsNetCore.Common;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ProgramsNetCore.Common.JWT
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModel model)
        {
            string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
            string aud = Appsettings.app(new string[] { "Audience", "Audience" });
            string secret = AppSecretConfig.Audience_Secret_String;
            var claims = new List<Claim>
                {
                 /*
                 * 特别重要：
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                   2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                 */

                    
                new Claim("Name",model.UName),
              //  new Claim(JwtRegisteredClaimNames.Sub,model.Work),
                new Claim(JwtRegisteredClaimNames.Jti, model.UId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),
                new Claim("Permission",model.Permissions??""),//TestByZSZ
                new Claim(ClaimTypes.Role,model.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
               };
            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            //claims.AddRange(model.Role.ToString().Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            //       MyMemoryCache.AddMemoryCache(encodedJwt, model, TimeSpan.FromSeconds(1000),TimeSpan.FromSeconds(1000));
            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            TokenModel tokenModelJwt = new TokenModel();

            // token校验
            if (!string.IsNullOrEmpty(jwtStr) && jwtHandler.CanReadToken(jwtStr))
            {

                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

                object role;
                object permissionList;
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
             //   jwtToken.Payload.TryGetValue(JwtRegisteredClaimNames.Sub, out depart);
                jwtToken.Payload.TryGetValue("Permission", out permissionList);
                tokenModelJwt = new TokenModel
                {
                    UId = (jwtToken.Id).ToInt32(),
                    Role = role.ToString(),
                  //  Work = depart.ToString(),
                    Permissions = permissionList.ToString()
                };
            }
            return tokenModelJwt;
        }

    }

}
