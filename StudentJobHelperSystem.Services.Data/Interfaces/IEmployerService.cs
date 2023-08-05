using StudentJobHelperSystem.Web.ViewModels.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentJobHelperSystem.Services.Data.Interfaces
{
    public interface IEmployerService
    {
        Task<bool> EmployerExistByUserId(string userId);

        Task<bool> EmployerExistByCompanyNumber(string companyNumber);

        Task<bool> UserHasWorkplace(string userId);

        Task Create(string userId, BecomeEmployerFormModel model);
    }
}
