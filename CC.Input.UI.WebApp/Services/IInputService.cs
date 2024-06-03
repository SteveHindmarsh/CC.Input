using CC.Input.Logic;

namespace CC.Input.UI.WebApp.Services;
public interface IInputService
{
    Task<IEnumerable<Logic.Model.Input>> RetrieveAsync();

    Task<ValidationResult> UploadAsync(Stream stream, bool commitIfValid);

    Task DeleteAllAsync();
}
