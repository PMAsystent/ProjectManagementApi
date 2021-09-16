﻿using Domain.Base;
using System;

namespace Domain.Entities
{
    public class TaskAssigment : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
