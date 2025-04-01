using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Data.Models;

namespace Assignment1.Data.Interfaces
{
    public interface IRookiesRepository
    {
        List<Person> GetMales();
        List<Person> GetAllRookies();
        List<Person> GetRookiesBornIn(int year);
        List<Person> GetRookiesBornAfter(int year);
        List<Person> GetRookiesBornBefore(int year);
    }
}
