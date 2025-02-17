﻿namespace CrystalQuartz.Application.Commands
{
    using System.Threading.Tasks;
    using CrystalQuartz.Core.Domain.Events;
    using Inputs;

    public class PauseGroupCommand : AbstractOperationCommand<GroupInput>
    {
        public PauseGroupCommand(ISchedulerHostProvider schedulerHostProvider) : base(schedulerHostProvider)
        {
        }

        protected override async Task PerformOperation(GroupInput input)
        {
            await SchedulerHost.Commander.PauseJobGroup(input.Group);

            RiseEvent(new RawSchedulerEvent(SchedulerEventScope.Group, SchedulerEventType.Paused, input.Group, null));
        }
    }
}