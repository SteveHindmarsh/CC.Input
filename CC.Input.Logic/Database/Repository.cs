using CC.Input.Data;
using Microsoft.EntityFrameworkCore;

namespace CC.Input.Logic.Database;

public class Repository : IRepository<Model.Input>
{
    private InputDbContext _inputDbContext { get; set; }

    private IValidationController _validationController;

    public Repository(
        InputDbContext inputDbContext,
        IValidationController validationController)
    {
        _inputDbContext = inputDbContext;
        _validationController = validationController;
    }
    public async Task<IEnumerable<Model.Input>> RetrieveAllAsync()
    {
        return await _inputDbContext.Inputs.ToListAsync();
    }

    public async Task<ValidationResult> UploadAsync(string text, bool commitIfValid)
    {
        ValidationResult result = _validationController.Validate(text);

        if (result.IsValid && commitIfValid)
        {
            IEnumerable<Model.Input> inputs = _validationController.Parse(text);
            foreach (var input in inputs)
            {
                await this.CreateAsync(input);
            }
            await _inputDbContext.SaveChangesAsync();
            result.IsCommitted = true;
            return result;
        }
        else
        {
            return result;
        }
    }
    private async Task<int> CreateAsync(Model.Input entity)
    {
        await _inputDbContext.Inputs.AddAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAllAsync()
    {
        await _inputDbContext.Inputs.ExecuteDeleteAsync();
    }

    //public async Task<Model.Input?> GetByIdAsync(int id)
    //{
    //    return await _inputDbContext.Inputs.FindAsync(id);
    //}

    //public async Task UpdateAsync(Model.Input entity)
    //{
    //    _inputDbContext.Inputs.Update(entity);
    //    await _inputDbContext.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(int id)
    //{
    //    var entity = await _inputDbContext.Inputs.SingleOrDefaultAsync(e => e.Id == id);
    //    if (entity != null)
    //        _inputDbContext.Inputs.Remove(entity);

    //    await _inputDbContext.SaveChangesAsync();
    //}
}
