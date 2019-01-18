
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LearnCode.Bussiness.SignalIR
{
    public  class UpdateHub:Hub
    {

        public override Task OnConnectedAsync()
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
        }

         

        public async Task Send2(string message)
            {
                await Clients.All.SendAsync("databaselisten",message);
            }
         
    }
}
