# prjDatabaseContacts
Database Contacts app 


create table tblUsers(
Username varchar(255) not null PRIMARY KEY,
[Password] varchar(255) not null
);

create table tblContacts(
[PersonID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
[FirstName] varchar(255) not null,
[LastName] varchar(255) not null,
PhoneNumber varchar(10) not null,
EmailAddress varchar(255) not null,
Username varchar(255) not null REFERENCES tblUsers(Username)
);
