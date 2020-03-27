using System;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace Kanski.Fitting.Core
{
    public static class Sample
    {
        public static void Test()
        {
            var selection = new EliteSelection();
            var crossover = new UniformCrossover(0.5f);
            var mutation = new PartialShuffleMutation();
            var chromosome = new FloatingPointChromosome(
                new double[] { 1000, 0, 22 },
                new double[] { 100000, 1, 50 },
                new int[] { 32, 63, 19 },
                new int[] { 0, 17, 4 }
            );
            //L, k, x0

            var population = new Population(1000, 1100, chromosome);
            var fitness = new FuncFitness(ProblemDomain.Fitness());

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(1000);

            ga.MutationProbability = 0.3f;
            ga.CrossoverProbability = 0.7f;


            ga.GenerationRan += (s, a) =>
            {
                if (0 == ga.GenerationsNumber % 10)
                {
                    Console.WriteLine("Best solution so far {0} for {1}", ga.BestChromosome.Fitness, ga.BestChromosome.ToPoint());
                }

            };

            Console.WriteLine("GA running...");
            ga.Start();


            Console.WriteLine("Best solution found has {0} fitness for {1}", ga.BestChromosome.Fitness, ga.BestChromosome.ToPoint());

        }

    }
}
