using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FitnessCenterService.Interface;
using FitnessCenterModel.DTO;
using FitnessCenterModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace FitnessCenterService.Repository
{
    public class CoachRepository : ICoachRepository<Coach>
    {
        SqlConnection con = new SqlConnection(
              WebConfigurationManager.ConnectionStrings["FitnessCenterConnectionString"].ConnectionString);
        public bool CheckifExist(string Account)
        {
            con.Open();
            var para = new DynamicParameters();
            var User = new Coach();
            try
            {
                para.Add("@Email", Account, DbType.String, ParameterDirection.Input, size: 100);
                User = con.Query<Coach>(@"select * from FitnessCenter.dbo.Coach
                           where Email=@Email", para, commandTimeout: 300, commandType: CommandType.Text).FirstOrDefault();
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
                return true;
            else
                return false;
        }

        public Coach GetMemberInfo(string MemberId)
        {
            con.Open();
            var para = new DynamicParameters();
            var User = new Coach();
            try
            {
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 100);
                User = con.Query<Coach>(@"select * from Coach where MemberId=@MemberId", para, commandTimeout: 300, commandType: CommandType.Text).FirstOrDefault();
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
                para.Add("@Password", Password, DbType.String, ParameterDirection.Input, size: 100);
                User = con.Query<User>(@"select MemberId,Name,Email,Phone,State from FitnessCenter.dbo.Coach
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

        public Result Register(Coach User)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            para.Add("@MemberId", User.MemberId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Name", User.Name, DbType.String, ParameterDirection.Input, 50);
            para.Add("@NickName", User.NickName, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Gender", User.Gender, DbType.Int32, ParameterDirection.Input, 1);
            para.Add("@Password", User.Password, DbType.String, ParameterDirection.Input, 200);
            para.Add("@Email", User.Email, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Phone", User.Phone, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Birthday", User.Birthday, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@Address", User.Address, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Intoduction", User.Intoduction, DbType.String, ParameterDirection.Input, 600);
            para.Add("@LineID", User.LineID, DbType.String, ParameterDirection.Input, 50);
            para.Add("@FaceBook", User.FaceBook, DbType.String, ParameterDirection.Input, 200);
            para.Add("@Instagram", User.Instagram, DbType.String, ParameterDirection.Input, 200);
            try
            {
                con.Execute(@"Insert into Coach(MemberId ,Name ,NickName ,Gender ,Password ,Email ,Phone ,Birthday ,Address,State,Intoduction,LineID,FaceBook,Instagram)
                     values(@MemberId,@Name,@NickName,@Gender,@Password,@Email,@Phone,@Birthday,@Address,0,@Intoduction,@LineID,@FaceBook,@Instagram)",
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
            result.Message = "註冊成功";
            return result;
        }
        public Result Update(Coach User)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            para.Add("@MemberId", User.MemberId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Name", User.Name, DbType.String, ParameterDirection.Input, 50);
            para.Add("@NickName", User.NickName, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Phone", User.Phone, DbType.String, ParameterDirection.Input, 100);
            para.Add("@Birthday", User.Birthday, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@Address", User.Address, DbType.String, ParameterDirection.Input, 50);
            para.Add("@Intoduction", User.Intoduction, DbType.String, ParameterDirection.Input, 600);
            para.Add("@LineID", User.LineID, DbType.String, ParameterDirection.Input, 50);
            para.Add("@FaceBook", User.FaceBook, DbType.String, ParameterDirection.Input, 200);
            para.Add("@Instagram", User.Instagram, DbType.String, ParameterDirection.Input, 200);
            try
            {
                con.Execute(@"update Coach set Name=@Name,NickName=@NickName,Phone=@Phone,Birthday=@Birthday,Address=@Address,Intoduction=@Intoduction,LineID=@LineID,FaceBook=@FaceBook,Instagram=@Instagram
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
            {
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 5000);
                para.Add("@ImageName", ImageName, DbType.String, ParameterDirection.Input, size: 5000);
                con.Execute(@"Insert into CoachImage(MemberId ,ImageName)
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
                con.Execute(@"update CoachImage  set ImageName=@ImageName where MemberId=@MemberId", para,
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
                imageName = con.Query<string>(@"select ImageName from CoachImage where MemberId=@MemberId", para,
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
                             Insert into CoachArea(MemberId ,Area)
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
                con.Execute(@"delete CoachArea where MemberId=@MemberId and Area=@Area", para,
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
                             select Area from CoachArea where MemberId=@MemberId", para,
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
        #region 訓練項目
        public Result InsertTrainingProgram(string MemberId, int TrainingProgram)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@TrainingProgramId", TrainingProgram, DbType.Int32, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into CoachTrainingProgram(MemberId ,TrainingProgramId)
                              values(@MemberId, @TrainingProgramId)", para,
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
        public Result DeleteTrainingProgram(string MemberId,int TrainingProgram)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@TrainingProgramId", TrainingProgram, DbType.Int32, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"delete CoachTrainingProgram where MemberId=@MemberId and TrainingProgramId=@TrainingProgramId", para,
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
        
        public List<CoachTrainingProgram> GetTrainingProgram(string MemberId)
        {
            var result = new List<CoachTrainingProgram>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<CoachTrainingProgram>(@"
                             select * from ViewTrainingProgram where MemberId=@MemberId", para,
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
        #region 選課形式
        public Result InsertCoachCourse(string MemberId, int Course)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@CourseId", Course, DbType.Int32, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into CoachCourse(MemberId ,CourseId)
                              values(@MemberId, @CourseId)", para,
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
        public Result DeleteCoachCourse(string MemberId, int Course)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@CourseId", Course, DbType.Int32, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"delete CoachCourse where MemberId=@MemberId and CourseId=@CourseId", para,
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
        
        public List<CoachCourse> GetCoachCourse(string MemberId)
        {
            var result = new List<CoachCourse>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<CoachCourse>(@"
                             select * from ViewCoachCourse where MemberId=@MemberId", para,
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
        #region 證照
        public Result InsertCoachLicense(string MemberId, string License)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@License", License, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into CoachLicense(MemberId ,License)
                              values(@MemberId, @License)", para,
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
        public Result DeleteCoachLicense(string MemberId, string License)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@License", License, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"delete CoachLicense where MemberId=@MemberId and License=@License", para,
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
        public List<string> GetCoachLicense(string MemberId)
        {
            var result = new List<string>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<string>(@"
                             select License from CoachLicense where MemberId=@MemberId", para,
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
        #region 經歷
        public Result InsertCoachExperience(string MemberId, string Experience)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Experience", Experience, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into CoachExperience(MemberId ,Experience)
                              values(@MemberId, @Experience)", para,
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
        public Result DeleteCoachExperience(string MemberId, string Experience)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Experience", Experience, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"delete CoachExperience where MemberId=@MemberId and Experience=@Experience", para,
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
        public List<string> GetCoachExperience(string MemberId)
        {
            var result = new List<string>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<string>(@"
                             select Experience from CoachExperience where MemberId=@MemberId", para,
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
        #region 比賽經驗
        public Result InsertCoachCompetiton(string MemberId, string Competiton)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Competiton", Competiton, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"
                             Insert into CoachCompetiton(MemberId ,Competiton)
                              values(@MemberId, @Competiton)", para,
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
        public Result DeleteCoachCompetiton(string MemberId, string Competiton)
        {
            var result = new Result();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                para.Add("@Competiton", Competiton, DbType.String, ParameterDirection.Input, size: 50);
                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                con.Execute(@"delete CoachCompetiton where MemberId=@MemberId and Competiton=@Competiton", para,
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
        public List<string> GetCoachCompetiton(string MemberId)
        {
            var result = new List<string>();
            con.Open();
            var para = new DynamicParameters();
            try
            {

                para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                result = con.Query<string>(@"
                             select Competition from CoachCompetiton where MemberId=@MemberId", para,
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