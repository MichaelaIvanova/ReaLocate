namespace ReaLocate.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExtendedUserRole : IdentityRole
    {
        public ExtendedUserRole() : base() { }
        public ExtendedUserRole(string name, string description)
            : base(name)
        {
            this.Description = description;
        }
        public virtual string Description { get; set; }
    }
}
