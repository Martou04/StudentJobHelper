namespace StudentJobHelperSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentJobHelperSystem.Services.Data;
    using StudentJobHelperSystem.Services.Data.Interfaces;
    using StudentJobHelperSystem.Web.Infrastructure.Extentions;
    using StudentJobHelperSystem.Web.ViewModels.JobAd;
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class JobAdController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IEmployerService employerService;
        private readonly IJobAdService jobAdService;

        public JobAdController(ICategoryService categoryService, IEmployerService employerService, 
            IJobAdService jobAdService)
        {
            this.categoryService = categoryService;
            this.employerService = employerService;
            this.jobAdService = jobAdService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return this.Ok();
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

        [HttpPost]
        public async Task<IActionResult> Add(JobAdFormModel model)
        {
            bool isEmployer =
                await this.employerService.EmployerExistByUserId(this.User.GetId()!);
            if (!isEmployer)
            {
                this.TempData[ErrorMessage] = "You must become an employer in order to add new job offers!";

                return this.RedirectToAction("Become", "Employer");
            }

            bool categoryExist =
                await this.categoryService.ExistsById(model.CategoryId);
            if (!categoryExist)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if(!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategories();

                return this.View(model);
            }

            try
            {
                string? employerId = 
                    await this.employerService.GetEmployerIdByUserId(this.User.GetId()!);

                await this.jobAdService.Create(model, employerId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new job offer!");

                model.Categories = await this.categoryService.AllCategories();
                return this.View(model);
            }

            return this.RedirectToAction("All","JobAd");
        }
    }
}
