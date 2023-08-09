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

                string jobAdId = 
                     await this.jobAdService.CreateAndReturnId(model, employerId!);

                return this.RedirectToAction("Details", "JobAd", new { id = jobAdId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new job offer!");

                model.Categories = await this.categoryService.AllCategories();
                return this.View(model);
            }
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

            try
            {
                JobAdDetailsViewModel viewModel = await this.jobAdService
               .GetDetailsById(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

           
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<JobAdAllViewModel> myJobAd = new List<JobAdAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserEmployer = await this.employerService
                .EmployerExistByUserId(userId);

            try
            {
                if (isUserEmployer)
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
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, JobAdFormModel model)
        {
            if(!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategories();

                return this.View(model);
            }

            bool jobAdExists = await this.jobAdService
               .ExistsById(id);
            if (!jobAdExists)
            {
                this.TempData[ErrorMessage] = "Job offer with the provided id does not exist!";

                return RedirectToAction("All", "JobAd");
            }

            bool isUserEmployer = await this.employerService
                .EmployerExistByUserId(this.User.GetId()!);
            if (!isUserEmployer)
            {
                this.TempData[ErrorMessage] = "You must become an employer in order to edit job offer info!";

                return RedirectToAction("Become", "Employer");
            }

            string? employerId =
                await this.employerService.GetEmployerIdByUserId(this.User.GetId()!);
            bool isEmployerOwner = await this.jobAdService
                .IsEmployerWithIdOwnerOfJobAdWithId(id, employerId!);
            if (!isEmployerOwner)
            {
                this.TempData[ErrorMessage] = "You must be the employer owner of job offer you want to edit!";

                return this.RedirectToAction("Mine", "JobAd");
            }

            try
            {
                await this.jobAdService.EditJobAdByIdAndFormModel(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error ocurred while trying to update the job offer!");
                model.Categories = await this.categoryService.AllCategories();

                return this.View(model);
            }

            return this.RedirectToAction("Details", "JobAd", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool jobAdExists = await this.jobAdService
               .ExistsById(id);
            if (!jobAdExists)
            {
                this.TempData[ErrorMessage] = "Job offer with the provided id does not exist!";

                return RedirectToAction("All", "JobAd");
            }

            bool isUserEmployer = await this.employerService
                .EmployerExistByUserId(this.User.GetId()!);
            if(!isUserEmployer)
            {
                this.TempData[ErrorMessage] = "You must become an employer in order to edit job offer info!";

                return RedirectToAction("Become","Employer");
            }

            string? employerId = 
                await this.employerService.GetEmployerIdByUserId(this.User.GetId()!);
            bool isEmployerOwner = await this.jobAdService
                .IsEmployerWithIdOwnerOfJobAdWithId(id, employerId!);
            if(!isEmployerOwner)
            {
                this.TempData[ErrorMessage] = "You must be the employer owner of job offer you want to edit!";

                return this.RedirectToAction("Mine", "JobAd");
            }

            try
            {
                JobAdFormModel formModel = await this.jobAdService
                .GetJobAdForEditById(id);
                formModel.Categories = await this.categoryService.AllCategories();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occured!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
