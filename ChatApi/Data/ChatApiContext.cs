using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApi.Model;

namespace ChatApi.Data
{
    public class ChatApiContext : DbContext
    {
        public ChatApiContext (DbContextOptions<ChatApiContext> options)
            : base(options)
        {
        }

        public DbSet<ChatApi.Model.chatMamber> chatMamber { get; set; } = default!;

        public DbSet<ChatApi.Model.friendChatModel>? friendChatModel { get; set; }

        public DbSet<ChatApi.Model.FriendModel>? FriendModel { get; set; }

        public DbSet<ChatApi.Model.Requests>? Requests { get; set; }

        public DbSet<ChatApi.Model.accountInformation>? accountInformation { get; set; }
    }
}
