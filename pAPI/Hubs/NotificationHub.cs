using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Users;
using Microsoft.AspNetCore.SignalR;

namespace pAPI.Hubs
{
    
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
    public class NotificationHub : Hub
    {
        
        
        
    }
}