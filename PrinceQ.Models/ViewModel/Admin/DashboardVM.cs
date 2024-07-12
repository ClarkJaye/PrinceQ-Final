using PrinceQ.Models.Entities;

namespace PrinceQ.Models.ViewModel.Admin
{
    public class DashboardVM
    {
        public List<User>? Users { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
