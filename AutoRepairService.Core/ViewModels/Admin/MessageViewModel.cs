namespace AutoRepairService.Core.ViewModels.Admin
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string ConnectionId { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
