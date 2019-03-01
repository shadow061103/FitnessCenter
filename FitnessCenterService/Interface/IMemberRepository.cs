using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterModel;
using FitnessCenterModel.DTO;

namespace FitnessCenterService.Interface
{
    public interface IMemberRepository<T>
    {
        bool CheckifExist(string Account);
        Result Register(T User);
        Result Update(T User);
        LoginResult<User> Login(string Account, string Password);

        T GetMemberInfo(string Account);
        Result InsertPhoto(string MemberId, string ImageName);
        Result UpdatePhoto(string MemberId, string ImageName);

        string GetUserImage(string MemberId);
        Result InsertUserArea(string MemberId, string Area);
        Result DeleteUserArea(string MemberId,string Area);
        List<string> GetUserArea(string MemberId);



    }
}