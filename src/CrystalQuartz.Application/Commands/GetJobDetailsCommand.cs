﻿namespace CrystalQuartz.Application.Commands
{
    using System.Threading.Tasks;
    using CrystalQuartz.Core.Domain;
    using CrystalQuartz.Core.Domain.ObjectTraversing;
    using Inputs;
    using Outputs;

    public class GetJobDetailsCommand : AbstractSchedulerCommand<JobInput, JobDetailsOutput>
    {
        private readonly TraversingOptions _jobDataMapTraversingOptions;

        public GetJobDetailsCommand(
            ISchedulerHostProvider schedulerHostProvider,
            TraversingOptions jobDataMapTraversingOptions) : base(schedulerHostProvider)
        {
            _jobDataMapTraversingOptions = jobDataMapTraversingOptions;
        }

        protected override async Task InternalExecute(JobInput input, JobDetailsOutput output)
        {
            JobDetailsData? detailsData = await SchedulerHost.Clerk.GetJobDetailsData(input.Job, input.Group);
            if (detailsData != null)
            {
                var objectTraverser = new ObjectTraverser(_jobDataMapTraversingOptions);

                output.JobDetails = detailsData.JobDetails;
                output.JobDataMap = objectTraverser.Traverse(detailsData.JobDataMap);
            }
        }
    }
}