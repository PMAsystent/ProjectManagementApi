using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.AddUserToProject
{
    public class AddUserToProjectCommandHandler : IRequestHandler<AddUserToProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddUserToProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
        {
            // TODO: Move to validator 
            ProjectRole role;
            ProjectMemberType memberType;
            
            if (!Enum.TryParse(request.ProjectRole, out role))
            {
                throw new ArgumentException("Wrong project role");
            }
            if (!Enum.TryParse(request.MemberType, out memberType))
            {
                throw new ArgumentException("Wrong member type");
            }
            if (await UserAlreadyAssigned(request.UserId, request.ProjectId))
            {
                throw new Exception("User already assigned");
            }
            
            var projectAssignment = _mapper.Map<ProjectAssignment>(request);
            await _context.ProjectAssignments.AddAsync(projectAssignment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        private async Task<bool> UserAlreadyAssigned(int userId, int projectId)
        {
            var assigment = await _context.ProjectAssignments
                .FirstOrDefaultAsync(a => a.ProjectId == projectId && a.UserId == userId);

            return assigment != null;
        }
    }
}