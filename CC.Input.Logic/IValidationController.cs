namespace CC.Input.Logic
{
    public interface IValidationController
    {
        ValidationResult Validate(string content);
        IEnumerable<Model.Input> Parse(string content);
    }
}
