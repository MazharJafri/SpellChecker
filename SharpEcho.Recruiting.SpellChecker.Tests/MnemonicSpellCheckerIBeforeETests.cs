using NUnit.Framework;

using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;
using System.Text.RegularExpressions;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    public class MnemonicSpellCheckerIBeforeETests
    {
        private ISpellChecker SpellChecker;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            SpellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            // implement this test


            // Act
            var result = SpellChecker.Check("pie");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var result = SpellChecker.Check("cie");
            Assert.IsFalse(result);
        }      
    }
}
