using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_API_Assessment.Interfaces
{
    public interface IDictionaryGenerator
    {
        void GenerateDictionary(string fileName, string baseWord);
    }
}
