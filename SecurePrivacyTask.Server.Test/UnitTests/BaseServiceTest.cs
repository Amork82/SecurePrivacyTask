using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecurePrivacyTask.Server.Services;

namespace SecurePrivacyTask.Server.Test.UnitTests
{
    [TestClass]
    public class BaseServiceTest
    {
        private BaseService baseService;
        public BaseServiceTest()
        {
            baseService = new BaseService();
        }

        [TestMethod]
        public async Task VerifyBinaryString_ShouldReturnTrue_WhenStringIsValid()
        {
            // Arrange
            string validBinaryString = "1100";

            // Act
            var result = await baseService.VerifyBinaryString(validBinaryString);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task VerifyBinaryString_ShouldReturnFalse_WhenStringHasMoreZerosInPrefix()
        {
            // Arrange
            string invalidBinaryString = "1001";

            // Act
            var result = await baseService.VerifyBinaryString(invalidBinaryString);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public async Task VerifyBinaryString_ShouldReturnFalse_WhenNumberOfOnesAndZerosAreNotEqual()
        {
            // Arrange
            string invalidBinaryString = "110";

            // Act
            var result = await baseService.VerifyBinaryString(invalidBinaryString);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public async Task VerifyBinaryString_ShouldThrowArgumentException_WhenStringContainsInvalidCharacters()
        {
            // Arrange
            string invalidBinaryString = "1102";

            // Act
            var result = await baseService.VerifyBinaryString(invalidBinaryString);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
