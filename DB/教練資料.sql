
select * from Coach


 
select * from CoachArea where MemberId='17bc479ef762413fac09584de2a1c65e'


select * from ViewTrainingProgram where MemberId='17bc479ef762413fac09584de2a1c65e'

select * from ViewCoachCourse where MemberId='17bc479ef762413fac09584de2a1c65e'

select * from CoachLicense  where MemberId='17bc479ef762413fac09584de2a1c65e'
select * from CoachExperience where MemberId='17bc479ef762413fac09584de2a1c65e'
select * from CoachCompetiton where MemberId='17bc479ef762413fac09584de2a1c65e'
select * from CoachImage

delete CoachImage where ID=8
--­º­¶¸ê®Æ
select * from ViewCoachData


select * from Member 

select * from MemberArea where MemberId='9bf78f22044a4412b653ddb15b3242ee'


select * from MemberImage




select * from ViewCoachData
where 1=1 and MemberId in(
select distinct(MemberId) from ViewCoachData where 1=1 and TrainingProgramId=2
)


select * from TrainingProgram
select * from Course


select GETDATE() as [Today],DATEDIFF(YY,'1993-04-23',GETDATE()) as [Age]


select * from ViewInterView

select * from ViewServie¡@



select * from ReserveStatus


