using System;
using System.Diagnostics.Contracts;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;

namespace Kanski.Fitting.Core
{
    public static class ProblemDomain
    {
        private static readonly int[] Observations = new int[] { 1, 1, 5, 6, 11, 17, 22, 31, 50, 66, 88, 109, 160, 220, 269, 337, 407, 518, 614, 728, 890, 1030, 1198 };

        public static (double L, double k, double x0) ToPoint(this IChromosome chromosome)
        {
            Contract.Assert(null != chromosome);
            var values = ((FloatingPointChromosome)chromosome).ToFloatingPoints();
            Contract.Assert(3 == values.Length);
            return (L: values[0], k: values[1], x0: values[2]);
        }

        public static Func<IChromosome, double> Fitness()
        {
            var avg = Observations.Average();
            var denom = Observations.Select(x => Math.Pow(x - avg, 2.0)).Sum();

            return chromosome =>
            {
                (var L, var k, var x0) = chromosome.ToPoint();
                var f = BuildLogisticFunction(L, k, x0);
                return denom / Observations.Select((obs, n) => Math.Pow(f(n) - obs, 2.0)).Sum();
            };
        }

        private static Func<int, double> BuildLogisticFunction(double L, double k, double x0)
        {
            return x => L / (1.0 + Math.Exp(k * (x0 - x)));
        }
    }
}
