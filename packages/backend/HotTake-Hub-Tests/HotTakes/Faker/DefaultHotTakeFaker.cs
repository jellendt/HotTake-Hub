using AutoBogus;
using HotTake_Hub_Backend.Entities;

namespace HotTake_Hub_Tests.HotTakes.Faker
{
    public class DefaultHotTakeFaker : AutoFaker<HotTake>
    {
        public DefaultHotTakeFaker() { 
            RuleFor(faker => faker.Text, faker => faker.Hacker.Phrase());
        }
    }

    public class ActiveHotTakeFaker : AutoFaker<HotTake>
    {
        public ActiveHotTakeFaker()
        {
            RuleFor(faker => faker.Text, faker => faker.Hacker.Phrase());
            RuleFor(faker => faker.IsDeleted, false);
        }
    }

    public class DeletedHotTakeFaker : AutoFaker<HotTake>
    {
        public DeletedHotTakeFaker()
        {
            RuleFor(faker => faker.Text, faker => faker.Hacker.Phrase());
            RuleFor(faker => faker.IsDeleted, true);
        }
    }
}
