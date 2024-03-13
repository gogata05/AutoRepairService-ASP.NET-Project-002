namespace AutoRepairService.Core.ViewModels.Admin
{
    public class StatisticViewModel
    {
        public int AllRepairs { get; set; }
        public int ActiveRepairs { get; set; }
        public int PendingRepairs { get; set; }
        public int DeclinedRepairs { get; set; }
        public int CompletedRepairs { get; set; }
    }
}
