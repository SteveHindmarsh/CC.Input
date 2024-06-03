using System.Runtime.CompilerServices;

namespace CC.Input.Logic.Mock;
public class Repository : IRepository<Model.Input>
{
    private IValidationController _validationController;
    private List<Model.Input> _list { get; }

    public Repository(IValidationController validationController)
    {
        _validationController = validationController;
        _list = new List<Model.Input>()
        {
            new Model.Input() { Id = 1, MPAN=1, MeterSerial="MS1", DateOfInstallation = DateOnly.FromDateTime(DateTime.Parse("1/1/2024")), AddressLine1="", PostCode =""  },
            new Model.Input() { Id = 2, MPAN=2, MeterSerial="MS2", DateOfInstallation = DateOnly.FromDateTime(DateTime.Parse("3/1/2024")), AddressLine1="3 Westmorland Drive", PostCode = "NE12 6AZ" },
        };
    }

    public async Task<IEnumerable<Model.Input>> RetrieveAllAsync()
    {
        return _list.AsEnumerable();
    }

    public async Task<ValidationResult> UploadAsync(string text, bool commitIfValid)
    {
        ValidationResult result = _validationController.Validate(text);

        if(result.IsValid && commitIfValid) 
        {
            IEnumerable<Model.Input> inputs = _validationController.Parse(text);
            _list.AddRange(inputs);
            result.IsCommitted = true;
            return result;
        }
        else
        {
            return result;
        }
    }

    public async Task DeleteAllAsync()
    {
       _list.Clear();
    }

    public async Task<Model.Input?> RetrieveAsync(int id)
    {
        return _list.SingleOrDefault(i => i.Id == id);
    }
}
