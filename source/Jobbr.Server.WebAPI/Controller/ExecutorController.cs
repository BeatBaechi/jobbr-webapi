﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jobbr.Common.Model;
using Jobbr.Server.Common;
using Jobbr.Server.Core;
using Jobbr.Shared;
using Newtonsoft.Json;

namespace WebAPI.Controller
{
    /// <summary>
    /// The job run controller.
    /// </summary>
    public class ExecutorController : ApiController
    {
        private readonly IStateService stateService;

        private readonly IJobStorageProvider jobStorageProvider;

        private readonly IArtefactsStorageProvider artefactsStorageProvider;

        private readonly IJobManagementService jobManagementService;

        public ExecutorController(IStateService stateService, IJobStorageProvider jobStorageProvider, IArtefactsStorageProvider artefactsStorageProvider, IJobManagementService jobManagementService)
        {
            this.stateService = stateService;
            this.jobStorageProvider = jobStorageProvider;
            this.artefactsStorageProvider = artefactsStorageProvider;
            this.jobManagementService = jobManagementService;
        }

        [HttpGet]
        [Route("client/jobrun/{jobRunId}")]
        public IHttpActionResult GetJonbRunInfos(long jobRunId)
        {
            var jobRun = this.jobManagementService.GetJobRun(jobRunId);

            if (jobRun == null)
            {
                return this.NotFound();
            }

            var trigger = this.jobStorageProvider.GetTriggerById(jobRun.TriggerId);
            var job = this.jobStorageProvider.GetJobById(jobRun.JobId);

            var infoDto = new JobRunInfoDto
                              {
                                  JobId = job.Id,
                                  TriggerId = trigger.Id,
                                  UserId = trigger.UserId,
                                  UserName = trigger.UserName,
                                  JobRunId = jobRunId,
                                  JobName = job.UniqueName,
                                  JobType = job.Type,
                                  TempDir = jobRun.TempDir,
                                  WorkingDir = jobRun.WorkingDir,
                                  UniqueId = new Guid(jobRun.UniqueId),
                                  JobParameter = jobRun.JobParameters != null ? JsonConvert.DeserializeObject(jobRun.JobParameters) : null,
                                  InstanceParameter = jobRun.InstanceParameters != null ? JsonConvert.DeserializeObject(jobRun.InstanceParameters) : null,
                              };
            
            return this.Ok(infoDto);
        }

        [HttpPut]
        [Route("client/jobrun/{jobRunId}")]
        public IHttpActionResult PutJobRunUpdate(long jobRunId, [FromBody] JobRunUpdateDto dto)
        {
            var jobRun = this.jobManagementService.GetJobRun(jobRunId);

            if (jobRun == null)
            {
                return this.NotFound();
            }

            if (dto.State != JobRunState.Null)
            {
                this.stateService.UpdateJobRunState(jobRun, dto.State);
            }

            return this.StatusCode(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("client/jobrun/{jobRunId}/artefacts")]
        public IHttpActionResult AddArtefacts(long jobRunId)
        {
            var jobRun = this.jobManagementService.GetJobRun(jobRunId);

            if (jobRun == null)
            {
                return this.NotFound();
            }

            IEnumerable<HttpContent> parts = this.Request.Content.ReadAsMultipartAsync().Result.Contents;

            foreach (var part in parts)
            {
                var contentDisposition = part.Headers.ContentDisposition;

                var result = part.ReadAsStreamAsync().Result;

                this.artefactsStorageProvider.Save(jobRun.UniqueId, contentDisposition.FileName, result);
            }

            return this.StatusCode(HttpStatusCode.Accepted);
        }
    }
}