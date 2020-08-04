using C.O.S.E.C.Domain.Entity;
using System;
using System.Threading.Tasks;

namespace C.O.S.E.C.Domain.InterfaceDrivers.Business
{
    public interface IUserInfoBLL : IBaseBLL<UserInfo>
    {
        UserInfo CheckLogin(string username, string password);
        UserInfo GetEntityForAccount(string account);
        Task<bool> RevisePasswordAsync(Guid keyValue, string Password);
        bool UpdateState(Guid keyValue, int State);
    }
}
