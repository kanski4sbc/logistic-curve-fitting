using System;
using System.Diagnostics.Contracts;
using GeneticSharp.Domain.Chromosomes;

namespace Kanski.Fitting.Core
{
    public static class ProblemDomain
    {
        public static (double x, double y) ToPoint(this IChromosome chromosome)
        {
            Contract.Assert(null != chromosome);
            var values = ((FloatingPointChromosome)chromosome).ToFloatingPoints();
            Contract.Assert(2 == values.Length);
            return (x: values[0], y: values[1]);
        }

        public static double Fitness(this IChromosome chromosome)
        {
            (var x, var y) = chromosome.ToPoint();
            return Math.Exp((1 - x) * (x - 1)) + Math.Exp((1 - y) * (y - 1));
        }

    }
}
