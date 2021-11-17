﻿using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;
using ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep;
using ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagement.Core.UseCases.Steps.Queries.GetStepById;
using ProjectManagement.Core.UseCases.Steps.Queries.GetStepByProjectId;
using ProjectManagement.Core.UseCases.Steps.Queries.GetSteps;
using ProjectManagement.Core.UseCases.Steps.ViewModels;


namespace ProjectManagementApi.Controllers
{
    public class StepController : ApiControllerBase
    {
        // [HttpGet]
        // public async Task<ActionResult<StepVm>> GetAllSteps()
        // {
        //     return await Mediator.Send(new GetStepsQuery());
        // }
        //
        // [HttpGet("{id}")]
        // public async Task<ActionResult<StepDto>> GetStepById(int id)
        // {
        //     var getStepByIdQuery = new GetStepByIdQuery()
        //     {
        //         StepId = id
        //     };
        //
        //     return await Mediator.Send(getStepByIdQuery);
        // }
        //
        // [HttpGet("Project/{projectId}")]
        // public async Task<ActionResult<StepVm>> GetStepsByProjectId(int projectId)
        // {
        //     var getStepByProjectIdQuery = new GetStepsByProjectIdQuery()
        //     {
        //         ProjectId = projectId
        //     };
        //
        //     return await Mediator.Send(getStepByProjectIdQuery);
        // }
        //
        [HttpPost]
        public async Task<ActionResult<Step>> AddStep([FromBody] CreateStepCommand createStepCommand)
        {
             var result = await Mediator.Send(createStepCommand);
             return Ok(result.Step);
        }
        
        [HttpPut(Name = "UpdateStep")]
        public async Task<ActionResult> Update([FromBody] UpdateStepCommand updateStepCommand)
        {
            var result = await Mediator.Send(updateStepCommand);
            return Ok(result.Step);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromQuery] bool moveTasks)
        {
            var deleteStepCommand = new DeleteStepCommand()
            {
                StepId = id,
                MoveTasks = moveTasks
            };
            await Mediator.Send(deleteStepCommand);
        
            return Ok();
        }
    }
}
