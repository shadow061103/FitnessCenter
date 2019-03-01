using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Web.Configuration;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using System.Data;
using FitnessCenterService.Interface;

namespace FitnessCenterService.Repository
{
    public class SearchRepository: ISearchRepository
    {
        SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["FitnessCenterConnectionString"].ConnectionString);
        /// <summary>
        /// 首頁查詢
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CoachView> ViewCoachData(CoachSearchView input)
        {
            var list = new List<CoachView>();
            con.Open();
            var para = new DynamicParameters();
            try
            {
                string wherestr = @"select  distinct(MemberId) from ViewCoachData where 1=1";
                string str = @"select * from ViewCoachData where 1=1 ";
                if (input.TrainingProgramId != 0)
                {
                    para.Add("@TrainingProgramId", input.TrainingProgramId, DbType.Int32, ParameterDirection.Input, size: 100);
                    wherestr += " and TrainingProgramId=@TrainingProgramId ";
                }
                if (input.CourseId != 0)
                {
                    para.Add("@CourseId", input.CourseId, DbType.Int32, ParameterDirection.Input, size: 100);
                    wherestr += " and CourseId=@CourseId ";
                }
                if (!string.IsNullOrEmpty(input.Area))
                {
                    para.Add("@Area", input.Area, DbType.String, ParameterDirection.Input, size: 100);
                    wherestr += " and Area=@Area ";
                }
                str += $"and MemberId in({wherestr})";

                list = con.Query<CoachView>(str, para, commandTimeout: 300, commandType: CommandType.Text).ToList();

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