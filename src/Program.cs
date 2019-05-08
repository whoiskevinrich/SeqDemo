using System;
using System.Threading;
using RandomNameGeneratorLibrary;
using Serilog;

namespace SeqDemo
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "SeqDemo")
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();


            while (true)
            {
                DoWork();
                Thread.Sleep(1_000);
            }
        }

        private static void DoWork()
        {
            var demo = new Demo(new PersonNameGenerator(), new Random());

            var transaction = demo.RandomTransaction;
            Log.Information("{User} {Action} {Product} {Price}/{Cost}",
                transaction.User, transaction.Action,
                transaction.Product, transaction.Price, transaction.Cost);
        }
    }
}
