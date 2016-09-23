using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace countries.Models
{
    public class CountriesRepository : ICountriesRepository
    {
        private List<Country> allCountries;
        private XDocument countriesData;
        //constructor
        public CountriesRepository()
        {
            allCountries = new List<Country>();
            countriesData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Countries.xml"));
            var countries = from country in countriesData.Descendants("country")
                           select new Country((int)country.Element("id"), country.Element("name").Value,
                           country.Element("continent").Value, country.Element("language").Value,
                           country.Element("capital").Value, (ulong)country.Element("area"),(ulong)country.Element("population"),
                           (uint)country.Element("callcode"));
            allCountries.AddRange(countries.ToList<Country>());
        }
        // return a list of all countries
        public IEnumerable<Country> GetCountries()
        {
            return allCountries;
        }
        public Country GetCountryByID(int id)
        {
            return allCountries.Find(country => country.id == id);
        }
        //insert record
        public void InsertCountry(Country country)
        {
            country.id = (int)(from b in countriesData.Descendants("country") orderby (int)b.Element("id") descending select (int)b.Element("id")).FirstOrDefault() + 1;

            countriesData.Root.Add(new XElement("country", new XElement("id", country.id), new XElement("continent", country.continent),
                new XElement("name", country.name), new XElement("language", country.language), new XElement("capital", country.capital),
                new XElement("area", country.area.ToString()), new XElement("population", country.population.ToString()), new XElement("callcode", country.callcode.ToString())));

            countriesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Countries.xml"));
        }
        public void DeleteCountry(int id)
        {
            countriesData.Root.Elements("country").Where(i => (int)i.Element("id") == id).Remove();
            countriesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Countries.xml"));
        }
        public void EditCountry(Country country)
        {
            XElement node = countriesData.Root.Elements("country").Where(i => (int)i.Element("id") == country.id).FirstOrDefault();

            node.SetElementValue("continent", country.continent);
            node.SetElementValue("name", country.name);
            node.SetElementValue("language", country.language);
            node.SetElementValue("capital", country.capital);
            node.SetElementValue("area", country.area.ToString());
            node.SetElementValue("population", country.population.ToString());
            node.SetElementValue("callcode", country.callcode.ToString());
            countriesData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Countries.xml"));
        }

    }
}