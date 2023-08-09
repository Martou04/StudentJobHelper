using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentJobHelperSystem.Web.ViewModels.JobAd
{
    public class JobAdPreDeleteDetailsViewModel
    {
        public string Title { get; set; } = null!;

        public string CityOfWork { get; set; } = null!;

        [Display(Name = "Logo Image Link")]
        public string LogoUrl { get; set; } = null!;
    }
}
