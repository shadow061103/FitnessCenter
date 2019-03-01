using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.DTO
{
   public class Member //一般會員(學員)
    {
        public string MemberId { get; set; }


        public string Name { get; set; }
        public string NickName { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Birthday { get; set; }

        public string Address { get; set; }
        public string Image { get; set; } //圖片檔名
        public List<string> Area { get; set; } //上課地區
        
        public Status State { get; set; }

    }
    public enum Status
    {
        Student=0,Coach=1
    }
    public class Coach:Member //教練會員
    {
        public Coach()
        {
            TrainingProgram = new List<string>();
            Course = new List<string>();
            TrainingProgramId = new List<int>();
            CourseId = new List<int>();
            Experience = new List<string>();
            License = new List<string>();
            Competiton = new List<string>();
        }

        public string Intoduction { get; set; } //自介
        public List<int> TrainingProgramId{get;set;} //訓練項目V

        public List<int> CourseId { get; set; } //選課形式 V

        public List<string> TrainingProgram { get; set; }
        public List<string> Course { get; set; }

        public List<string> License { get; set; } //證照

        public List<string> Experience { get; set; } //經歷

        public List<string> Competiton { get; set; }//比賽經驗

        public string LineID { get; set; }

        public string FaceBook { get; set; }

        public string Instagram { get; set; }
        
        


    }
}
