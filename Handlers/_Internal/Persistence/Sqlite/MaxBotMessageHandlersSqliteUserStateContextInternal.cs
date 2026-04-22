using Microsoft.EntityFrameworkCore;

namespace KmsDev.MaxBot.Full.Handlers
{
    internal class MaxBotMessageHandlersSqliteUserStateContextInternal : DbContext
    {
        public DbSet<MaxBotMessageHandlersSqliteUserStateModelInternal> UserStates { get; set; }

        public MaxBotMessageHandlersSqliteUserStateContextInternal(DbContextOptions<MaxBotMessageHandlersSqliteUserStateContextInternal> options) : base(options)
        {
            
        }
    }
}
