using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.UserServices;
using MovieCollection.Shared;
using MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity;

namespace MovieCollection.Services.App.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        public IUserService _userService;

        public AuthenticationService( IUserService userService)
        {
            _userService = userService;
        }

        public User UserInfo(HttpRequest req)
        {
            string accessTokenWithBearerPrefix = req.Headers[HeaderNames.Authorization];
            string accessToken = accessTokenWithBearerPrefix.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken);
            var tokenS = jsonToken as JwtSecurityToken;

            var username = tokenS.Claims.First(claim => claim.Type == "username").Value;

            return _userService.QueryUserByName(username);

        }

        public LoginRespViewEntity Login(LoginReqViewEntity loginReq)
        {
            var authenticatedUser = _userService.login(loginReq.UserName, loginReq.Password);
            if (authenticatedUser == null)
            {
                return null;
            }
            var userRoles = _userService.QueryUserRoles(authenticatedUser.UserId);

            var tokenExpiryTimestamp = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECUTIRY_KEY);
            var securityTokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", loginReq.UserName),
                }),
                Expires = tokenExpiryTimestamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            
            foreach (var role in userRoles)
            {
                securityTokenDiscriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var secutiryToken = jwtSecurityTokenHandler.CreateToken(securityTokenDiscriptor);
            var token = jwtSecurityTokenHandler.WriteToken(secutiryToken);

            return new LoginRespViewEntity
            {
                token = token,
                user_name = loginReq.UserName,
                expires_in = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}

