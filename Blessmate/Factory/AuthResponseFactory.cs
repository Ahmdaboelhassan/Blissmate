using Blessmate.Models;
using Blessmate.Records;

namespace Blessmate.Factory
{
    public static class AuthResponseFactory
    {
        public static AuthResponse SuccessAuthResponse(ApplicationUser user , (string key, DateTime expireOn) token){
            return new AuthResponse(){
                 id = user.Id,
                email = user.Email,
                firstname = user.FirstName,
                lastname = user.LastName,
                token = token.key,
                expireOn = token.expireOn,
                isAuth =  true,
                isEmailConfirmed = user.EmailConfirmed,
                messages = "User Authenticate Successfully",
            };
        }
    }
}