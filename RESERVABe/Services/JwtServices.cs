using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RESERVABe.Models;

public class JwtServices
{
    public static ResponseLogin? GenTokenKey(ResponseLogin userToken, JwtSettingsDto jwtSetting)
    {
        try
        {
            if (userToken == null) throw new ArgumentException(nameof(userToken));
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSetting.IssuerSingingKey);
            DateTime ExpireTime = DateTime.Now;

            if (jwtSetting.FlagExpirationTimeHours)
            {
                ExpireTime = DateTime.Now.AddHours(jwtSetting.ExpirationTimeHours);
            }
            else
            {
                if (jwtSetting.FlagExpirationTimeMinutes)
                {
                    ExpireTime = DateTime.Now.AddMinutes(jwtSetting.ExpirationTimeMinutes);
                }
                else
                {
                    return null;
                }
            }

            IEnumerable<Claim> claims = new Claim[] {
                new Claim("TimeExpiration",ExpireTime.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            var JWToken = new JwtSecurityToken(
                issuer: jwtSetting.ValidIssuer,
                audience: jwtSetting.ValidAudience,
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(ExpireTime).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

            userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            userToken.TiempoExpiracion = ExpireTime;

            return userToken;
        }
        catch (Exception)
        {
            return null;
        }
    }
}