using System;
using System.Collections.Generic;
using System.Text;
using AutoBogus;
using HotTake_Hub_Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace HotTake_Hub_Tests
{
    public class TestBase<TUnitUnderTest> : TestBed<TestFixture>
    {
        private readonly IServiceScope _scope;
        public TestBase(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
        {
            _scope = fixture.GetServiceProvider(testOutputHelper).CreateScope();
        }

        protected TService GetService<TService>()
        {
            return _scope.ServiceProvider.GetService<TService>();
        }

        protected override void Dispose(bool disposing)
        {
            _scope.Dispose();
            base.Dispose(disposing);
        }

        protected override ValueTask DisposeAsyncCore()
        {
            _scope.Dispose();
            return base.DisposeAsyncCore();
        }

        protected virtual TUnitUnderTest CreateUnitUnderTest()
        {
            return GetService<TUnitUnderTest>();
        }

        protected void UsingDbContext(Action<DbHotTakeContext> action)
        {
            DbHotTakeContext context = GetService<DbHotTakeContext>();
            action(context);
            context.SaveChanges();
        }

        protected T UsingDbContext<T>(Func<DbHotTakeContext, T> action)
        {
            DbHotTakeContext context = GetService<DbHotTakeContext>();
            T? result = action(context);
            context.SaveChanges();

            return result;
        }

        protected IEnumerable<T> CreateAndInsertEntity<T>(AutoFaker<T> faker, int count) where T : class
        {
            return UsingDbContext(ctx =>
            {
                IEnumerable<T> entity = faker.Generate(count);
                ctx.Set<T>().AddRange(entity);

                return entity;
            });
        }

        protected T InsertEntity<T>(T entity) where T : class
        {
            return UsingDbContext(ctx =>
            {
                ctx.Set<T>().Add(entity);

                return entity;
            });
        }
    }
}
