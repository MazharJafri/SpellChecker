using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    class DictionaryDotComSpellCheckerTests
    {
        private ISpellChecker SpellChecker;


        [TestFixtureSetUp]
        public void TestFixureSetUp()
        {
            var httpClientMock = new Mock<IHttpClient>();

            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            
            ServicePointManager.SecurityProtocol = Tls12;
            
            var mock = new Mock<HttpHandler>();
            
            
            var uri = "https://dictionary.reference.com/browse/";   
            


            SpellChecker = new DictionaryDotComSpellChecker(mock.Object, uri);
        }

        [Test]
        public void Check_That_SharpEcho_Is_Misspelled()
        {
            var result = SpellChecker.Check("test");
            Assert.IsTrue(result);


        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var result = SpellChecker.Check("teeeeseeeet");
            Assert.IsFalse(result);
        }
    }
}
