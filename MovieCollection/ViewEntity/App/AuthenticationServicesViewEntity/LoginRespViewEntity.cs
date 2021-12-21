using System;
namespace MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity
{
    [Serializable]
    public class LoginRespViewEntity
    {
        public string token { get; set; }
        public string user_name { get; set; }
        public int expires_in { get; set; }
    }
}
