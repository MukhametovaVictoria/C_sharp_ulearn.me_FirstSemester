using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.

            Stopwatch stopWatch = new Stopwatch();
            task.Run();
            stopWatch.Start();
            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            stopWatch.Stop();
            TimeSpan tms = stopWatch.Elapsed;
            return tms.TotalMilliseconds / repetitionCount;
			//throw new NotImplementedException();
		}
	}
    public class NewStringBuilder : ITask
    {
        public void Run()
        {
            var str = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                str.Append('a');
            }
            var newStr = str.ToString();
        }

    }

    public class StringCreator : ITask
    {
        public void Run()
        {
            var str = new string('a', 10000);
        }

    }


    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var runner1 = new NewStringBuilder();
            var runner2 = new StringCreator();
            var benchmark = new Benchmark();
            var repetitionsCount = 50;
            var case1 = benchmark.MeasureDurationInMs(runner1, repetitionsCount);
            var case2 = benchmark.MeasureDurationInMs(runner2, repetitionsCount);
            Assert.Less(case2, case1);

            //throw new NotImplementedException();
        }
    }
}