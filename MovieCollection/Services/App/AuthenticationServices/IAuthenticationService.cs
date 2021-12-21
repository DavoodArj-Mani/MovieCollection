using System;
using Microsoft.AspNetCore.Http;
using MovieCollection.Model.Core;
using MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity;

namespace MovieCollection.Services.App.AuthenticationServices
{
    public interface IAuthenticationService
    {
        User UserInfo(HttpRequest req);

        LoginRespViewEntity Login(LoginReqViewEntity loginReq);
    }
}
