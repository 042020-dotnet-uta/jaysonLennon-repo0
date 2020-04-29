using System;
using Xunit;

namespace test_hello
{
    public class UnitTest1
    {
        [Fact]
        public void TestHello()
        {
            Assert.Equal(1, libhello.Class1.TestMe());
        }
    }
}
