using System.Xml.Serialization;

namespace CircleSearchAlgorithm
{
    public class CircleSearchAlgorithm : IOptimizationAlgorithm
    {
        public string Name { get; set; } = "Circle Search Algorithm";

        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }

        private int SearchAgentsNo;
        private int MaxIter;
        private double[] lb;
        private double[] ub;
        private int dim;
        private Func<double[], double> fobj;
        private double[][] X_t;
        public double c = 0.8;
        private double[] ResultsHistory;

        public CircleSearchAlgorithm(int N, int I, int Dim, Func<double[], double> Fun, double[] Lb, double[] Ub)
        {
            this.SearchAgentsNo = N;
            this.MaxIter = I;
            this.dim = Dim;
            this.fobj = Fun;
            this.X_t = new double[SearchAgentsNo][];
            this.ResultsHistory = new double[MaxIter];
            this.NumberOfEvaluationFitnessFunction = 0;

            if (Lb.Length == 1)
            {
                this.lb = new double[Dim];
                Array.Fill(this.lb, Lb[0]);
            }
            else
            {
                this.lb = Lb;
            }
            if (Ub.Length == 1)
            {
                this.ub = new double[Dim];
                Array.Fill(this.ub, Ub[0]);
            }
            else
            {
                this.ub = Ub;
            }

            Random rand = new Random();
            for (int i = 0; i < SearchAgentsNo; i++)
            {
                X_t[i] = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    X_t[i][j] = lb[j] + (ub[j] - lb[j]) * rand.NextDouble();
                }
            }
        }

        public double Solve()
        {
            double[] fitness = new double[SearchAgentsNo];

            for (int i = 0; i < SearchAgentsNo; i++)
            {
                fitness[i] = fobj(X_t[i]);
                NumberOfEvaluationFitnessFunction++;
            }

            double best = fitness[0];
            int indx = 0;
            for (int i = 1; i < SearchAgentsNo; i++)
            {
                if (fitness[i] < best)
                {
                    best = fitness[i];
                    indx = i;
                }
            }

            double[] X_c = new double[dim];
            Array.Copy(X_t[indx], X_c, dim);

            int counter = 0;
            Random rand = new Random();

            while (counter < MaxIter)
            {
                double a = Math.PI - Math.PI * Math.Pow((double)counter / MaxIter, 2);

                for (int i = 0; i < SearchAgentsNo; i++)
                {
                    double w = Math.PI * rand.NextDouble() - Math.PI;
                    double p = 1 - 0.9 * Math.Sqrt((double)counter / MaxIter);

                    for (int j = 0; j < dim; j++)
                    {
                        if (counter > (c * MaxIter))
                        {
                            X_t[i][j] = X_c[j] + (X_c[j] - X_t[i][j]) * Math.Tan(w * rand.NextDouble());
                        }
                        else
                        {
                            X_t[i][j] = X_c[j] + (X_c[j] - X_t[i][j]) * Math.Tan(w * p);
                        }

                        if (X_t[i][j] > ub[j])
                        {
                            X_t[i][j] = ub[j];
                        }
                        else if (X_t[i][j] < lb[j])
                        {
                            X_t[i][j] = lb[j];
                        }
                    }
                }

                for (int i = 0; i < SearchAgentsNo; i++)
                {
                    fitness[i] = fobj(X_t[i]);
                    NumberOfEvaluationFitnessFunction++;

                    if (fitness[i] < best)
                    {
                        best = fitness[i];
                        Array.Copy(X_t[i], X_c, dim);
                    }
                }

                ResultsHistory[counter] = best;
                counter++;
            }

            XBest = new double[dim];
            Array.Copy(X_c, XBest, dim);
            FBest = best;
            return FBest;
        }

        public double[] GetResultsHistory()
        {
            return ResultsHistory;
        }
    }
}
