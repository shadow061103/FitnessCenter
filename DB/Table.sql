--會員
create Table Member (
MemberId Nvarchar(50) not null,
Name Nvarchar(50) not null,
NickhName Nvarchar(50) null,
Gender Int , --0男1女
Password Nvarchar(100) not null,
Email Nvarchar(100) not null,
Phone Nvarchar(100) not null,
Birthday Date,
Address Nvarchar(200) not null,
State int --0一般會員 1教練
PRIMARY KEY( MemberId )
);
--教練
create Table Coach (
MemberId Nvarchar(50) not null,
Name Nvarchar(50) not null,
NickhName Nvarchar(50) null,
Gender Int ,
Password Nvarchar(100) not null,
Email Nvarchar(100) not null,
Phone Nvarchar(100) not null,
Birthday Date,
Address Nvarchar(200) not null,
State int,
Intoduction Nvarchar(600),
LineID Nvarchar(50),
FaceBook Nvarchar(200),
Instagram Nvarchar(200)
PRIMARY KEY( MemberId )
);
--會員上課地區
create table MemberArea(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Area Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Member(MemberId)
);
--教練上課地區
create table CoachArea(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Area Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--教練訓練項目
create table CoachTrainingProgram(
MemberId Nvarchar(50) not null,
TrainingProgramId Int,
CONSTRAINT PK_CoachTrainingProgram PRIMARY KEY
 (
 MemberId,
 TrainingProgramId
 ),
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId),
FOREIGN KEY (TrainingProgramId) REFERENCES TrainingProgram(ID)
);

--訓練項目總攬
create table TrainingProgram(
ID Int identity(1,1),
TrainingProgram Nvarchar(50)
PRIMARY KEY( ID )

);



--教練選課形式

create table CoachCourse(

MemberId Nvarchar(50) not null,
CourseId  Int not null,
CONSTRAINT PK_CoachCourse PRIMARY KEY
 (
 MemberId,
 CourseId
 ),
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId),
FOREIGN KEY (CourseId) REFERENCES Course(ID)
);


--選課形式
create table Course(
ID Int identity(1,1),
Course Nvarchar(50)
PRIMARY KEY( ID )
)
select * from Course
--教練證照
create table CoachLicense(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
License Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--教練經歷
create table CoachExperience(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Experience Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--比賽經驗
create table CoachCompetiton(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Competiton Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);

ALTER TABLE Member ADD UNIQUE (Email);

--會員大頭照
create table MemberImage(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
ImageData VARBINARY(MAX)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Member(MemberId)
)
alter table CoachImage add ImageName Nvarchar(max)
--alter table CoachImage alter column ImageData VARBINARY(MAX)

--教練照片
create table CoachImage(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
ImageData VARBINARY(MAX)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
)

--訓練目標對應MemberID 檢視表
create view ViewTrainingProgram as
select a.MemberId,b.TrainingProgram
from FitnessCenter.dbo.CoachTrainingProgram as a
inner join FitnessCenter.dbo.TrainingProgram as b
on a.TrainingProgramId=b.ID;

--課程形式對應MemberID 檢視表
create view ViewCoachCourse as
select a.MemberId,b.Course
from FitnessCenter.dbo.CoachCourse as a
inner join FitnessCenter.dbo.Course as b
on a.CourseId=b.ID;

--首頁查詢 單純顯示用
create view ViewCoachData as
select a.MemberId,e.ImageName,a.Name,a.NickName,a.Intoduction,a.Email,b.TrainingProgramId,b.TrainingProgram,c.CourseId,c.Course,d.Area
from FitnessCenter.dbo.Coach a
left join FitnessCenter.dbo.ViewTrainingProgram b
on a.MemberId=b.MemberId 
left join FitnessCenter.dbo.ViewCoachCourse c
on a.MemberId=c.MemberId
left join FitnessCenter.dbo.CoachArea as d
on a.MemberId=d.MemberId
left join FitnessCenter.dbo.CoachImage e
on a.MemberId = e.MemberId

--訂單狀態 填單>教練確認>學員付款、服務完成
create Table ReserveStatus(
ID Int identity(1,1),
Stauts nvarchar(50)
PRIMARY KEY( ID )
)
drop table ReserveStatus



--預約面談資料表
create table ReserveInterView(
OrderId Int identity(1,1),
CoachId Nvarchar(50),
MemberId Nvarchar(50),
StatusId Int,
ReserveDate datetime,
Location Nvarchar(200),
Message Nvarchar(600)
PRIMARY KEY(OrderId)
FOREIGN KEY (MemberId) REFERENCES Member(MemberId),
FOREIGN KEY (CoachId) REFERENCES Coach(MemberId),
FOREIGN KEY (StatusId) REFERENCES ReserveStatus(ID)
)

select * from ReserveInterView
delete ReserveInterView where OrderId=7

select * from ReserveService
Update ReserveService set ServiceDate='2019-06-16 １６:00:00.000' where OrderId=5

--ALTER TABLE ReserveService ALTER COLUMN ServiceDate datetime;

--預約服務資料表
create table ReserveService(
OrderId Int identity(1,1),
CoachId Nvarchar(50),
MemberId Nvarchar(50),
StatusId Int,
ServiceDate datetime,
Location Nvarchar(200),
TrainingProgramId Int,
CourseId Int,
Charge Int

PRIMARY KEY(OrderId)
FOREIGN KEY (MemberId) REFERENCES Member(MemberId),
FOREIGN KEY (CoachId) REFERENCES Coach(MemberId),
FOREIGN KEY (StatusId) REFERENCES ReserveStatus(ID),
FOREIGN KEY (TrainingProgramId) REFERENCES TrainingProgram(ID),
FOREIGN KEY (CourseId) REFERENCES Course(ID)

)
update ReserveService set Charge=1200

--預約面談檢視表
alter view ViewInterView as
select 
A.OrderId,
C.MemberId [CoachId],
B.MemberId [MemberId],
B.Name [MemberName],
B.Gender [MemberGenger],
B.Email [MemberEmail],
B.Phone [MemberPhone],
DATEDIFF(YY,B.Birthday,GETDATE()) as [MemberAge],
H.ImageName [MemberImage],
C.Name [CoachName],
C.Email [CoachEmail],
C.Phone [CoachPhone],
G.ImageName [CoachImage],
C.LineID,
D.ID [StatusId],
D.Status,
A.ReserveDate,
A.Message,
A.Location
from ReserveInterView as A
join Member as B
on A.MemberId=B.MemberId
join Coach as C
on A.CoachId = C.MemberId
join CoachImage as G
on A.CoachId=G.MemberId
join MemberImage as H
on A.MemberId=H.MemberId
join ReserveStatus as D
on A.StatusId=D.ID



--預約服務檢視表
alter view ViewServie as
select 
A.OrderId,
C.MemberId [CoachId],
B.MemberId [MemberId],
B.Name [MemberName],
B.Gender [MemberGenger],
B.Email [MemberEmail],
B.Phone [MemberPhone],
DATEDIFF(YY,B.Birthday,GETDATE()) as [MemberAge],
H.ImageName [MemberImage],
C.Name [CoachName],
C.Email [CoachEmail],
C.Phone [CoachPhone],
G.ImageName [CoachImage],
C.LineID,
D.ID [StatusId],
D.Status,
E.TrainingProgram,
F.Course,
A.ServiceDate,
A.Location,
A.Charge
from ReserveService as A
join Member as B
on A.MemberId=B.MemberId
join Coach as C
on A.CoachId = C.MemberId
join CoachImage as G
on A.CoachId=G.MemberId
join MemberImage as H
on A.MemberId=H.MemberId
join ReserveStatus as D
on A.StatusId=D.ID
join TrainingProgram as E
on A.TrainingProgramId=E.ID
join Course as F
on A.CourseId=F.ID