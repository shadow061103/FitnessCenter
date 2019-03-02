using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;
using FitnessCenterModel.DTO;
using System.Data;
using FitnessCenterService.Interface;
using FitnessCenterModel;
using FitnessCenterModel.Para;

namespace FitnessCenterService.Repository
{
    public class ReserveRepository: IReserveRepository
    {
        SqlConnection con = new SqlConnection(
              WebConfigurationManager.ConnectionStrings["FitnessCenterConnectionString"].ConnectionString);

        /// <summary>
        /// 訂單狀態列表
        /// </summary>
        /// <returns></returns>
        public List<ReserveStatus> GetOrderStatus()
        {
            con.Open();
            var para = new DynamicParameters();
            var list = new List<ReserveStatus>();
            try
            {

                list = con.Query<ReserveStatus>(@"select * from ReserveStatus ",
                    para, commandTimeout: 300,
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
            return list;
        }
        /// <summary>
        /// 預約面談
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result ReserveInterview(InteriewPara model)
        {

            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            para.Add("@CoachId", model.CoachId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@MemberId", model.MemberId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@StatusId",model.StatusId, DbType.Int32, ParameterDirection.Input, 50);
            para.Add("@ReserveDate", model.ReserveDate, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@Location", model.Location, DbType.String, ParameterDirection.Input, 200);
            para.Add("@Message", model.Message, DbType.String, ParameterDirection.Input, 100);
            
            try
            {
                con.Execute(@"Insert into ReserveInterView(CoachId,MemberId,StatusId,ReserveDate,Location,Message)
                             values(@CoachId,@MemberId,@StatusId,@ReserveDate,@Location,@Message)",
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
            result.Message = "新增訂單成功";
            return result;

        }
        public Result UpdateInterviewOrder(UpdateInterview model)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            string str = "Update ReserveInterView set StatusId=@StatusId where OrderId=@OrderId";
            para.Add("@OrderId", model.OrderId, DbType.Int32, ParameterDirection.Input, 50);
            para.Add("@StatusId", model.StatusId, DbType.Int32, ParameterDirection.Input, 50);
            if (!string.IsNullOrEmpty(model.Location))
            {
                para.Add("@Location", model.Location, DbType.String, ParameterDirection.Input, 20);
                str = "Update ReserveInterView set StatusId=@StatusId,Location=@Location where OrderId=@OrderId";
            }
            try
            {
                con.Execute(str,
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
            result.Message = "修改訂單成功";
            return result;
        }
        /// <summary>
        /// 預約服務
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result ReserveClass(ServicePara model)
        {

            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            para.Add("@CoachId", model.CoachId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@MemberId", model.MemberId, DbType.String, ParameterDirection.Input, 50);
            para.Add("@StatusId", model.StatusId, DbType.Int32, ParameterDirection.Input, 50);
            para.Add("@ServiceDate", model.ServiceDate, DbType.DateTime, ParameterDirection.Input, 50);
            para.Add("@TrainingProgramId", model.TrainingProgramId, DbType.Int32, ParameterDirection.Input, 200);
            para.Add("@CourseId", model.CourseId, DbType.Int32, ParameterDirection.Input, 100);
            para.Add("@Charge", model.Charge, DbType.Int32, ParameterDirection.Input, 100);
            para.Add("@Location", model.Location, DbType.String, ParameterDirection.Input, 200);
            try
            {
                con.Execute(@"Insert into ReserveService(CoachId,MemberId,StatusId,ServiceDate,TrainingProgramId,CourseId,Charge,Location)
                              values(@CoachId,@MemberId,@StatusId,@ServiceDate,@TrainingProgramId,@CourseId,@Charge,@Location)",
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
            result.Message = "新增訂單成功";
            return result;

        }
        public Result UpdateServiceOrder(UpdateServicePara model)
        {
            var result = new Result();
            var para = new DynamicParameters();
            con.Open();
            string str = "Update ReserveService set StatusId=@StatusId where OrderId=@OrderId";
            para.Add("@OrderId", model.OrderId, DbType.Int32, ParameterDirection.Input, 50);
            para.Add("@StatusId", model.StatusId, DbType.Int32, ParameterDirection.Input, 50);


            if (!string.IsNullOrEmpty(model.Location))
            {
                para.Add("@Location", model.Location, DbType.String, ParameterDirection.Input, 20);
                str = "Update ReserveService set StatusId=@StatusId,Location=@Location where OrderId=@OrderId";
            }
            try
            {
                con.Execute(str,
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
            result.Message = "修改訂單成功";
            return result;
        }
        /// <summary>
        /// 預約面談資料 
        /// </summary>
        /// <param name="MemberId">會員編號</param>
        /// <param name="Type">0學員1教練</param>
        /// <returns></returns>
        public List<Interview> InterViewList(string MemberId,int Type)
        {
            con.Open();
            var list = new List<Interview>();
            try
            {
                string str = "select * from FitnessCenter.dbo.ViewInterView where 1=1";
                var para = new DynamicParameters();
                if (Type == 0)
                {
                    para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                    str += " and MemberId=@MemberId";
                }
                else
                {
                    para.Add("@CoachId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                    str += " and CoachId=@CoachId and  StatusId <> 7";
                }
                str += " order by StatusId asc";
                list = con.Query<Interview>(str, para, commandTimeout: 300, commandType: CommandType.Text).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return list;


        }
        public List<CoachService> ServiceList(string MemberId, int Type)
        {
            con.Open();
            var list = new List<CoachService>();
            try
            {
                string str = "select * from FitnessCenter.dbo.ViewServie where 1=1";
                var para = new DynamicParameters();
                if (Type == 0)
                {
                    para.Add("@MemberId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                    str += " and MemberId=@MemberId";
                }
                else
                {
                    para.Add("@CoachId", MemberId, DbType.String, ParameterDirection.Input, size: 50);
                    str += " and CoachId=@CoachId and  StatusId <> 7";
                }
                str += " order by StatusId asc";
                list = con.Query<CoachService>(str, para, commandTimeout: 300, commandType: CommandType.Text).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return list;


        }
    }
}