using BenchmarkDotNet.Running;
using Performance;

_ = BenchmarkRunner.Run<HttpClientsComparison>();