namespace ReaLocate.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VisitorsDetails : BaseModel<int>
    {
        private ICollection<User> users;

        public VisitorsDetails()
        {
            this.users = new HashSet<User>();
        }

        public virtual ICollection<User> AllUsers { get { return this.users; } set { this.users = value; } }
    }
}
