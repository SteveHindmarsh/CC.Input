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

    //private async Task<int> BulkUploadAsync(string text)
    //{
    //    //TODO Investigate optimal import from large file with millions of lines
    //    using (System.Data.SqlClient.SqlBulkCopy bulkCopy =
    //    new System.Data.SqlClient.SqlBulkCopy(sqlConnection))
    //    {
    //        bulkCopy.DestinationTableName = destinationTableName;
    //        bulkCopy.BatchSize = 1000; // 1000 rows
    //        bulkCopy.WriteToServer(dataTable); // May also pass in DataRow[]
    //    }
    //}

    public async Task<Model.Input?> RetrieveAsync(int id)
    {
        return await _inputDbContext.Inputs.SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task DeleteAllAsync()
    {
        await _inputDbContext.Inputs.ExecuteDeleteAsync();
    }
}
