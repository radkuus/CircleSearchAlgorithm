namespace CircleSearchAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            CircleSearchAlgorithm csa = new CircleSearchAlgorithm(N: 40, I: 40, Dim: 2, Fun: TestFunctions.Rastrigin, Lb: [-5.12], Ub: [5.12]);
            double Fbest = csa.Solve();
            double[] XBest = csa.XBest;
            Console.WriteLine($"Result: f({string.Join(", ", XBest)}) = {Fbest}");
        }
    }
}
