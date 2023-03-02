using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
        [Required]
        public string Position { get; set; }
        public string Requirement { get; set; }
        [Required]
        public string Location { get; set; }
        public string Benefits { get; set; }
        public string Salary { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
