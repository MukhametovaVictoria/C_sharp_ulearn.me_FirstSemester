using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            for (int i = 16; i <= 512; i*=2)
            {
                var runner1 = new ClassArrayCreationTask(i);
                classesTimes.Add(Build(runner1, i, benchmark, repetitionsCount));
                var runner2 = new StructArrayCreationTask(i);
                structuresTimes.Add(Build(runner2, i, benchmark, repetitionsCount));
            }
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            for (int i = 16; i <= 512; i *= 2)
            {
                var runner1 = new MethodCallWithClassArgumentTask(i);
                classesTimes.Add(Build(runner1, i, benchmark, repetitionsCount));
                var runner2 = new MethodCallWithStructArgumentTask(i);
                structuresTimes.Add(Build(runner2, i, benchmark, repetitionsCount));
            }
            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ExperimentResult Build(ITask task, int i, IBenchmark benchmark, int repetitionsCount)
        {
            var case1 = benchmark.MeasureDurationInMs(task, repetitionsCount);
            var experimentResult1 = new ExperimentResult(i, case1);

            return experimentResult1;
        }
    }
}