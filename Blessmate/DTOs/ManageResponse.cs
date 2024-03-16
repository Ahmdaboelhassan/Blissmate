
namespace Blessmate.Records
{
    public record class ManageResponse
    (
        string Message,
        bool IsCompleted = false,
        string? token = null,
        string? email = null
    )
    {}
}