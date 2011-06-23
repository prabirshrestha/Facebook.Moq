
namespace Facebook.Moq
{
    using global::Moq;

    public static class FacebookMock
    {
        public static Mock<FacebookClient> New()
        {
            return new Mock<FacebookClient> { CallBase = true };
        }
    }
}