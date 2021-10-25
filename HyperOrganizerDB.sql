Create database HyperOrganizerDB

Create table Users(
UserID int identity(1,1) not null primary key,
Username Varchar(50) NOT NULL,
Password Varchar(50) NOT NULL
);

Create table Modules(
ID int identity(1,1) not null primary key,
Module_Code varchar(50) not null,
Module_Name varchar(50) not null,
Credits int not null,
Weekly_Hours int not null,
Semester_Weeks int not null,
StartDate dateTime not null,
UserID int,
foreign key (UserID) references Users(UserID)
);

--Have another table for added hours ?
Create table Completed_Hours(
CompHours_ID int identity(1,1) not null primary key,
date dateTime not null,
hours int not null,
ID int,
foreign key (ID) references Modules(ID)
);


Drop database HyperOrganizerDB

SELECT*FROM USERS
SELECT*FROM Modules
SELECT*FROM Completed_Hours
