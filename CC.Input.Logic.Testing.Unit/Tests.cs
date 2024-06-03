namespace CC.Input.Logic.Testing.Unit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ValidationFailure1()
        {

            IValidationController validationController = new ValidationController();
            IRepository<Model.Input> repo = new Logic.Mock.Repository(validationController);
            ValidationResult validationResult = await repo.UploadAsync(GetFileWithErrors(), true);

            Assert.IsNotNull(validationResult);
            Assert.That(!validationResult.IsValid);
            Assert.That(!validationResult.IsCommitted);
            Assert.That(4 == validationResult.TotalLines);
            Assert.That(2 == validationResult.Errors.Count());
            Assert.That("Line:2  MPAN: is required." == validationResult.Errors[0]);
            Assert.That("Line:3  MeterSerial: is required." == validationResult.Errors[1]);
        }

        public async Task ValidationSuccess1()
        {

            IValidationController validationController = new ValidationController();
            IRepository<Model.Input> repo = new Logic.Mock.Repository(validationController);
            //ValidationResult validationResult = await repo.UploadAsync(GetFileWithErrors(), true);

            Assert.Pass();
        }

        public string GetFileWithErrors()
        {
            //TODO
            return @"0000000000009|XQ8E020624|20050920||
9999999999999|XQ3E103920|20030623||
|XQ2E05971|20050920||
9912345679873||20021021|SUITE 4; SECOND FLOOR|";
        }
    }
}