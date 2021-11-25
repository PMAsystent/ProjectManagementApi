using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
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
            ProjectRole role;
            ProjectMemberType memberType;
            
            if (Enum.TryParse(request.ProjectRole, out role))
            {
                throw new ArgumentException("Wrong project role");
            }
            if (Enum.TryParse(request.MemberType, out memberType))
            {
                throw new ArgumentException("Wrong member type");
            }
            
            var projectAssignment = _mapper.Map<ProjectAssignment>(request);
            await _context.ProjectAssignments.AddAsync(projectAssignment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}