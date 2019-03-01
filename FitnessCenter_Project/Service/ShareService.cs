using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using FitnessCenterModel;
using FitnessCenterModel.DTO;

namespace FitnessCenter_Project.Service
{
    public class ShareService
    {
        //單例 延遲初始化
        private static readonly Lazy<ShareService> LazyInstance = new Lazy<ShareService>(() => new ShareService());
        private ShareService() { }
        public static ShareService Instance { get { return LazyInstance.Value; } }


        public bool CheckFormat(string InputValue, string strRegex, int oLength, ref string Msg)
        {
            if (Encoding.Default.GetBytes(InputValue).Length > oLength)
            {
                Msg = "長度有誤";
                return false;
            }

            Regex pattern = new Regex(strRegex);//RegexOptions.IgnoreCase
            Match match = pattern.Match(InputValue);
            if (match.Success)
            {
                return true;
            }
            else
            {
                Msg = "格式有誤";
                return false;
            }
        }
        public string  SendApi(string url, string postData)
        {
            url = Properties.Settings.Default.Apiurl + url;
            HttpWebRequest WebRequest = (HttpWebRequest)HttpWebRequest.Create(url.Trim());
            
            byte[] parameterString = Encoding.UTF8.GetBytes(postData);
            WebRequest.Method = "POST";
            WebRequest.ContentType = "application/json";
            WebRequest.ContentLength = parameterString.Length;
            //等待要求逾時之前的毫秒數。預設值為 100,000 毫秒 (100 秒)。
            using (Stream stream = WebRequest.GetRequestStream())
            {
                stream.Write(parameterString, 0, parameterString.Length);
                stream.Close();
            }

            string returnString = "";
            using (HttpWebResponse WebResponse = (HttpWebResponse)WebRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(WebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    returnString = sr.ReadToEnd();
                    sr.Close();
                }
                WebResponse.Close();
            }
            return returnString;

        }
        #region LOG
        public  void LogTxt(string Msg)
        {
            if (!string.IsNullOrEmpty(Msg))
                LogTxt(Msg, "log", 180);
        }
        /// <summary> 
        /// 紀錄資料純放文字檔 <para></para>  
        /// 可自訂存放天數的txt檔案 
        /// </summary> 
        /// <param name="Msg">訊息</param> 
        /// <param name="directory">自訂資料夾(選填)</param> 
        /// <param name="retainDay">存放天數(選填)</param>  
        public  void LogTxt(string Msg, string directory = "log", int retainDay = 180, string Name = "Log")
        {
            DeleteFile("*.txt", retainDay, directory);

            if (!string.IsNullOrEmpty(Msg))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory));
                try
                {
                    System.IO.File.AppendAllText(string.Format(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory) + "/" + Name + "-{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")), string.Concat(new object[] { Msg, Environment.NewLine }));
                }
                catch (Exception Ex)
                {
                    System.IO.File.AppendAllText(string.Format(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory) + "/" + Name + "-{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")), string.Concat(new object[] { "=============Error===============" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine + Ex.ToString() + Environment.NewLine + "=============Error===============", Environment.NewLine }));
                }
            }
        }
        private  void DeleteFile(string FilePattern, int retainDay, string directory)
        {
            try
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory));
                String[] FileCollection;
                if (string.IsNullOrEmpty(FilePattern))
                    FileCollection = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory));
                else
                    FileCollection = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/" + directory), FilePattern);

                for (int i = 0; i < FileCollection.Length; i++)
                {
                    FileInfo theFileInfo = new FileInfo(FileCollection[i]);
                    TimeSpan TIS = DateTime.Now.Subtract(theFileInfo.LastWriteTime);
                    if (TIS.TotalDays > retainDay)
                        System.IO.File.Delete(theFileInfo.FullName);
                }
            }
            catch (Exception)
            {
            }

        }
        #endregion
        #region DATA
        /// <summary>
        /// 縣市地區列表
        /// </summary>
        /// <returns></returns>
        public List<TaiwanCountry> GetCountry()
        {
            var result = new List<TaiwanCountry>();
            ShareService service = new ShareService();
            string returnStr = service.SendApi("Data/GetCountryList", "");
            result = JsonConvert.DeserializeObject<List<TaiwanCountry>>(returnStr);
            return result;
        }
        public List<FitnessTrainingProgram> GetTrainingProgram()
        {
            var result = new List<FitnessTrainingProgram>();
            ShareService service = new ShareService();
            string returnStr = service.SendApi("Data/GetTrainingProgramList", "");
            result = JsonConvert.DeserializeObject<List<FitnessTrainingProgram>>(returnStr);
            return result;
        }
        public List<FitnessCourse> GetCourse()
        {
            var result = new List<FitnessCourse>();
            ShareService service = new ShareService();
            string returnStr = service.SendApi("Data/GetCourseList", "");
            result = JsonConvert.DeserializeObject<List<FitnessCourse>>(returnStr);
            return result;
        }
        public List<ReserveStatus> GetReserveStatus()
        {
            var result = new List<ReserveStatus>();
            ShareService service = new ShareService();
            string returnStr = service.SendApi("Reserve/GetOrderStatus", "");
            result = JsonConvert.DeserializeObject<List<ReserveStatus>>(returnStr);
            return result;

        }
        #endregion
    }
}