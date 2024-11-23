using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_API_Assessment.Interfaces
{
    public interface IAuthenticator
    {
        Task<string> Authenticate(string url, string username, string dictFile);
    }
}
