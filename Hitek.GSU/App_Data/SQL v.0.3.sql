if columnproperty(object_id('Tests'),'IsCanShowResultAnswer','AllowsNull') is NULL
begin
ALTER TABLE [Tests]
    ADD  [IsCanShowResultAnswer] BIT NOT NULL default 0;
end;

Go

if columnproperty(object_id('TestAnswers'),'AccountId','AllowsNull') is not null

ALTER TABLE [TestAnswers]
   drop column AccountId;

Go

if columnproperty(object_id('WorkTestAnswers'),'FK_dbo.WorkTestAnswers_dbo.TestAnswers_TestAnswerId','AllowsNull') is not null
	alter table [WorkTestAnswers]
		drop constraint [FK_dbo.WorkTestAnswers_dbo.TestAnswers_TestAnswerId];

go