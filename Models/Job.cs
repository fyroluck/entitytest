using System.ComponentModel.DataAnnotations;

namespace entitytest.Models;

public class Job
{
    public int JobID { get; set; }  // Primary Key

    [Required]
    public string JobTitle { get; set; }

    public int EmployerID { get; set; }  // Foreign Key to Employer

    public Employer Employer { get; set; }  // Navigation property to Employer
}