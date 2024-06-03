namespace CC.Input.Logic
{
    public class ValidationController : IValidationController
    {
        public string LineDelimiter
        {
            get { return _LineDelimiter; }
            set { _LineDelimiter = value; }
        }
        private string _LineDelimiter;
        public string FieldDelimiter
        {
            get { return _FieldDelimiter; }
            set { _FieldDelimiter = value; }
        }
        private string _FieldDelimiter;

        public string DateFormat
        {
            get { return _DateFormat; }
            set { _DateFormat = value; }
        }
        private string _DateFormat;
        private string[]? _Lines;
        private int _FirstLineIndex;

        public ValidationController()
        {
            _LineDelimiter = Environment.NewLine;// "\r\n";
            _FieldDelimiter = "|";
            _DateFormat ="yyyyMMdd";
        }
        public void SplitContent(string content)
        {
            _Lines = content.Split(_LineDelimiter);//, StringSplitOptions.RemoveEmptyEntries);
            DetermineHeadersInclusion();
        }

        public void DetermineHeadersInclusion()
        {
            if (_Lines.Length > 0 && _Lines[0].Contains("MPAN"))
                _FirstLineIndex = 1;
            else
                _FirstLineIndex = 0;
        }
        public ValidationResult Validate(string content)
        {
            ValidationResult result = new ValidationResult();

            SplitContent(content);
            if (_Lines != null)
            {
                if (_Lines.Length > _FirstLineIndex + 1)
                {
                    for (int lineNumber = _FirstLineIndex; lineNumber < _Lines.Length; lineNumber++)
                    {
                        string lineMessage = ValidateLine(_Lines[lineNumber]);
                        if (!string.IsNullOrWhiteSpace(lineMessage))
                            result.Errors.Add($"Line:{lineNumber + _FirstLineIndex} {lineMessage}");
                    }
                }
                else
                {
                    result.Errors.Add($"No lines found.");
                }

                result.TotalLines = _Lines.Length-_FirstLineIndex;
            }

            return result;
        }
        private string ValidateLine(string line)
        {
            string message = String.Empty;
            string[] fields = line.Split(_FieldDelimiter, StringSplitOptions.TrimEntries);
            if (fields == null)
            {
                message = "No fields exist on this line.";
            }
            else
            {
                if (fields.Length > 0)
                {
                    string fieldName = "MPAN";
                    if (string.IsNullOrWhiteSpace(fields[0]))
                        message += $" {fieldName}: is required.";
                    else
                    {
                        if (long.TryParse(fields[0], out long field1))
                        {
                            if (field1 < -9999999999999 | field1 > 9999999999999)
                                message += $" {fieldName}: '{field1}' is out of range.";
                        }
                        else
                            message += $" {fieldName}: cannot be parsed as an integer '{fields[0]}'.";
                    }
                }
                if (fields.Length > 1)
                {
                    string fieldName = "MeterSerial";
                    if (string.IsNullOrWhiteSpace(fields[1]))
                        message += $" {fieldName}: is required.";
                    int maxLength = 10;
                    if (fields[1].Length > maxLength)
                        message += $" {fieldName}: {fields[1].Length} characters is too long, max length is {maxLength}.";
                }
                if (fields.Length > 2)
                {
                    string fieldName = "DateOfInstallation";
                    if (string.IsNullOrWhiteSpace(fields[2]))
                        message += $" {fieldName}: is required.";
                    else
                    {
                        if (!DateOnly.TryParseExact(fields[2], _DateFormat, out DateOnly field3))
                            message += $" {fieldName}: cannot be parsed as a date '{fields[2]}'.";
                        else
                        {
                            if (field3 > DateOnly.FromDateTime(DateTime.Now))
                                message += $" {fieldName}: {field3.ToString("yyyyMMdd")} is not in the past.";
                        }
                    }
                }
                if (fields.Length > 3)
                {
                    string fieldName = "AddressLine1";
                    int maxLength = 40;
                    if (fields[3].Length > maxLength)
                        message += $" {fieldName}: {fields[3].Length} characters is too long, max length is {maxLength}.";
                }
                if (fields.Length > 4)
                {
                    string fieldName = "PostCode";
                    int maxLength = 10;
                    if (fields[4].Length > maxLength)
                        message += $" {fieldName}: {fields[4].Length} characters is too long, max length is {maxLength}.";
                }
            }

            return message;
        }
        public IEnumerable<Model.Input> Parse(string content)
        {
            List<Model.Input> inputs = new List<Model.Input>();
            SplitContent(content);
            if (_Lines != null)
            {
                for (int lineNumber = _FirstLineIndex; lineNumber < _Lines.Length; lineNumber++)
                {
                    string line = _Lines[lineNumber];

                    if (String.IsNullOrWhiteSpace(ValidateLine(line)))
                            inputs.Add(ParseLine(line));
                }
            }

            return inputs;
        }
        private Model.Input ParseLine(string line)
        {
            Model.Input input = new Model.Input();
            string[] fields = line.Split(_FieldDelimiter, StringSplitOptions.TrimEntries);
            if(fields != null)
            {
                if (fields.Length > 0)
                    input.MPAN = long.Parse(fields[0]);
                if (fields.Length > 1)
                    input.MeterSerial = fields[1];
                if (fields.Length > 2)
                    input.DateOfInstallation = DateOnly.ParseExact(fields[2], _DateFormat);
                if (fields.Length > 3)
                    input.AddressLine1 = fields[3];
                if (fields.Length > 4)
                    input.PostCode = fields[4];
            }

            return input;
        }
    }
}
