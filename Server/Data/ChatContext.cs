using Microsoft.EntityFrameworkCore;
using RealTimeChat.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeChat.Server.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Message> Messages { get; set; }

    }
}
