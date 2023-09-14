using Application.Abstractions.Messaging;
using Application.Queries.Users.GetUserByEmailQuery;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetAllUserQuerry
{
    public class GetAllUserQueryHandler : IQueryHandler<GetAllUserRequest, IList<GetAllUserUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IList<GetAllUserUserResponse>>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync();

            var data = users.Adapt<IList<GetAllUserUserResponse>>();

            return await Result<IList<GetAllUserUserResponse>>.SuccessAsync(data, "success");
        }
    }
}
