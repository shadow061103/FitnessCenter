using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;
using FitnessCenterModel;
using System.Data;
using FitnessCenterService.Interface;
using FitnessCenterModel.DTO;

namespace FitnessCenterService.Repository
{
    public class ShareRepository: IShareRepository
    {
        SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["FitnessCenterConnectionString"].ConnectionString);

        /// <summary>
        /// 取台灣地區列表
        /// </summary>
        /// <returns></returns>
        public IList<TaiwanCountry> GetCountryList()
        {
            con.Open();
            var para = new DynamicParameters();
            var list = new List<TaiwanCountry>();
            try
            {

                list = con.Query<TaiwanCountry>(@"select Zipcode,City,Area from TaiwanCountry",
                    para,
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
            return list;
        }
        /// <summary>
        ///取得訓練項目
        /// </summary>
        /// <returns></returns>
        public IList<FitnessTrainingProgram> GetTrainingProgramList()
        {
            con.Open();
            var para = new DynamicParameters();
            var list = new List<FitnessTrainingProgram>();
            try
            {

                list = con.Query<FitnessTrainingProgram>(@"select * from TrainingProgram",
                    para,
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
            return list;
        }
        //課程形式
        public IList<FitnessCourse> GetCourseList()
        {
            con.Open();
            var para = new DynamicParameters();
            var list = new List<FitnessCourse>();
            try
            {

                list = con.Query<FitnessCourse>(@"select * from Course",
                    para,
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
            return list;
        }
    }
}