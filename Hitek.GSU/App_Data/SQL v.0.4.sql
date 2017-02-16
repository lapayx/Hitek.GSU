if columnproperty(object_id('AspNetUsers'),'RegistrationDate','AllowsNull') is NULL
begin
ALTER TABLE AspNetUsers
    ADD  RegistrationDate DATETIME  NULL ;
end;

Go

if (select count(*) from AspNetUsers) = (select count(*) from AspNetUsers where RegistrationDate is null)
begin
	update AspNetUsers set RegistrationDate = SYSDATETIME();
end;

go

ALTER TABLE AspNetUsers ALTER COLUMN RegistrationDate DATETIME NOT NULL;
go;

if columnproperty(object_id('AspNetUsers'),'LastLoginDate','AllowsNull') is NULL
begin
ALTER TABLE AspNetUsers
    ADD  LastLoginDate DATETIME  NULL ;
end;

go;

ALTER TABLE TestAnswers ALTER COLUMN Text nvarchar(500) NOT NULL;
ALTER TABLE WorkTestAnswers ALTER COLUMN Text nvarchar(500) NOT NULL;
go

