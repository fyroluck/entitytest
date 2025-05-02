
namespace entitytest.Models;

public class Employer
{
    public int EmployerID { get; set; }
    public string EmployerName { get; set; }

    public List<Job> Jobs { get; set; }  // Navigation property for related jobs
}

