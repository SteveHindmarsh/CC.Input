namespace CC.Input.Logic
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsCommitted = false;
            Errors = new List<string>();
            TotalLines = 0;
        }

        public List<string> Errors { get; }

        public int TotalLines { get; set; }
        public bool IsValid 
        { 
            get {
                return Errors.Count() == 0 && TotalLines > 0;
            }
        }

        public bool IsCommitted { get; set; }
    }
}
