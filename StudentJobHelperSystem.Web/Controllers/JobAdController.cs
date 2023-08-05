namespace StudentJobHelperSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentJobHelperSystem.Services.Data.Interfaces;
    using StudentJobHelperSystem.Web.Infrastructure.Extentions;
    using StudentJobHelperSystem.Web.ViewModels.JobAd;
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class JobAdController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IEmployerService employerService;

        public JobAdController(ICategoryService categoryService, IEmployerService employerService)
        {
            this.categoryService = categoryService;
            this.employerService = employerService; 
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isEmployer =
                await this.employerService.EmployerExistByUserId(this.User.GetId()!);

            if (!isEmployer)
            { 
                this.TempData[ErrorMessage] = "You must become an employer in order to add new job offers!";

                return this.RedirectToAction("Become", "Employer");
            }


            JobAdFormModel model = new JobAdFormModel()
            {
                Categories = await this.categoryService.AllCategories()
            };

            return View(model);
        }
    }
}
