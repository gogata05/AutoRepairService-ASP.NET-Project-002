
namespace AutoJobService.IServices
{
    public interface IRepairService
    {
        public Task AddRepairAsync(AddRepairViewModel model);
        //Task AddJumpAsync(string id, JumpModel model);
    }
}
