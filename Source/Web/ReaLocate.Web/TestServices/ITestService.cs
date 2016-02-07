using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaLocate.Web.TestServices
{
    public interface ITestService
    {
        IEnumerable<Item> GetData();
       
    }
}
