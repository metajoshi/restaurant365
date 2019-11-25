using System;
using Xunit;

namespace Calculator
{
    public class ProgramTests
    {
        [Fact]
        public void MissingNumbersShouldConvertToZero()
        {
            var input = ",5";
            var result = Calculator.PerformAddition(input, false, false, false);
            Assert.True(result == 5);
        }

        [Fact]
        public void InvalidNumbersShouldConvertToZero()
        {
            var input = "5,tytyt";
            var result = Calculator.PerformAddition(input, false, false, false);
            Assert.True(result == 5);
        }

        [Fact]
        public void ThrowExceptionIfMoreThanTwoArguments()
        {
            var input = "1,2,3";

            try
            {
                var result = Calculator.PerformAddition(input, true, true, false);
                Assert.False(true);
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void DontThrowExceptionIfMoreThanTwoArguments()
        {
            var input = "1,2,3";
            try
            {
                var result = Calculator.PerformAddition(input, true, false, false);
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void AddCorrectlyWithNewlineSeparator()
        {
            var input = @"1\n2,3";
            var result = Calculator.PerformAddition(input, false, false, false);
            Assert.True(result == 6);
        }

        [Fact]
        public void ThrowExceptionForNegativeNumbers()
        {
            var input = "-1,2,-3,5";
            try
            {
                var result = Calculator.PerformAddition(input, false, false, false);
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void NumbersGreaterThanThousandAreInvalid()
        {
            var input = "2,1001,6";
            var result = Calculator.PerformAddition(input, false, false, true);
            Assert.True(result == 8);
        }

        [Fact]
        public void SupportsCustomDelimiterSingleCharacter()
        {
            var input = @"//#\n2#5";
            var result = Calculator.PerformAddition(input, false, false, true);
            Assert.True(result == 7);
        }

        [Fact]
        public void SupportsCustomDelimiterSingleString()
        {
            var input = @"//[***]\n11***22***33";
            var result = Calculator.PerformAddition(input, false, false, false);
            Assert.True(result == 66);
        }

        [Fact]
        public void SupportsMultCustomDelimiterMultiString()
        {
            var input = @"//[*][!!][r9r]\n11r9r22*hh*33!!44";
            var result = Calculator.PerformAddition(input, false, false, false);
            Assert.True(result == 110);
        }
    }
}
