using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace countries.Models
{
    public interface ICountriesRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryByID(int id);
        void InsertCountry(Country country);
        void DeleteCountry(int id);
        void EditCountry(Country country);
    }
}