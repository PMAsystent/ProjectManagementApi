using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment
{
    public class UpdateProjectAssignmentCommandHandler : IRequestHandler<UpdateProjectAssignmentCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProjectAssignmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProjectAssignmentCommand request, CancellationToken cancellationToken)
        {
            var projectAssignment = await _context.ProjectAssignments
                .SingleOrDefaultAsync(a =>
                        a.UserId == request.UserId &&
                        a.ProjectId == request.ProjectId,
                    cancellationToken);
            
            if (projectAssignment == null)
            {
                throw new NotFoundException(
                    $"{nameof(ProjectAssignment)} with user({request.UserId} in project({request.ProjectId}) does not exists.");
            }

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

            _mapper.Map(request, projectAssignment);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}