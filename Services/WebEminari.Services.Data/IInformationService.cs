using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Web.ViewModels.Administration;

namespace WebEminari.Services.Data
{
    public interface IInformationService
    {
        Task<AdministrationIndexViewModel> GenerateAdministrationIndexViewModel();
    }
}
