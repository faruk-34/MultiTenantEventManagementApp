using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        Task<Response<UsersVM>> Get(int id, CancellationToken cancellationToken);
        Task<Response<UsersVM>> Update(RequestUsers request, CancellationToken cancellationToken);
        Task<IEnumerable<UsersVM>> GetAllByTenantAsync(int tenantId,CancellationToken cancellationToken);
    }
}
