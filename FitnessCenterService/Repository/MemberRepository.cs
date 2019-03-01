using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FitnessCenterService.Interface;
using FitnessCenterModel.DTO;
using FitnessCenterModel;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace FitnessCenterService.Repository
{
    public class MemberRepository : IMemberRepository<Member>
    {
        SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["FitnessCenterConnectionString"].ConnectionString);
        /// <summary>
        /// 檢查會員是否存在
        /// </summary>
        /// <param name="Account">Email</param>
        /// <returns></returns>
        public bool CheckifExist(string Account)
        {
            con.Open();
            var para = new DynamicParameters();
            var User = new Member();
            try
            {
                para.Add("@Email", Account, DbType.String, ParameterDirection.Input, size: 100);
                User = con.Query<Member>(@"select * from FitnessCenter.dbo.Member
                           where Email=@Email", para, commandTimeout: 300, commandType: CommandType.Text).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                con.Close();
            }
            
            if (User != null)
                return true;
            else
                return false;
                
            
        }

        public Member GetMemberInfo(string MemberId)
        {
            con.Open();
            var para = new DynamicParameters();
            var User = new Member();
            try
            {
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 100);
                User = con.Query<Member>(@"select * from FitnessCenter.dbo.Member
                           where MemberId=@MemberId", para, commandTimeout: 300, commandType: CommandType.Text).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return User;


        }

        

        public LoginResult<User> Login(string Account, string Password)
        {
            var result = new LoginResult<User>();
            var para = new DynamicParameters();
            con.Open();
            var User = new User();
            try
            {
                para.Add("@Email", Account, DbType.String, ParameterDirection.Input, size: 100);
                para.Add("@Password", Password, DbType.String, ParameterDirection.Input, size: 200);
                User = con.Query<User>(@"select MemberId,Name,Email,Phone,State from FitnessCenter.dbo.Member
                           where Email=@Email and Password=@Password", para, commandTimeout: 300, commandType: CommandType.Text).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            if (User != null)
            {
                result.ReturnNo = 1;
                result.Message = "登入成功";
                result.Memberinfo = User;
            }
            else
            {
                result.ReturnNo = -99;
                result.Message = "登入失敗";
            }
            return result;
        }

        public Result Register(Member User)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            para.Add("@MemberId", User.MemberId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Name",User.Name, DbType.String, ParameterDirection.Input, 50);
            para.Add("@NickName", User.NickName, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Gender", User.Gender, DbType.Int32, ParameterDirection.Input,1);
            para.Add("@Password", User.Password, DbType.String, ParameterDirection.Input, 200);
            para.Add("@Email", User.Email, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Phone", User.Phone, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Birthday", User.Birthday, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@Address", User.Address, DbType.String, ParameterDirection.Input, 50);
            try
            {
                con.Execute(@"Insert into Member(MemberId ,Name ,NickName ,Gender ,Password ,Email ,Phone ,Birthday ,Address,State)
                     values(@MemberId,@Name,@NickName,@Gender,@Password,@Email,@Phone,@Birthday,@Address,0)",
                     para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "註冊成功";
            return result;
        }
        public Result Update(Member User)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            
            para.Add("@Name", User.Name, DbType.String, ParameterDirection.Input, 50);
            para.Add("@NickName", User.NickName, DbType.String, ParameterDirection.Input, 50);
            
            para.Add("@Gender", User.Gender, DbType.Int32, ParameterDirection.Input, 1);
            para.Add("@Phone", User.Phone, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Birthday", User.Birthday, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@Address", User.Address, DbType.String, ParameterDirection.Input, 50);
            para.Add("@MemberId", User.MemberId, DbType.String, ParameterDirection.Input, 50);
            try
            {
                con.Execute(@"update Member set Name=@Name,NickName=@NickName,Gender=@Gender,Phone=@Phone,Birthday=@Birthday,Address=@Address
                              where MemberId=@MemberId",
                     para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally
            {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "更新會員資料成功";
            return result;
            
        }
        #region 上傳圖片
        public Result InsertPhoto(string MemberId, string ImageName)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            { //先刪除再新增
                para.Add("@ImageName", ImageName, DbType.String, ParameterDirection.Input, size: 5000);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 5000);
                con.Execute(@"
                             Insert into MemberImage(MemberId ,ImageName)
                              values(@MemberId, @ImageName)", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally
            {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "新增成功";
            return result;
        }
        public Result UpdatePhoto(string MemberId, string ImageName)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 5000);
                para.Add("@ImageName", ImageName, DbType.String, ParameterDirection.Input, size: 5000);
                con.Execute(@"update MemberImage  set ImageName=@ImageName where MemberId=@MemberId", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally
            {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "新增成功";
            return result;
        }
        public string GetUserImage(string MemberId)
        {
            con.Open();
            var para = new DynamicParameters();
            string imageName = string.Empty;
            try
            {
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 5000);
                imageName= con.Query<string>(@"select ImageName from MemberImage where MemberId=@MemberId", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return imageName;

        }
        #endregion
        #region 上課地區新增/修改
        public Result InsertUserArea(string MemberId, string Area)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Area", Area, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into MemberArea(MemberId ,Area)
                              values(@MemberId, @Area)", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally
            {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "新增成功";
            return result;
        }
        public Result DeleteUserArea(string MemberId,string Area)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Area", Area, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             delete MemberArea where MemberId=@MemberId and Area=@Area", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text);

            }
            catch (Exception ex)
            {
                result.ReturnNo = -99;
                result.Message = ex.ToString();
                return result;
            }
            finally
            {
                con.Close();
            }
            result.ReturnNo = 1;
            result.Message = "刪除成功";
            return result;
        }
        public List<string> GetUserArea(string MemberId)
        {
            var result = new List<string>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<string>(@"
                             select Area from MemberArea where MemberId=@MemberId", para,
                   commandTimeout: 300,
                   commandType: CommandType.Text).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return result;
        }
        #endregion

    }
}