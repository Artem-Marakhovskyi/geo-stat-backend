using System;
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
        [Test]
        public void Should_Be_A_Problematic_Test()
        {
            Assert.False(new FakeClass().FakeTrueMethod());
        }
    }
}
