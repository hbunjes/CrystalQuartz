﻿namespace CrystalQuartz.Application.Tests.Commands
{
    using System.Threading.Tasks;
    using Application.Commands;
    using Application.Commands.Inputs;
    using Application.Commands.Outputs;
    using CrystalQuartz.Application.Tests.Stubs;
    using CrystalQuartz.Stubs;
    using NUnit.Framework;

    [TestFixture]
    public class GetAllowedJobTypesCommandTests
    {
        [Test]
        public async Task Execute_HasAllowedJobTypes_ShouldReturnAllowedJobTypes()
        {
            var stub = new SchedulerHostStub(new[] { typeof(string) });

            var result = (JobTypesOutput) await new GetAllowedJobTypesCommand(stub).Execute(new NoInput());

            result.AssertSuccessfull();

            Assert.That(result.AllowedTypes, Is.Not.Null);
            Assert.That(result.AllowedTypes.Length, Is.EqualTo(1));
            Assert.That(result.AllowedTypes[0], Is.EqualTo(typeof(string)));
        }
    }
}