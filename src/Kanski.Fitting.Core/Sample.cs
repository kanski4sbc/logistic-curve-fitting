﻿using System;
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
            var selection = new RouletteWheelSelection();
            var crossover = new UniformCrossover(0.5f);
            var mutation = new FlipBitMutation();
            var chromosome = new FloatingPointChromosome(
                new double[] { 0, 0 },
                new double[] { 2, 2 },
                new int[] { 63, 63 },
                new int[] { 17, 17 }
            );
            var population = new Population(500, 700, chromosome);
            var fitness = new FuncFitness(ProblemDomain.Fitness);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(5000);

            ga.MutationProbability = 0.1f;
            ga.CrossoverProbability = 0.7f;


            ga.GenerationRan += (s,a) =>
            {
                if (0 == ga.GenerationsNumber % 100)
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
