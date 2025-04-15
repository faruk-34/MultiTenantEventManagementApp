using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UsersVM>> Register(RequestUsers request, CancellationToken cancellationToken);
        Task<Response<LoginVM>> Login(RequestLogin request,CancellationToken cancellationToken);
    }
}
