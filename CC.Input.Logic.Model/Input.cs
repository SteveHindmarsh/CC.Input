using System.ComponentModel.DataAnnotations;

namespace CC.Input.Logic.Model
{
    /// <summary>
    /// Field Name	        Type	        Optionality	    Format	        Notes
    /// MPAN                Numeric(13,0)   Mandatory	    9(13,0)	
    /// MeterSerial         Varchar(10)     Mandatory	    1-10 characters
    /// DateOfInstallation  Date            Mandatory       YYYYMMDD        Must be a date in the past
    /// AddressLine1        Varchar(40)     Optional
    /// PostCode            Varchar(10)     Optional        XX9 9XX
    /// </summary>
    [InputValidation]
    public class Input
    {
        public int Id { get; set; }//TODO: Is this required? What is and how unique is the MPAN? Can/should that be used instead?
        /// <summary>
        /// int max value is less than 9999999999999 so...
        /// long MaxValue = 9223372036854775807 
        /// TODO: what is optimal type here?
        /// </summary>
        [Required]
        [Range(0, 9999999999999)]
        public long MPAN { get; set; }//TODO: Field name -> Mpan
        [Required]
        [StringLength(10)]
        public string MeterSerial { get; set; }
        [Required]
        public DateOnly DateOfInstallation { get; set; }
        [StringLength(40)]
        public string? AddressLine1 { get; set; }
        [StringLength(10)]
        public string? PostCode { get; set; }
    }
}
