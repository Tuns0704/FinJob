using finjob_backend.Models;
using finjob_backend.Models.DTO;

namespace finjob_backend.Data
{
    public static class CompanyStore
    {
        public static List<CompanyDTO> villaList = new List<CompanyDTO> {
            new CompanyDTO{Id=1,Name="NFQ", Description = "None",Scale ="20-50 Employees", Location = "HaNoi" },
            new CompanyDTO{Id=2,Name="Orient", Description = "None",Scale ="30-50 Employees", Location = "DaNang" },
            new CompanyDTO{Id=3,Name="KMS", Description = "None",Scale ="50-70 Employees", Location = "SaiGon" },
            new CompanyDTO{Id=4,Name="Orient", Description = "None",Scale ="20-150 Employees", Location = "DaNang" },
            };
    }
}
