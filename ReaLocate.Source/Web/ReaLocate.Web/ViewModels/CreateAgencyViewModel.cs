namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;

    public class CreateAgencyViewModel : IMapTo<Agency>
    {
        public string Name { get; set; }
    }
}