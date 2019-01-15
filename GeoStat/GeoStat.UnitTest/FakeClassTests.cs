using GeoStat.BussinessLogic;
using NUnit.Framework;

namespace UnitTest1
{
    [TestFixture]
    public class FakeClassTests
    {
        [Test]
        public void Should_ReturnTrue_When_Always()
        {
            Assert.True(new FakeClass().FakeTrueMethod());
        }
    }
}
