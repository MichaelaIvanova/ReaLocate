using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaLocate.Services.Web;


namespace ReaLocate.Services.Web.Tests
{
    public partial class IdentifierProviderTest
    {

        [TestMethod]
        [PexGeneratedBy(typeof(IdentifierProviderTest))]
        public void EncodeId897()
        {
            string s;
            IdentifierProvider s0 = new IdentifierProvider();
            s = this.EncodeId(s0, 0);
            Assert.AreEqual<string>("MC4xMjMxMjMxMzEyMw==", s);
            Assert.IsNotNull((object)s0);
        }
    }
}
