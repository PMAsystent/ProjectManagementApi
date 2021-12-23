using System;
using MediatR;

namespace ProjectManagement.Core.UseCases.Users.Queries.CheckIfUserWithEmailExistsQuery
{
    public class CheckIfUserWithEmailExistsQuery : IRequest<bool>
    {
        public string Email { get; }

        public CheckIfUserWithEmailExistsQuery(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty");
            }
            Email = email;
        }
    }
}