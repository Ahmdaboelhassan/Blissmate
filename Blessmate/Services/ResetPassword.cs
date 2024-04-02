using System.Text;
using Blessmate.DTOs;
using Blessmate.Models;
using Blessmate.Records;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Blessmate.Services.IServices
{
    public class ResetPassword : IResetPassword
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPassword(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ManageResponse> ChangePassword(ChanagePasswordModel model)
        { var user = await _userManager.FindByIdAsync(model.id.ToString());

           if (user is null)
                 return new ManageResponse("User is not exists");

          var result =  await _userManager.ChangePasswordAsync(user,model.oldPassword,model.newPassword);
          if (!result.Succeeded)
                 return new ManageResponse("Something went wrong");

          return new ManageResponse("Changed Successfully", true);
        }

        public async Task<ManageResponse> GetPasswordResetToken(int id)
        {
           var user = await _userManager.FindByIdAsync(id.ToString());

           if (user is null)
                 return new ManageResponse("User is not exists");
            
            var result = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            if (string.IsNullOrEmpty(result))
                  return new ManageResponse("Something went wrong");
            
            result = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(result));

            return new ManageResponse("Generated Successfully", true, result);
        }

        public async Task<ManageResponse> ResetPasswordAsync(ResetPassModel model)
        {
            var user = await _userManager.FindByIdAsync((model.id).ToString());

            if (user is null)  
                    return new ManageResponse("User is not exists");


            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken,
                model.newPassword);
            
           if(!result.Succeeded){
                var messagesBuilder = new StringBuilder();
                foreach (var err in result.Errors){
                    messagesBuilder.Append(err.Description);
                }
                return new ManageResponse(messagesBuilder.ToString());
            }

             return new ManageResponse("Generated Successfully", true);
        }
    }
}