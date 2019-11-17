using Xunit;

namespace FrontendTests.Integration
{
    public class IntegrationTest : IClassFixture<HotelApplicationFactory>
    {
        protected readonly HotelApplicationFactory _factory;

        public IntegrationTest(HotelApplicationFactory factory)
        {
            _factory = factory;
        }
    }
}
