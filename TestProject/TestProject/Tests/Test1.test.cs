using NUnit.Framework;
using TestProject.App;

namespace TestProject.Tests
{
    [TestFixture]
    public class Test1
    {
        private QuadraticEquationService _quadraticEquationService;

        [SetUp]
        public void SetUp()
        {
            _quadraticEquationService = new QuadraticEquationService();
        }

        [Test]
        public void DiscriminantNegative_NoRoots()
        {
            double[] result = _quadraticEquationService.Solve(1, 0, 1);

            Assert.That(result, Is.Empty, "no roots");
        }

        [Test]
        public void DiscriminantPositive_TwoRoots()
        {
            double[] result = _quadraticEquationService.Solve(1, 0, -1);
            double[] expected = [1, -1];

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DiscriminantZero_OneRoot()
        {
            double[] result = _quadraticEquationService.Solve(1, 2, 1);
            double[] expected = [-1];

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ACoefficientIsZero_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _quadraticEquationService.Solve(0, 2, 1));
            Assert.Throws<ArgumentException>(() => _quadraticEquationService.Solve(1e-325, 2, 1));
        }

        [Test]
        public void NonNumericCoefficients_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _quadraticEquationService.Solve(double.NaN, double.NaN, double.NaN));
            Assert.Throws<ArgumentException>(() => _quadraticEquationService.Solve(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity));
            Assert.Throws<ArgumentException>(() => _quadraticEquationService.Solve(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity));
        }
    }
}
