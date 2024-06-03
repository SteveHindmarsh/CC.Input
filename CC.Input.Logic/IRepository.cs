namespace CC.Input.Logic
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<ValidationResult> UploadAsync(string text, bool commitIfValid);
        Task DeleteAllAsync();
    }
}
