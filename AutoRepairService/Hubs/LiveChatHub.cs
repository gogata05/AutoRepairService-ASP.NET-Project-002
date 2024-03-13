using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Hubs
{
    public class LiveChatHub : Hub
    {
        private readonly IRepository repo;

        public LiveChatHub(IRepository repo)
        {
            this.repo = repo;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.UserIdentifier != "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e")
            {
                var existingUserMessage = await repo.All<Message>().Where(x => x.UserId == Context.UserIdentifier).FirstOrDefaultAsync();

                if (existingUserMessage == null)
                {
                    var messageInfo = new Message()
                    {
                        ConnectionId = Context.ConnectionId,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        UserId = Context.UserIdentifier
                    };

                    await repo.AddAsync(messageInfo);
                    await repo.SaveChangesAsync();
                }
                else
                {
                    existingUserMessage.IsActive = true;
                    existingUserMessage.ConnectionId = Context.ConnectionId;
                    existingUserMessage.CreatedOn = DateTime.Now;
                    await repo.SaveChangesAsync();
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var existingUserMessage = await repo.All<Message>().Where(x => x.UserId == Context.UserIdentifier).FirstOrDefaultAsync();

            if (existingUserMessage != null)
            {
                existingUserMessage.IsActive = false;
                await repo.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveAdminFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }


        public async Task RemoveUserFromGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Group(Context.UserIdentifier).SendAsync("ReceiveMessage", user, message);
        }


        public async Task SendAdminMessage(string user, string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }
    }
}
