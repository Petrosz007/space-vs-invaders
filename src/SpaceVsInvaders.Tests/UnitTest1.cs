using System.Runtime.InteropServices;
using System;
using Xunit;
using SpaceVsInvaders;

namespace SpaceVsInvaders.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(ToBeTested.AddOne(5) == 6, "5 + 1 = 6");
        }
    }
}
