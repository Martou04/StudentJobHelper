namespace StudentJobHelperSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StudentJobHelperSystem.Services.Data.Interfaces;
    using StudentJobHelperSystem.Web.Infrastructure.Extentions;
    using StudentJobHelperSystem.Web.ViewModels.Employer;
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class EmployerController : Controller
    {
        private readonly IEmployerService employerService;

        public EmployerController(IEmployerService employerService)
        {
            this.employerService = employerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.employerService.EmployerExistByUserId(userId);

            if(isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an employer!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeEmployerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isEmployer = await this.employerService.EmployerExistByUserId(userId);
            if (isEmployer)
            {
                this.TempData[ErrorMessage] = "You are already an employer!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isCompanyNumberTaken =
                await this.employerService.EmployerExistByCompanyNumber(model.CompanyNumber);
            if(isCompanyNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.CompanyNumber), "Employer with the provided company number already exists!");
            }

            if(!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool userHasWorkPlace = await this.employerService
                .UserHasWorkplace(userId);

            if(userHasWorkPlace)
            {
                this.TempData[ErrorMessage] = "You must not have any workplaces to become an employer!";

                return this.RedirectToAction("MineWorkplaces", "JobAd");
            }

            try
            {
                await this.employerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as an employer!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "JobAd");
        }
    }
}
