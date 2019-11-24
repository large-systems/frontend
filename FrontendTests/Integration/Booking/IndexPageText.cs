using AngleSharp;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FrontendTests.Integration.Booking
{
    public class IndexPageText : IntegrationTest
    {
        public IndexPageText(HotelApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Get_Index_ReturnsPageWithSearchFormAndCreateButton()
        {
            var response = await _factory.CreateClient().GetAsync("/Booking");
            var content = await response.Content.ReadAsStringAsync();
            var doc = await BrowsingContext.New().OpenAsync(req => req.Content(content));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(doc.QuerySelector("form"));
            var createBtn = doc.QuerySelector("form a");
            Assert.NotNull(createBtn);
            Assert.Equal("Search Availability", createBtn.TextContent.ToString());
        }

    }
}
