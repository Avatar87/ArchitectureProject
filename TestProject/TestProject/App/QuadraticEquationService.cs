namespace TestProject.App
{
    public class QuadraticEquationService
    {
        public double[] Solve(double a, double b, double c)
        {
            double discr = Math.Pow(b, 2) - 4 * a * c;
            double[] roots = new double[2];

            if (a.Equals(0.0))
            {
                throw new ArgumentException();
            }

            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
            {
                throw new ArgumentException();
            }

            if (double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
            {
                throw new ArgumentException();
            }

            if (discr < 0)
            {
                return new double[0];
            }
            else if (discr > 0)
            {
                var root1 = (-b + Math.Sqrt(discr)) / 2 * a;
                var root2 = (-b - Math.Sqrt(discr)) / 2 * a;
                return [root1, root2];
            }
            else
            {
                var root = -b / 2 * a;
                return [root];
            }
        }
    }
}
