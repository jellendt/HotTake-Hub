using System;
using System.Collections.Generic;
using System.Text;
using HotTake_Hub_Backend.Entities;
using HotTake_Hub_Backend.Services.HotTakeService;
using HotTake_Hub_Tests.HotTakes.Faker;
using Microsoft.EntityFrameworkCore;

namespace HotTake_Hub_Tests.HotTakes
{
    public class HotTake_Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : TestBase<IHotTakeService>(testOutputHelper, fixture)
    {
        [Theory]
        [AutoFakerData<HotTake, DefaultHotTakeFaker>(5)]
        [AutoFakerData<HotTake, DefaultHotTakeFaker>(0)]
        public async Task AddHotTakes_ShouldAdd_WhenHotTakeIsProvided(List<HotTake> hotTakes)
        {
            //arange
            IHotTakeService service = CreateUnitUnderTest();

            //act
            List<HotTake> savedHotTakes = await service.AddAsync(hotTakes);

            //assert
            Assert.Equal(hotTakes.Count, savedHotTakes.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(99)]
        public async Task GetHotTakes_ShouldFetch_WhenEntityIsNotDeleted(int count)
        {
            //arange
            IHotTakeService service = CreateUnitUnderTest();
            List<HotTake> hotTake = [.. CreateAndInsertEntity(new ActiveHotTakeFaker(), count)];

            //act
            List<HotTake> savedHotTakes = await service.GetAllAsync();

            //assert
            Assert.Equal(count, savedHotTakes.Count);
        }

        [Fact]
        public async Task GetHotTakes_ShouldNotFetch_WhenEntityIsDeleted()
        {
            //arange
            IHotTakeService service = CreateUnitUnderTest();
            HotTake hotTakes = new DeletedHotTakeFaker().Generate(1).First();
            InsertEntity(hotTakes);

            //act
            List<HotTake> fetchedHotTakes = await service.GetAllAsync();

            //assert
            Assert.Empty(fetchedHotTakes);
        }

        [Fact]
        public async Task GetHotTakesForAuthor_ShouldGet_WhenCorrectAuthorIdIsProvided()
        {
            //arange
            Guid authorId = Guid.NewGuid();
            ActiveHotTakeFaker faker = new ActiveHotTakeFaker();
            faker.RuleFor(ht => ht.AuthorId, authorId);

            IHotTakeService service = CreateUnitUnderTest();
            List<HotTake> authorHotTakes = [.. CreateAndInsertEntity(faker, 5)];
            List<HotTake> notAuthorHotTakes = [.. CreateAndInsertEntity(new ActiveHotTakeFaker(), 5)];

            //act
            List<HotTake> allHotTake = await service.GetAllAsync();
            List<HotTake> fetchedHotTakes = await service.GetByAuthorId(authorId, TestContext.Current.CancellationToken);

            //assert
            Assert.Equal(allHotTake.Count, authorHotTakes.Count + notAuthorHotTakes.Count);
            Assert.Equal(authorHotTakes, fetchedHotTakes);
        }
    }
}
