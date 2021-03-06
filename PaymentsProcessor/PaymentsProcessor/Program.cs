﻿using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using PaymentsProcessor.Actors;
using PaymentsProcessor.ExternalSystems;
using PaymentsProcessor.Messages;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PaymentsProcessor
{
    class Program
    {
        private static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            CreateActorSystem();

            IActorRef jobCoordinator = ActorSystem.ActorOf<JobCoordinatorActor>("JobCoordinator");

            PeakTimeDemoSimulator.StartDemo(6);

            jobCoordinator.Tell(new ProcessFileMessage("file1.csv"));

            Task.WaitAll(ActorSystem.WhenTerminated);

            Console.WriteLine($"Job complete");
            Console.ReadKey();

        }

        private static void CreateActorSystem()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DemoPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PaymentWorkerActor>();
            var container = builder.Build();

            ActorSystem = ActorSystem.Create("PaymentProcessing");

            new AutoFacDependencyResolver(container, ActorSystem);
        }
    }
}
