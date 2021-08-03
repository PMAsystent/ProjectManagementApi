﻿using MediatR;
using ProjectManagement.Core.UseCases.Bosses.Dto;

namespace ProjectManagement.Core.UseCases.Bosses.Queries.GetBossById
{
    public class GetBossByIdQuery : IRequest<DetailedBossDto>
    {
        public int BossId { get; set; }
    }
}
