using FitnessCenterModel;
using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterService.Interface
{
    public interface ICoachRepository<T>:IMemberRepository<T>
    {
        Result InsertTrainingProgram(string MemberId, int TrainingProgram);
        Result DeleteTrainingProgram(string MemberId, int TrainingProgram);
        List<CoachTrainingProgram> GetTrainingProgram(string MemberId);
        Result InsertCoachCourse(string MemberId, int Course);
        Result DeleteCoachCourse(string MemberId, int Course);
        List<CoachCourse> GetCoachCourse(string MemberId);
        Result InsertCoachLicense(string MemberId, string License);
        Result DeleteCoachLicense(string MemberId, string License);
        List<string> GetCoachLicense(string MemberId);
        Result InsertCoachExperience(string MemberId, string Experience);
        Result DeleteCoachExperience(string MemberId, string Experience);
        List<string> GetCoachExperience(string MemberId);
        Result InsertCoachCompetiton(string MemberId, string Competiton);
        Result DeleteCoachCompetiton(string MemberId, string Competiton);
        List<string> GetCoachCompetiton(string MemberId);
    }
}