using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoBogus;
using Bogus;
using Xunit.Sdk;
using Xunit.v3;

namespace HotTake_Hub_Tests
{
    public sealed class AutoFakerDataAttribute<T, TFaker>(int count) : DataAttribute
    where T : class
    where TFaker : Faker<T>, new()
    {
        public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(
            MethodInfo testMethod,
            DisposalTracker disposalTracker)
        {
            var faker = new TFaker();

            IReadOnlyCollection<ITheoryDataRow> rows =
            [
                new TheoryDataRow(faker.Generate(count))
            ];

            return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
        }

        public override bool SupportsDiscoveryEnumeration() => true;
    }
}
