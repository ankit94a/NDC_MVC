using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface ICountryMasterRepository : IRepository<CountryMaster>
    {
        IEnumerable<SelectListItem> GetCountries();
        IEnumerable<SelectListItem> GetCountriesOnlyName();
        IEnumerable<SelectListItem> GetSelectedCountries(int id);
    }
}
