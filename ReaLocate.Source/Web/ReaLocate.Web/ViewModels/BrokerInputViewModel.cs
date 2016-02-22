using ReaLocate.Data.Models;
using ReaLocate.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.ViewModels
{
    public class BrokerInputViewModel : IMapFrom<User>, IMapTo<User>
    {
        public string UserName { get; set; }
    }
}