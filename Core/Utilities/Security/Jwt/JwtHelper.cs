using Core.Entities.Conceretes;
using Core.EXtensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration configuration { get; }

        private TokenOptions? tokenOptions;
        private DateTime accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
            tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateAccessToken(User user, List<OperationClaim> operationClaims)
        {
            var secuirtyKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(secuirtyKey);
            accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
            var jwt = CreateJwtSecurityToken(user, operationClaims, tokenOptions, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(User user, List<OperationClaim> operationClaims,
            TokenOptions tokenOptions, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:accessTokenExpiration,
                claims:SetClaims(user,operationClaims),
                notBefore:DateTime.Now,
                signingCredentials:signingCredentials);

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName}{user.LastName}");

            claims.AddEmail(user.Email);

            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}