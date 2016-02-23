namespace ReaLocate.Services.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReaLocate.Services.Web;

    [TestClass()]
    public class IdentifierProviderTests
    {
        [TestMethod]
        public void EncodingAndDecodingDoesntChangeTheId()
        {
            const int Id = 1337;
            IIdentifierProvider provider = new IdentifierProvider();
            var encoded = provider.EncodeId(Id);
            var actual = provider.DecodeId(encoded);
            Assert.AreEqual(Id, actual);
        }
    }
}