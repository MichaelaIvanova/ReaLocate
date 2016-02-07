using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.TestServices
{
    public class TestService : ITestService
    {
        public IEnumerable<Item> GetData()
        {
                return new[] {
                    new Item() {Value="Value 1"},
                    new Item() {Value="Value 2"},
                    new Item() {Value="Value 3"},
                    new Item() {Value="Value 4"},
                };
        }
    }
}