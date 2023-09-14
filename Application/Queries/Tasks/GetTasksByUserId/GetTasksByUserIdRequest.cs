using Application.Abstractions.Messaging;
using Application.Queries.Users.GetUserByEmailQuery;
using Domain.Contracts.Repositories;
using Domain.Enums;
using Domain.Wrapper;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Tasks.GetTasksByUserId
{

    public class GetTasksByUserIdQuerryHandler : IQueryHandler<GetTasksByUserIdRequest, IList<GetTasksByUserIdResponse>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksByUserIdQuerryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<IList<GetTasksByUserIdResponse>>> Handle(GetTasksByUserIdRequest request, CancellationToken cancellationToken)
        {
            if(request.userId != Guid.Empty)
            {
                var tasks = await _taskRepository.GetAsync(request.userId);
                var data = tasks.Adapt<IList<GetTasksByUserIdResponse>>();
                return await Result<IList<GetTasksByUserIdResponse>>.SuccessAsync(data, "Success");
            }
            return await Result<IList<GetTasksByUserIdResponse>>.FailAsync($"Error Fetching Task, Check your details");
        }
    }

    public sealed record GetTasksByUserIdRequest(Guid userId) : IQuery<IList<GetTasksByUserIdResponse>>
    {
    }

    public sealed record GetTasksByUserIdResponse(Guid taskId, string tittle, Guid userId, string description, DateTime dueDate, Priority priority, Status status)
    {
    }

    
}
