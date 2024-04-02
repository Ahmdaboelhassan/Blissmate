namespace Blessmate;

public interface IPhotoService
{
    Task<string?> AddProfilePicture(int id , IFormFile picture);
    Task<string?> AddTherapistCertificate(int id , IFormFile certificate);
}
