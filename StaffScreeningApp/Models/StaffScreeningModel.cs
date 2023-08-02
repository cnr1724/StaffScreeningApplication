using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StaffScreeningApp.Models
{
    [Table("tblStaffscreening")]
    public class Staffscreening
    {
        public int user_id { get; set; }
        [Required]
        public string fever_check { get; set; }
        [Required]
        public string runny_nose_check { get; set; }
        [Required]
        public string sore_throat_check { get; set; }       
    }
}
