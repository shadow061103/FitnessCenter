using FitnessCenterAPI.Factory;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterService.Repository;
namespace FitnessCenterAPI.Strategy
{
    public class MemberStrategy //K:Repository T Member or Coach
    { 
        IMemberRepository<Member> memberRepository;
        public MemberStrategy()
        {
            //不確定是Member還是Coach K:repository型別
            memberRepository = GenericFactory.CreateInastance<IMemberRepository<Member>>(typeof(MemberRepository));
        }
        public bool CheckifExist(string Account)
        {
            return memberRepository.CheckifExist(Account);
        }
        public Member GetMemberInfo(string MemberId)
        {
            return memberRepository.GetMemberInfo(MemberId);
        }
        public Result Register(Member User)
        {
            return memberRepository.Register(User);

        }
        public LoginResult<User> Login(string Account, string Password)
        {
            return memberRepository.Login(Account, Password);
        }
        public Result Update(Member User)
        {
            return memberRepository.Update(User);
        }
        public Result InsertUserPhoto(string MemberId,string ImageName)
        {
            return memberRepository.InsertPhoto(MemberId, ImageName);
        }
        public Result UpdateUserPhoto(string MemberId, string ImageName)
        {
            return memberRepository.UpdatePhoto(MemberId, ImageName);
        }
        public string GetUserImage(string MemberId)
        {
            return memberRepository.GetUserImage(MemberId);
        }
        public Result InsertUserArea(string MemberId, string Area)
        {
            return memberRepository.InsertUserArea(MemberId, Area);
        }
        public Result DeleteUserArea(string MemberId,string Area)
        {
            return memberRepository.DeleteUserArea(MemberId, Area);
        }
        public List<string> GetUserArea(string MemberId)
        {
            return memberRepository.GetUserArea(MemberId);
        }


    }
}