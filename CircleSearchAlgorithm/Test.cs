namespace CircleSearchAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int dim = 2; 
            double[] lb = new double[dim];
            double[] ub = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                lb[i] = -5.12;
                ub[i] = 5.12;
            }
            CircleSearchAlgorithm csa = new CircleSearchAlgorithm(N: 20,I: 80,Dim: dim,Fun: TestFunctions.Rastrigin,Lb: lb, Ub: ub);
            double Fbest = csa.Solve();
            double[] XBest = csa.XBest;
            Console.WriteLine($"Result: f({string.Join(", ", XBest)}) = {Fbest}");
        }
    }
}
