﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetSteps
{
    public class GetStepsQuery : IRequest<StepVm> { }
}