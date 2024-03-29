﻿using Domain.Events;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class RegisterUserCommand : IRequest<RegisterResponseDto>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public RegisterUserCommandHandler(IDomainEventService domainEventService, IIdentityService identityService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<RegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email, Id) = await _identityService.RegisterUserAsync(request.Email, request.UserName, request.Password);

            if (Result.Succeeded)
            {
                await _domainEventService.Publish(new UserRegisteredEvent(Id, request.UserName, request.Email));
            }

            return new RegisterResponseDto { UserName = UserName, Email = Email, Errors = new List<string>(Result.Errors) };
        }
    }
}
