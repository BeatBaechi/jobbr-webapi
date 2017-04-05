﻿using System;
using System.Collections.Generic;

namespace Jobbr.WebAPI.Common.Models
{
    /// <summary>
    /// The job run dto.
    /// </summary>
    public class JobRunDto
    {
        public long JobId { get; set; }

        public long TriggerId { get; set; }

        public long JobRunId { get; set; }

        public object JobParameter { get; set; }

        public object InstanceParameter { get; set; }

        public string JobName { get; set; }

        public string State { get; set; }

        public double? Progress { get; set; }

        public DateTime PlannedStartUtc { get; set; }

        public DateTime? AuctualStartUtc { get; set; }

        public DateTime? EstimatedEndtUtc { get; set; }

        public DateTime? AuctualEndUtc { get; set; }

        public List<JobRunArtefactDto> Artefacts { get; set; }

        public string JobTitle { get; set; }
    }

    public class JobRunArtefactDto
    {
        public string Filename { get; set; }
        public long Size { get; set; }
    }
}
