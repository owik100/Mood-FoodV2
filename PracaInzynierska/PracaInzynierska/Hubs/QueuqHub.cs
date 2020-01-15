using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PracaInzynierska.Models.Entities;

namespace PracaInzynierska.Hubs
{
    public class QueuqHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendNewOrder(Order order)
        {
            await Clients.All.SendAsync("ReceiveOrder", order);
        }
    }
}
