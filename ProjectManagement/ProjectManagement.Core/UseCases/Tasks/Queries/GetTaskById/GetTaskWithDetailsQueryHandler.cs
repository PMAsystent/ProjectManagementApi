using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById
{
    public class GetTaskWithDetailsQueryHandler : IRequestHandler<GetTaskWithDetailsQuery, DetailedTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTaskWithDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DetailedTaskDto> Handle(GetTaskWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.TaskId);
            if (task == null)
            {
                throw new NotFoundException(nameof(Task), request.TaskId);
            }

            var taskToReturn = _mapper.Map<DetailedTaskDto>(task);

            return taskToReturn;
        }
    }
}
