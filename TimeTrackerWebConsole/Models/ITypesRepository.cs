using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace types.Models
{
    public interface ITypesRepository //ICountrieRepository
    {
        IEnumerable<Typex> GetTypes();
        Typex GetTypeByID(int id);
        void InsertType(Typex type);
        void DeleteType(int id);
        void EditType(Typex type);
    }
}