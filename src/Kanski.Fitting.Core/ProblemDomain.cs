using System;
using System.Diagnostics.Contracts;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;

namespace Kanski.Fitting.Core
{
    public static class ProblemDomain
    {
        private static readonly int[] Observations = new int[] { 1, 1, 5, 6, 11, 17, 22, 31, 51, 68, 104, 125, 177, 238, 287, 355, 425, 536, 634, 749, 890 };

        public static (double L, double k, double x0) ToPoint(this IChromosome chromosome)
        {
            Contract.Assert(null != chromosome);
            var values = ((FloatingPointChromosome)chromosome).ToFloatingPoints();
            Contract.Assert(3 == values.Length);
            return (L: values[0], k: values[1], x0: values[2]);
        }

        public static double Fitness(this IChromosome chromosome)
        {
            (var L, var k, var x0) = chromosome.ToPoint();
            var f = BuildLogisticFunction(L, k, x0);
            return 1 / Observations.Select((obs, n) => Math.Pow(f(n) - obs, 2.0)).Sum();
        }

        private static Func<int, double> BuildLogisticFunction(double L, double k, double x0)
        {
            return x => L / (1.0 + Math.Exp(k * (x0 - x)));
        }
    }
}
