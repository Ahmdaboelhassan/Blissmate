using Blessmate.DTOs;
using Blessmate.Records;

namespace Blessmate.Services.IServices
{
    public interface IResetPassword
    {
        public Task<ManageResponse> GetPasswordResetToken(int id);
        public Task<ManageResponse> ResetPasswordAsync (ResetPassModel model);
        public Task<ManageResponse> ChangePassword (ChanagePasswordModel model);
    }
}