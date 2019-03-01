using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
   public class UpdateMemberViewModel
    {
        public UpdateMemberViewModel()
        {
            UpdateModel = new UpdateMember();
        }
        public UpdateMember UpdateModel { get; set; }
        public string addressCity { get; set; }

        public string addressArea { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public SelectList addressCityList { get; set; }
        public SelectList addresAreaList { get; set; }
        public MultiSelectList AreaList { get; set; } //jquery multiselect搭配使用 才可以繫結多個值

       
    }
    public class UpdateMember
    {
        public Member User { get; set; }

        public List<string> OriginArea { get; set; }

    }
}
