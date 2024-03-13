using AutoRepairService.Core.ViewModels.Admin;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Core.Services
{
    public class StatisticAdministrationService
    {
        private readonly IRepository repo;
        public StatisticAdministrationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<StatisticViewModel> StatisticAsync()
        {

            /*Pending
            Approved
            Declined
            Deleted
            Completed*/

            int allRepairs = repo.AllReadonly<Repair>().Count();
            int activeRepairs = repo.AllReadonly<Repair>().Where(x => x.Status == "Active").Count();
            int completedRepairs = repo.AllReadonly<Repair>().Where(x => x.Status == "Completed").Count();
            int pendingRepairs = repo.AllReadonly<Repair>().Where(x => x.Status == "Pending").Count();
            int declinedRepairs = repo.AllReadonly<Repair>().Where(x => x.Status == "Declined").Count();

            return new StatisticViewModel()
            {
                ActiveRepairs = activeRepairs,
                AllRepairs = allRepairs,
                PendingRepairs = pendingRepairs,
                DeclinedRepairs = declinedRepairs,
                CompletedRepairs = completedRepairs
            };
        }
    }
}
