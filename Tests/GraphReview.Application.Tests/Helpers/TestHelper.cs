using AutoFixture;

namespace GraphReview.Application.Tests.Helpers
{
    public static class TestHelper
    {
        public static IFixture SetupFixture()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
