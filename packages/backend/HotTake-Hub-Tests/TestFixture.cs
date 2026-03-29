using System;
using System.Collections.Generic;
using System.Text;
using HotTake_Hub_Backend;
using HotTake_Hub_Backend.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HotTake_Hub_Tests
{
    public class TestFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServices();
            services.AddEntityFrameworkInMemoryDatabase();

            services.AddDbContext<DbHotTakeContext>((options, context) =>
            {
                context.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
        }

        protected override ValueTask DisposeAsyncCore()
        {
            return new();
        }
    }
}
