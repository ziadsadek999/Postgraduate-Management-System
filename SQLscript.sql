create database postGradDb
go
--Apend your ID to the Database name before excuting
Use postGradDb

create table PostGradUser (
id int primary key identity,
email varchar(50) ,
password varchar(50),
);

create table Admin (
	id int,
	primary key (id),
	FOREIGN KEY(id) REFERENCES PostGradUser(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table GucianStudent (
	id int primary key ,
	firstName varchar(50),
	lastName varchar(50),
	type varchar(50)  ,
	faculty varchar(50)  ,
	address varchar (50) ,
	GPA decimal default 0.0,
	undergradID varchar(10) ,
	FOREIGN KEY(id) REFERENCES PostGradUser(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table NonGucianStudent (
	id int primary key ,
	firstName varchar(50)  ,
	lastName varchar(50)  ,
	type varchar(50)  ,
	faculty varchar(50)  ,
	address varchar (50) ,
	GPA decimal default 0.0,
	FOREIGN KEY(id) REFERENCES PostGradUser(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table GUCStudentPhoneNumber (
id int,
phone varchar(50),
primary key (id, phone),
FOREIGN KEY(id) REFERENCES GucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
);


create table NonGUCStudentPhoneNumber (
id int,
phone varchar(50),
primary key (id, phone),
FOREIGN KEY(id) REFERENCES NonGucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table Course (
	id int primary key identity,
	fees decimal  ,
	creditHours int  ,
	code varchar(50)   ,
);

create table Supervisor (
	id int primary key,
	name varchar(50) not  null,
	faculty varchar(50)  ,
	FOREIGN KEY(id) REFERENCES PostGradUser(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

/* all amounts, fundPercentage , and no_Installments would be better to check that cannot be -ve value and by default they are 0 */
create table Payment (
	id int primary key identity,
	amount decimal Default 0,
	no_Installments int Default 0,
	fundPercentage decimal Default 0
);

/* grade , noOfExtension is by default 0*/
create table Thesis (
serialNumber int primary key,
field varchar (50) , 
type varchar (50)  ,
title varchar(50)  ,
startDate datetime   ,
endDate datetime  ,
defenseDate datetime  ,
years as year(endDate) - year(startDate),
grade decimal,
payment_id int,
FOREIGN KEY(payment_id) REFERENCES Payment(id) ON DELETE CASCADE ON UPDATE CASCADE,
noExtension int   default 0
);

create table Publication (
	id int primary key identity,
	title varchar(50)  ,
	date datetime  ,
	place varchar(50)  ,
	accepted bit  ,
	host varchar(50)  
);
 

create table Examiner (
	id int primary key,
	FOREIGN KEY(id) REFERENCES PostGradUser(id) ON DELETE CASCADE ON UPDATE CASCADE,
	name varchar(50)  ,
	fieldOfWork varchar(50)  ,
	isNational bit  
);

create table Defense (
	serialNumber int,
	date datetime,
	location varchar(50),
	grade decimal,
	primary key (serialNumber , date ),
	FOREIGN KEY(serialNumber) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	
);

/* foreign key SUPID cannot on delete cascade */
create table GUCianProgressReport (
	sid int,
	no int identity,
	primary key (sid , no),
	FOREIGN KEY(sid) REFERENCES GucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	date datetime  ,
	eval int,
	state int  ,
	thesisSerialNumber int,
	FOREIGN KEY(thesisSerialNumber) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	supid int,
	FOREIGN KEY(supid) REFERENCES Supervisor(id),
	description varchar(200)
);

/* foreign key SUPID cannot on delete cascade */
create table NonGUCianProgressReport (
	sid int,
	no int identity,
	primary key (sid , no),
	FOREIGN KEY(sid) REFERENCES NonGucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	date datetime  ,
	eval int,
	state int  ,
	thesisSerialNumber int,
	FOREIGN KEY(thesisSerialNumber) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	supid int,
	FOREIGN KEY(supid) REFERENCES Supervisor(id),
	description varchar(200)
);

/* amount is by default 0*/
create table Installment (
	date datetime ,
	paymentID int,
	primary key (date , paymentID),
	FOREIGN KEY(paymentID) REFERENCES Payment(id) ON DELETE CASCADE ON UPDATE CASCADE,
	amount decimal default 0.0,
	done bit default '0'
);

create table NonGucianStudentPayForCourse (
	sid int,
	paymentNo int,
	cid int,
	primary Key (sid , paymentNo , cid),
	FOREIGN KEY(sid) REFERENCES NonGucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(paymentNo) REFERENCES Payment(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(cid) REFERENCES Course(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table NonGucianStudentTakeCourse (
	sid int ,
	cid int,
	grade decimal  default 0,
	primary key (sid , cid),
	FOREIGN KEY(sid) REFERENCES NonGucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(cid) REFERENCES Course(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

/* supervisor id cannot have on delete cascade action or update cascade action */
create table GUCianStudentRegisterThesis (
	sid int,
	supid int,
	serial_no int,
	primary key (sid , supid , serial_no),
	FOREIGN KEY(sid) REFERENCES GucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(supid) REFERENCES Supervisor(id) ,
	FOREIGN KEY(serial_no) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
);

/* supervisor id cannot have on delete cascade action or update cascade action */
create table NonGUCianStudentRegisterThesis (
	sid int,
	supid int,
	serial_no int,
	primary key (sid , supid , serial_no),
	FOREIGN KEY(sid) REFERENCES NonGucianStudent(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(supid) REFERENCES Supervisor(id) ,
	FOREIGN KEY(serial_no) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table ExaminerEvaluateDefense (
	date datetime,
	serialNo int,
	examinerId int ,
	comment varchar (300),
	primary key (date , serialNo , examinerId),
	FOREIGN KEY(serialNo, date) REFERENCES Defense(serialNumber ,date) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(examinerId) REFERENCES Examiner(id) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table ThesisHasPublication (
	serialNo int,
	pubid int,
	primary key(serialNo , pubid),
	FOREIGN KEY(serialNo) REFERENCES Thesis(serialNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(pubid) REFERENCES Publication(id) ON DELETE CASCADE ON UPDATE CASCADE,
);



--User story 1 (a) Register to the website by using my name (First and last name), password, faculty, email, and address.
--for students:
use postGradDb;
go
create proc StudentRegister
@first_name varchar(20), 
@last_name varchar(20), 
@password varchar(20), 
@faculty varchar(20),
@Gucian bit, 
@email varchar(50), 
@address varchar(50)
as
insert into PostGradUser(email, password) values (@email,@password)
declare @foreign_id int;
SELECT @foreign_id=SCOPE_IDENTITY()
if @Gucian = '1'
begin
insert into GucianStudent(id,firstName, lastName, faculty, address)
values(@foreign_id,@first_name,@last_name,@faculty,@address)
end
else
begin
insert into NonGucianStudent(id,firstName, lastName, faculty, address)
values(@foreign_id,@first_name,@last_name,@faculty,@address)
end
--for supervisors
go
create proc SupervisorRegister
@first_name varchar(20), 
@last_name varchar(20), 
@password varchar(20), 
@faculty varchar(20), 
@email varchar(50)
as
insert into PostGradUser(email, password) values (@email,@password)
declare @foreign_id int;
SELECT @foreign_id=SCOPE_IDENTITY()
declare @name varchar(50)
set @name = CONCAT(@first_name,@last_name)
insert into Supervisor(id,name, faculty)
values(@foreign_id,@name,@faculty)

--User story 2 (a) login using my username and password.
go
Create proc userLogin
@id int,
@password varchar(20),
@success bit output,
@type int output
as
begin
if exists(
select ID,password
from PostGradUser
where id=@id and password=@password)
begin
set @success =1
-- check user type 0-->Student,1-->Admin,2-->Supervisor ,3-->Examiner
if exists(select id from GucianStudent where id=@id union select id from
NonGucianStudent where id=@id )
set @type=0
if exists(select id from Admin where id=@id)
set @type=1
if exists(select id from Supervisor where id=@id)
set @type=2
if exists(select id from Examiner where id=@id)
set @type=3
end
else
set @success=0
end


--User story 2 (b) add my mobile number(s).
go
create proc addMobile
@ID int, @mobile_number varchar(20)
as
if exists(select *
from GucianStudent 
where id = @ID)
begin
insert into GUCStudentPhoneNumber(id,phone) values(@ID,@mobile_number)
end
else
begin
if exists(select *
from NonGucianStudent 
where id = @ID)
begin
insert into NonGUCStudentPhoneNumber(id,phone) values(@ID,@mobile_number)
end
else
raiserror('There is no student with this ID',16,1)
end

--User story 3 (a) List all supervisors in the system.
go
create proc AdminListSup
as
select p.*,s.name,s.faculty
from Supervisor s, PostGradUser p
where s.id = p.id

--User story 3 (b) view the profile of any supervisor that contains all his/her information.
go
create proc AdminViewSupervisorProfile
@supId int
as
select p.*,s.name,s.faculty
from Supervisor s,PostGradUser p
where s.id = @supId and p.id = @supId;

--User story 3 (c) List all Theses in the system.
go
create proc AdminViewAllTheses
as
select *
from Thesis


--User story 3 (d) List the number of on going theses.
go
create proc AdminViewOnGoingTheses
@thesesCount int output
as
select @thesesCount = count(*)
from Thesis
where startDate <= current_timestamp and endDate >current_timestamp

--User story 3 (e) List all supervisors’ names currently supervising students, theses title, student name.
go
create proc AdminViewStudentThesisBySupervisor
as
select sup.name, th.title, st.firstName, st.lastName  
from GUCianStudentRegisterThesis pr, Supervisor sup,Thesis th,  GucianStudent st
where sup.id = pr.supid and pr.sid = st.id and pr.serial_no = th.serialNumber and th.startDate <= current_timestamp and th.endDate >current_timestamp
union
select sup2.name, th2.title, st2.firstName, st2.lastName  
from NonGUCianStudentRegisterThesis pr2, Supervisor sup2,Thesis th2, NonGucianStudent st2
where sup2.id = pr2.supid and pr2.sid = st2.id and pr2.serial_no = th2.serialNumber and th2.startDate <= current_timestamp and th2.endDate >current_timestamp

--User story 3 (f) List nonGucians names, course code, and respective grade.
go
create proc AdminListNonGucianCourse
@courseID int
as
select s.firstName,s.lastName,c.code, enroll.grade
from  NonGucianStudent s inner join NonGucianStudentTakeCourse enroll on s.id = enroll.sid
inner join Course c on c.id = enroll.cid
where c.id = @courseID

--User story 3 (g) increase the number of thesis extension by 1.
go
create proc AdminUpdateExtension
@ThesisSerialNo int
as
if exists (select * from Thesis where serialNumber = @ThesisSerialNo)
begin
Declare @current_extensions int
select @current_extensions = noExtension
from Thesis
where serialNumber = @ThesisSerialNo
update Thesis set noExtension = @current_extensions+1 where serialNumber = @ThesisSerialNo
end
else
raiserror('There is no Thesis with this serial number',16,1)

--User story 3 (h) Issue a thesis payment.
go
create proc AdminIssueThesisPayment
@ThesisSerialNo int, @amount decimal, @noOfInstallments int, @fundPercentage decimal,@Success bit output
as
if exists(select * from Thesis where serialNumber = @ThesisSerialNo)
begin
declare @lastID int
insert into Payment(amount, no_Installments, fundPercentage) values (@amount, @noOfInstallments, @fundPercentage)
select @lastID = @@IDENTITY
update Thesis set payment_id = @lastID where serialNumber = @ThesisSerialNo
set @Success = '1'
end
else
begin
set @Success = '0'
end

--User story 3 (I) view the profile of any student that contains all his/her information.
go 
create proc AdminViewStudentProfile
@sid int
as
if exists (select * from GucianStudent where id = @sid)
begin
(select p.email,p.password, s.*
from PostGradUser p, GucianStudent s
where p.id = @sid and s.id = @sid)
end
else
begin
(select p2.email,p2.password, s2.*
from PostGradUser p2, NonGucianStudent s2
where p2.id = @sid and s2.id = @sid)
end


--User story 3 (J) Issue installments as per the number of installments for a certain payment every six months starting from the entered date.
go
create proc AdminIssueInstallPayment
@paymentID int, @InstallStartDate date
as
if exists (select * from Payment
where id = @paymentID)
begin
declare @n_inst int
select @n_inst = no_Installments
from Payment
where id = @paymentID

declare @amount decimal
select @amount = amount
from Payment
where id = @paymentID
set @amount = @amount/(@n_inst*1.0)
WHILE @n_inst >0
BEGIN
   insert into Installment(date, paymentId,amount)
   values (@InstallStartDate,@paymentID,@amount)
   set @InstallStartDate = (SELECT DATEADD(month, 6, @InstallStartDate) AS DateAdd)
   SET @n_inst = @n_inst - 1;
END;
end
else
raiserror ('There is no payment with this ID',16,1);

--User story 3 (K) List the title(s) of accepted publication(s) per thesis.
go
create proc AdminListAcceptPublication
as
select  th.title as 'Thesis title',th.serialNumber as 'Thesis serial number',p.title as 'Publication title'
from ThesisHasPublication has,Publication p,Thesis th
where has.serialNo =th.serialNumber and has.pubid = p.id and p.accepted = '1'

go

--3 l)  Add courses and link courses to students.

--  Add the Course to the stored courses
create proc AddCourse
@courseCode varchar(10),
@creditHrs int,
@fees decimal
AS
insert into Course values(@fees, @creditHrs, @courseCode)

go
--   Link a course with student (enroll)
create proc linkCourseStudent
@courseID int,
@studentID int
As
insert into NonGucianStudentTakeCourse values (@studentID, @courseID, null)

go
-- Add grade to some student in some course
create proc addStudentCourseGrade
@courseID int,
@studentID int,
@grade decimal
As
if exists(select * from NonGucianStudentTakeCourse where sid = @studentID and cid = @courseID)
begin
update NonGucianStudentTakeCourse 
set grade = @grade
where sid = @studentID and cid = @courseID
end
else
raiserror ('This student is not enrolled in this course',16,1)

go
-- 3 m) View examiners and supervisor(s) names attending a thesis defense taking place on a certain date.
create proc ViewExamSupDefense
@defenseDate datetime
As
(select E.name as 'Examiner name', Sup.name as 'Supervisor name'
from Defense D inner join GUCianStudentRegisterThesis GT on GT.serial_no = D.serialNumber
inner join Supervisor Sup on GT.supid = Sup.id
inner join ExaminerEvaluateDefense Ex on Ex.serialNo = D.serialNumber
inner join Examiner E on E.id = Ex.examinerId
where D.date = @defenseDate)
union
(select E2.name as 'Examiner name', Sup2.name as 'Supervisor name'
from Defense D2 inner join NonGUCianStudentRegisterThesis GT2 on GT2.serial_no = D2.serialNumber
inner join Supervisor Sup2 on GT2.supid = Sup2.id
inner join ExaminerEvaluateDefense Ex2 on Ex2.serialNo = D2.serialNumber
inner join Examiner E2 on E2.id = Ex2.examinerId
where D2.date = @defenseDate)


go



--4  a)Evaluate a student’s progress report, and give evaluation value 0 to 3
create proc EvaluateProgressReport
@supervisorID int,
@thesisSerialNo int,
@progressReportNo int,
@evaluation int
AS
if exists(select * from GUCianProgressReport where no = @progressReportNo and thesisSerialNumber = @thesisSerialNo and supid = @supervisorID union select * from NonGUCianProgressReport where no = @progressReportNo and thesisSerialNumber = @thesisSerialNo and supid = @supervisorID )
begin
if (@evaluation >=0 and @evaluation<=3)
begin
update GUCianProgressReport
set eval = @evaluation
where no = @progressReportNo and thesisSerialNumber = @thesisSerialNo and supid = @supervisorID
update NonGUCianProgressReport
set eval = @evaluation
where no = @progressReportNo and thesisSerialNumber = @thesisSerialNo and supid = @supervisorID
end
else
begin
raiserror('eror not between 0 and 3',16,1)
end
end
else
raiserror('There is no supervisor with this id',16,1)

go
-- 4 b) View all my students’s names and years spent in the thesis.
create proc ViewSupStudentsYears
@supervisorID int
As 
if exists(select * from Supervisor where id = @supervisorID)
begin
(select S.firstName As 'first name',S.lastName as 'last name', T.years AS 'Years'
from GUCianStudentRegisterThesis R inner join Thesis T on T.serialNumber = R.serial_no
inner join GucianStudent S on S.id = R.sid
where R.supid = @supervisorID)
union 
(select S2.firstName As 'first name',S2.lastName as 'last name', T2.years AS 'Years'
from NonGUCianStudentRegisterThesis R2 inner join Thesis T2 on T2.serialNumber = R2.serial_no
inner join NonGucianStudent S2 on S2.id = R2.sid
where R2.supid = @supervisorID)
end
else
raiserror('There is no supervisor with this id',16,1)

go
-- 4 c) View my profile and update my personal information.
create proc SupViewProfile
@supervisorID int
As
if exists(select * from Supervisor where id = @supervisorID)
begin
select p.*,s.name,s.faculty
from Supervisor s,PostGradUser p
where s.id = p.id and s.id = @supervisorID
end
else
raiserror('There is no supervisor with this id',16,1)
go
-- update Supervisor Profile
create proc UpdateSupProfile
@supervisorID int,
@name varchar(20),
@faculty varchar(20)
As
if exists(select * from Supervisor where id = @supervisorID)
begin
update Supervisor 
set name = @name, faculty = @faculty
where id = @supervisorID
end
else
raiserror('There is no supervisor with this id',16,1)
go
--4 d) View all publications of a student.
create proc ViewAStudentPublications
@StudentID int
AS
(select P.*
from GUCianStudentRegisterThesis R inner join ThesisHasPublication TP on R.serial_no = TP.serialNo
inner join Publication P on P.id = TP.pubid
where R.sid = @StudentID)
union
(select P2.*
from NonGUCianStudentRegisterThesis R2 inner join ThesisHasPublication TP2 on R2.serial_no = TP2.serialNo
inner join Publication P2 on P2.id = TP2.pubid
where R2.sid = @StudentID)

go
-- 4 e) Add defense for a thesis, for nonGucian students all courses’ grades should be greater than 50
create proc AddDefenseGucian
@ThesisSerialNo int,
@DefenseDate Datetime,
@DefenseLocation varchar(15)
As
insert into Defense values (@ThesisSerialNo, @DefenseDate,@DefenseLocation,null)


go
--  Add defense for non GUCIAN student but check constraint first
create proc AddDefenseNonGucian
@ThesisSerialNo int,
@DefenseDate Datetime,
@DefenseLocation varchar(15)
AS
if 50 < all(select C.grade from NonGucianStudentTakeCourse C inner join NonGUCianStudentRegisterThesis GT on GT.sid = C.sid where GT.serial_no = @ThesisSerialNo)
insert into Defense values (@ThesisSerialNo, @DefenseDate,@DefenseLocation,null)
else
raiserror( 'Student Did not Pass all his/her Courses',16,1)
go
-- 4 f) Add examiner(s) for a defense.
create proc AddExaminer
@ThesisSerialNo int,
@DefenseDate Datetime,
@ExaminerName  varchar(20),
@National bit,
@fieldOfWork varchar(20)
As
if exists (select * from Defense where serialNumber=@ThesisSerialNo and @DefenseDate = date)
begin
declare @ExaminerID int
insert into PostGradUser(email, password) values (null,null)
select @ExaminerID = @@IDENTITY
insert into Examiner(id,name, fieldOfWork, isNational) values(@ExaminerID,@ExaminerName,@fieldOfWork,@National)
insert into ExaminerEvaluateDefense values(@DefenseDate, @ThesisSerialNo, @ExaminerID, null)
end
else
raiserror('There is not a defense with these data',16,1)

go

-- 4 g) Cancel a Thesis if the evaluation of the last progress report is zero.
create proc CancelThesis
@ThesisSerialNo int,
@suc bit output
AS
if not exists(select * from Thesis where serialNumber = @ThesisSerialNo)
begin
raiserror('There is no thesis with this serial number',16,1)
end
else
begin
declare @Lasteval as int
if exists(select * from GUCianStudentRegisterThesis where  serial_no = @ThesisSerialNo)
begin
	select @Lasteval = eval
	from GUCianProgressReport a
	where a.thesisSerialNumber = @ThesisSerialNo and a.date >= all(select b.date from GUCianProgressReport b where b.thesisSerialNumber = @ThesisSerialNo)
if(@lasteval = 0)
	begin
	delete from Thesis where serialNumber = @ThesisSerialNo
	set @suc = 1
	end
end
else
begin
	select @Lasteval = eval
	from NonGUCianProgressReport c
	where c.thesisSerialNumber = @ThesisSerialNo and c.date >= all(select d.date from NonGUCianProgressReport d where d.thesisSerialNumber = @ThesisSerialNo)
if(@lasteval = 0)
		begin
		delete from Thesis where serialNumber = @ThesisSerialNo
		set @suc = 1
		end
		else
		begin
		set @suc = 0
		end
end
end
go

-- 4 h) Add a grade for a thesis.
create proc AddGrade
@ThesisSerialNo int
As
if exists (select * from Thesis where serialNumber = @ThesisSerialNo)
begin

declare @grade decimal
if exists(select grade
from Defense
where serialNumber = @ThesisSerialNo )
begin
select @grade = grade
from Defense
where serialNumber = @ThesisSerialNo 

update Thesis
set grade = @grade
where serialNumber = @ThesisSerialNo
end
else
begin
raiserror('There is no defense linked with this thesis',16,1)
end
end
else
begin
raiserror('There is no a thesis with this serial number',16,1)
end



go
/*User story 5 (a)  Add grade for a defense*/
create procedure   AddDefenseGrade  @ThesisSerialNo int , @DefenseDate Datetime , @grade decimal
as
if exists(select * from Defense where serialNumber= @ThesisSerialNo and date= @DefenseDate)
update Defense set grade= @grade where serialNumber= @ThesisSerialNo and date= @DefenseDate;
else
raiserror('Invalid input',16,1)  
go

 /*User story 5 (b) Add comments for a defense*/
create procedure  AddCommentsGrade @ThesisSerialNo int , @DefenseDate Datetime , @comments varchar(300)
as
if exists(select * from ExaminerEvaluateDefense where date=@DefenseDate and serialNo=@ThesisSerialNo)
update ExaminerEvaluateDefense set comment= @comments where date=@DefenseDate and serialNo=@ThesisSerialNo;
else
raiserror('Invalid input',16,1)
go

/*User story 6 (a) View my profile that contains all my information*/
create procedure  viewMyProfile @studentId int
as
if exists(select id from GucianStudent where id = @studentId )
	begin
	select u.email,u.password,gs.*,p.phone from PostGradUser u
	inner join GucianStudent gs on gs.id=u.id
	left outer join GUCStudentPhoneNumber p on p.id=u.id
	where u.id = @studentId
	end
else
	begin
	if exists(select id from nonGucianStudent where id = @studentId )
		begin
		select u2.email,u2.password,ns.*,p2.phone from PostGradUser u2
		inner join nonGucianStudent ns on ns.id=u2.id
		left outer join nonGUCStudentPhoneNumber p2 on p2.id=u2.id
		where u2.id = @studentId
	end
	else
		begin
		raiserror('There is not any student with this id',16,1);
		end
end
go



/*User story 6 (b) Edit my profile (change any of my personal information)*/
create procedure  editMyProfile @studentID int, @firstName varchar(50), @lastName varchar(50), @password varchar(50), @email varchar(50), @address varchar(50), @type varchar(50)
as
if not exists((select id from GucianStudent where id = @studentID) union (select id from NonGucianStudent where id = @studentID))
begin
raiserror('There is not any student with this id',16,1);
end
else
begin
UPDATE PostGradUser set email=@email , password=@password where id=@studentID;
UPDATE GucianStudent set firstName=@firstName, lastName=@lastName ,  address=@address , type=@type where id=@studentID;
UPDATE NonGucianStudent set firstName=@firstName, lastName=@lastName ,  address=@address , type=@type  where id=@studentID;
end
go


/*User story 6 (c) As a Gucian graduate, add my undergarduate ID*/
create procedure  addUndergradID @studentID int, @undergradID varchar(10)
as
if not exists(select id from GucianStudent where id = @studentID)
begin
raiserror('There is not any gucian student with this id',16,1);
end
else
begin
UPDATE GucianStudent set undergradID=@undergradID where id=@studentID
end
go



/*User story 6 (d) As a nonGucian student, view my courses’ grades*/
create procedure   ViewCoursesGrades  @studentID int
as
if not exists(select id from NonGucianStudent where id = @studentID)
begin
raiserror('There is not any non gucian student with this id',16,1);
end
else
begin
select  c.code , c.id , s.grade from NonGucianStudentTakeCourse s
inner join Course c on c.id= s.cid 
where s.sid=@studentID;
end
go




/*User story 6 (e) View all my payments and installments*/
create procedure   ViewCoursePaymentsInstall  @studentID int
as
if not exists(select id from GucianStudent where id = @studentID union select id from NonGucianStudent where id = @studentID)
begin
raiserror('There is not any student with this id',16,1);
end
else
begin
select P.id, P.amount, P.no_Installments, P.fundPercentage, I.date, I.amount, I.done from NonGucianStudentPayForCourse S 
inner join  Payment P on S.paymentNo= P.id 
inner join Installment I on P.id=I.paymentId
where @studentID= S.sid
end
go


create procedure   ViewThesisPaymentsInstall  @studentID int
as
if not exists(select id from GucianStudent where id = @studentID union select id from NonGucianStudent where id = @studentID)
begin
raiserror('There is not any student with this id',16,1);
end
else
begin
(select P.id, P.amount, P.no_Installments, P.fundPercentage, I.date, I.amount, I.done from GUCianStudentRegisterThesis S
inner join Thesis T on T.serialNumber= S.serial_no
inner join  Payment P on T.payment_id= P.id 
inner join Installment I on P.id=I.paymentId
where @studentID= S.sid)
union 
(select P2.id, P2.amount, P2.no_Installments, P2.fundPercentage, I2.date, I2.amount, I2.done from NonGUCianStudentRegisterThesis S2
inner join Thesis T2 on T2.serialNumber= S2.serial_no
inner join  Payment P2 on T2.payment_id= P2.id 
inner join Installment I2 on P2.id=I2.paymentId
where @studentID= S2.sid)
end
go


create procedure   ViewUpcomingInstallments  @studentID int
as
if not exists(select id from GucianStudent where id = @studentID union select id from NonGucianStudent where id = @studentID)
begin
raiserror('There is not any student with this id',16,1);
end
else
begin
((select I.* from GUCianStudentRegisterThesis S
inner join Thesis T on T.serialNumber= S.serial_no
inner join  Payment P on T.payment_id= P.id 
inner join Installment I on P.id=I.paymentId
where @studentID= S.sid and I.date>current_timestamp)
union 
(select I2.* from NonGUCianStudentRegisterThesis S2
inner join Thesis T2 on T2.serialNumber= S2.serial_no
inner join  Payment P2 on T2.payment_id= P2.id 
inner join Installment I2 on P2.id=I2.paymentId
where @studentID= S2.sid and I2.date>current_timestamp))
union 
(select I3.* from NonGucianStudentPayForCourse S3 
inner join  Payment P3 on S3.paymentNo= P3.id 
inner join Installment I3 on P3.id=I3.paymentId
where @studentID= S3.sid and I3.date>current_timestamp)
end
go


create procedure ViewMissedInstallments  @studentID int
as
if not exists(select id from GucianStudent where id = @studentID union select id from NonGucianStudent where id = @studentID)
begin
raiserror('There is not any student with this id',16,1);
end
else
begin
((select I.* from GUCianStudentRegisterThesis S
inner join Thesis T on T.serialNumber= S.serial_no
inner join  Payment P on T.payment_id= P.id 
inner join Installment I on P.id=I.paymentId
where @studentID= S.sid and I.date<current_timestamp and I.done='0')
union 
(select I2.* from NonGUCianStudentRegisterThesis S2
inner join Thesis T2 on T2.serialNumber= S2.serial_no
inner join  Payment P2 on T2.payment_id= P2.id 
inner join Installment I2 on P2.id=I2.paymentId
where @studentID= S2.sid and I2.date<current_timestamp and I2.done='0'))
union 
(select I3.* from NonGucianStudentPayForCourse S3
inner join  Payment P3 on S3.paymentNo= P3.id 
inner join Installment I3 on P3.id=I3.paymentId
where @studentID= S3.sid and I3.date<current_timestamp and I3.done='0')
end
go

/*User story 6 (f)  Add and fill my progress report(s)*/
create procedure   AddProgressReport  @thesisSerialNo int, @progressReportDate date
as

if exists(select * from GUCianStudentRegisterThesis where serial_no = @thesisSerialNo)
begin
declare @stID int
declare @sup int
select @stID = sid, @sup = supid
from GUCianStudentRegisterThesis where serial_no = @thesisSerialNo


insert into GUCianProgressReport (supid,sid,date, thesisSerialNumber)  values (@sup,@stID, @progressReportDate , @thesisSerialNo)
end
else
begin
if exists(select * from nonGUCianStudentRegisterThesis where serial_no = @thesisSerialNo)
begin
declare @stID2 int
declare @sup2 int
select @stID2 = sid, @sup2 = supid
from NonGUCianStudentRegisterThesis where serial_no = @thesisSerialNo
insert into NonGUCianProgressReport  (supid,sid,date, thesisSerialNumber)  values (@sup2,@stID2, @progressReportDate , @thesisSerialNo)
end
else
begin
raiserror('There is not any student with this id',16,1);
end

end

go

create procedure  FillProgressReport @thesisSerialNo int, @progressReportNo int, @state int, @description varchar(200)
as
if exists (select * from GUCianProgressReport where @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo)
begin
UPDATE GUCianProgressReport set  state=@state , description=@description  where @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo
end
else
begin
if exists (select * from NonGUCianProgressReport where @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo)
begin
UPDATE NonGUCianProgressReport  set state=@state , description=@description where  @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo

end
else
begin
raiserror('There is not any progress report with this progress report number and thesis serial number',16,1);
end
end
go




/*User story 6 (g) View my progress report(s) evaluations*/
create procedure  ViewEvalProgressReport  @thesisSerialNo int, @progressReportNo int
as
if exists (select * from GUCianProgressReport where @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo)
begin
(select eval from GUCianProgressReport GP  where GP.no=@progressReportNo AND GP.thesisSerialNumber= @thesisSerialNo )
end
else
begin
if exists (select * from NonGUCianProgressReport where @thesisSerialNo=thesisSerialNumber and  no= @progressReportNo)
begin
(select eval from NonGUCianProgressReport GP2  where GP2.no=@progressReportNo AND GP2.thesisSerialNumber= @thesisSerialNo )
end
else
begin
raiserror('There is not any progress report with this progress report number and thesis serial number',16,1);
end
end
go



/*User story 6 (h) Add publication*/
create procedure   addPublication  @title varchar(50), @pubDate datetime, @host varchar(50), @place varchar(50), @accepted bit
as
insert into Publication (title, date, place, accepted, host) values ( @title , @pubDate , @place , @accepted ,@host )
go


go
/*User story 6 (i) Link publication to my thesis*/
create procedure  linkPubThesis @PubID int, @thesisSerialNo int
as
if exists(select * from Publication where id = @PubID) and exists(select * from Thesis where serialNumber = @thesisSerialNo)
insert into ThesisHasPublication values (@thesisSerialNo,@PubID)
else
raiserror('There is not a publication with this ID or there is not a thesis with this serial number',16,1);
go
------------
go 
create proc studentViewAllThesis
@id int
as
select t.*
from Thesis t,GUCianStudentRegisterThesis s
where t.serialNumber=s.serial_no and s.sid = @id
union
select t2.*
from Thesis t2,NonGUCianStudentRegisterThesis s2
where t2.serialNumber=s2.serial_no and s2.sid = @id
go
create proc isAdmin
@id int,
@isAdmin bit output
as
if exists(select * from Admin where id=@id)
set @isAdmin = 1
else
set @isAdmin = 0

go


create proc updateExamName
@ExamId int,
@newName varchar(50)
As
update Examiner
set name = @newName
where id = @ExamId
go


create proc updateExamFieldOfWork
@ExamId int,
@newField varchar(50)
As
update Examiner
set fieldOfWork = @newField
where id = @ExamId
go

create proc ViewExamSupStu
@ExamID int
AS
(select T.title AS 'Title' , G.firstName+' '+G.lastName As 'Student_Name', Sup.name as 'Supervisor_Name'
from ExaminerEvaluateDefense EED 
inner join GUCianStudentRegisterThesis GT on EED.serialNo = GT.serial_no
inner join Thesis T on GT.serial_no = T.serialNumber 
inner join GucianStudent G on GT.sid = G.id
inner join Supervisor Sup on Sup.id = GT.supid
where EED.examinerId = @ExamID)
union
(select T1.title AS 'Title' , G1.firstName+' '+G1.lastName As 'Student_Name', Sup1.name as 'Supervisor_Name'
from ExaminerEvaluateDefense EED1 
inner join NonGUCianStudentRegisterThesis GT1 on EED1.serialNo = GT1.serial_no
inner join Thesis T1 on GT1.serial_no = T1.serialNumber 
inner join NonGucianStudent G1 on GT1.sid = G1.id
inner join Supervisor Sup1 on Sup1.id = GT1.supid
where EED1.examinerId = @ExamID)

go 
create proc isExaminer
@id int,
@isEx bit output
as
if exists(select * from Examiner where id=@id)
set @isEx = 1
else
set @isEx= 0

go
create proc searchThesis
@keyWord varchar(30)
as
select *
from Thesis
where title like '%'+@keyWord+'%'

go
create proc checkProgressReportIsMine
@id int,
@serialNo int,
@res bit output
as
if (exists(select * from GUCianStudentRegisterThesis where sid=@id and serial_no=@serialNo)or exists(select * from NonGUCianStudentRegisterThesis where sid=@id and serial_no=@serialNo))
set @res = '1'
else
set @res = '0'
go
create proc alreadyLinked
@thNo int,
@pubId int,
@res bit output
as
if(exists(select * from ThesisHasPublication where serialNo=@thNo and pubid=@pubId))
set @res = '1'
else
set @res = '0'

go
create proc checkThesisDate
@serialNo int,
@date datetime,
@res bit output
as
if(not exists(select * from Thesis where serialNumber = @serialNo))
set @res = '1'
else
begin
declare @start datetime
declare @end datetime
select @start  = startDate , @end = endDate
from Thesis
where serialNumber=@serialNo

if(@date>=@start and @date< @end)
set @res = '1'
else
set @res = '0'
end

go
create proc checkThesisExam
@ThesisSerialNo int,
@ExamID int,
@out bit output
as
if(Exists(select * from ExaminerEvaluateDefense where examinerId = @ExamID and  serialNo = @ThesisSerialNo))
set @out ='1'
else
set @out = '0'

go
create proc checkThesisSup
@ThesisSerialNo int,
@SupID int,
@out bit output
as
if(Exists(select * from GUCianStudentRegisterThesis where supid = @SupID and  serial_no = @ThesisSerialNo) or Exists(select * from NonGUCianStudentRegisterThesis where supid =@SupID and serial_no =@ThesisSerialNo))
set @out ='1'
else
set @out = '0'

go
create proc examinerRegister
@name varchar(50),
@email varchar(50),
@password varchar(50),
@field varchar(50),
@isN bit
as
insert into PostGradUser values(@email,@password)
declare @id int
select @id = @@IDENTITY
insert into Examiner values(@id,@name,@field,@isN)

go
create proc checkOnGoing
@serial int
as
if (not exists(select * from Thesis where serialNumber = @serial))
begin
raiserror('You are not registered to a thesis with this serial number',16,1)
end
else
begin
declare @start datetime
declare @end datetime
select @start = startDate , @end = endDate 
from Thesis
where serialNumber = @serial
if(not(@start<= CURRENT_TIMESTAMP and @end>CURRENT_TIMESTAMP))
raiserror('This thesis is not an on going thesis',16,1);
end
