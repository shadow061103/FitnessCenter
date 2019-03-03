--�|��
create Table Member (
MemberId Nvarchar(50) not null,
Name Nvarchar(50) not null,
NickhName Nvarchar(50) null,
Gender Int , --0�k1�k
Password Nvarchar(100) not null,
Email Nvarchar(100) not null,
Phone Nvarchar(100) not null,
Birthday Date,
Address Nvarchar(200) not null,
State int --0�@��|�� 1�нm
PRIMARY KEY( MemberId )
);
--�нm
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
--�|���W�Ҧa��
create table MemberArea(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Area Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Member(MemberId)
);
--�нm�W�Ҧa��
create table CoachArea(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Area Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--�нm�V�m����
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

--�V�m�����`��
create table TrainingProgram(
ID Int identity(1,1),
TrainingProgram Nvarchar(50)
PRIMARY KEY( ID )

);



--�нm��ҧΦ�

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


--��ҧΦ�
create table Course(
ID Int identity(1,1),
Course Nvarchar(50)
PRIMARY KEY( ID )
)
select * from Course
--�нm�ҷ�
create table CoachLicense(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
License Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--�нm�g��
create table CoachExperience(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Experience Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);
--���ɸg��
create table CoachCompetiton(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
Competiton Nvarchar(50)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
);

ALTER TABLE Member ADD UNIQUE (Email);

--�|���j�Y��
create table MemberImage(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
ImageData VARBINARY(MAX)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Member(MemberId)
)
alter table CoachImage add ImageName Nvarchar(max)
--alter table CoachImage alter column ImageData VARBINARY(MAX)

--�нm�Ӥ�
create table CoachImage(
ID Int identity(1,1),
MemberId Nvarchar(50) not null,
ImageData VARBINARY(MAX)
PRIMARY KEY( ID )
FOREIGN KEY (MemberId) REFERENCES Coach(MemberId)
)

--�V�m�ؼй���MemberID �˵���
create view ViewTrainingProgram as
select a.MemberId,b.TrainingProgram
from FitnessCenter.dbo.CoachTrainingProgram as a
inner join FitnessCenter.dbo.TrainingProgram as b
on a.TrainingProgramId=b.ID;

--�ҵ{�Φ�����MemberID �˵���
create view ViewCoachCourse as
select a.MemberId,b.Course
from FitnessCenter.dbo.CoachCourse as a
inner join FitnessCenter.dbo.Course as b
on a.CourseId=b.ID;

--�����d�� �����ܥ�
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

--�q�檬�A ���>�нm�T�{>�ǭ��I�ڡB�A�ȧ���
create Table ReserveStatus(
ID Int identity(1,1),
Stauts nvarchar(50)
PRIMARY KEY( ID )
)
drop table ReserveStatus



--�w�����͸�ƪ�
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
Update ReserveService set ServiceDate='2019-06-16 ����:00:00.000' where OrderId=5

--ALTER TABLE ReserveService ALTER COLUMN ServiceDate datetime;

--�w���A�ȸ�ƪ�
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

--�w�������˵���
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



--�w���A���˵���
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