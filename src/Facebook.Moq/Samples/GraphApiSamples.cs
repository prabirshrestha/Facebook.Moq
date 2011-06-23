using Xunit;

namespace Facebook.Moq.Samples
{
    public class GraphApiSamples
    {
        [Fact]
        public void AnythingReturnsTheMockedResult()
        {
            var mockFb = FacebookMock.New();

            mockFb
                .FbSetup()
                .ReturnsJson("{\"id\":\"4\",\"name\":\"Mark Zuckerberg\",\"first_name\":\"Mark\",\"last_name\":\"Zuckerberg\",\"link\":\"http:\\/\\/www.facebook.com\\/zuck\",\"username\":\"zuck\",\"gender\":\"male\",\"locale\":\"en_US\"}");

            var fb = mockFb.Object;

            dynamic result = fb.Get("/4");

            Assert.Equal("Mark Zuckerberg", result.name);
        }
    }
}