namespace StudentJobHelperSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentJobHelperSystem.Services.Data;
    using StudentJobHelperSystem.Services.Data.Interfaces;
    using StudentJobHelperSystem.Services.Data.Models.JobAd;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllJobAdQueryModel queryModel)
        {
            AllJobAdFilteredAndPagedServiceModel serviceModel = 
                await this.jobAdService.All(queryModel);

            queryModel.JobAds = serviceModel.jobAds;
            queryModel.TotalJobAds = serviceModel.TotalJobAdCount;
            queryModel.Categories = await this.categoryService.AllCategoryName();

            return this.View(queryModel);
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool jobAdExists = await this.jobAdService
                .ExistsById(id);
            if(!jobAdExists)
            {
                this.TempData[ErrorMessage] = "Job offer with the provided id does not exist!";

                return RedirectToAction("All", "JobAd");
            }

            JobAdDetailsViewModel viewModel = await this.jobAdService
                .GetDetailsById(id);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<JobAdAllViewModel> myJobAd = new List<JobAdAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserEmployer = await this.employerService
                .EmployerExistByUserId(userId);

            if(isUserEmployer)
            {
                string? employerId = await this.employerService.GetEmployerIdByUserId(userId);

                myJobAd.AddRange(await this.jobAdService.AllByEmployerId(employerId!));
            }
            else
            {
                myJobAd.AddRange(await this.jobAdService.AllByUserId(userId));
            }

            return this.View(myJobAd);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

        }
    }
}
