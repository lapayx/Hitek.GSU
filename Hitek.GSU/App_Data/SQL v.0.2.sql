CREATE TABLE [RefreshToken] (
    [Id]        NVARCHAR(100)    NOT NULL,
    [Subject]    NVARCHAR(50)     NOT NULL,
    [ClientId]    NVARCHAR(50)   NOT NULL,
	[AccountId]		BIGINT,
    [IssuedUtc]      DATETIME DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
	[ExpiresUtc]      DATETIME DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
	[ProtectedTicket]        NVARCHAR(MAX)    NOT NULL,
    CONSTRAINT [PK_dbo.RefreshToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE TABLE [Client] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Secret]               NVARCHAR (MAX) NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [ApplicationType]      INT            NOT NULL,
    [Active]               BIT            NOT NULL,
    [RefreshTokenLifeTime] INT            NOT NULL,
    [AllowedOrigin]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE TABLE [WorkTests] (
    [Id]        BIGINT   IDENTITY (1, 1) NOT NULL,
    [TestId]    BIGINT   NOT NULL,
    [UserId]    BIGINT   NOT NULL,
	[Name]    NVARCHAR(200)   NOT NULL,
    [StartDate] DATETIME NOT NULL,
    [EndDate]   DATETIME NULL,
	[Result] REAL NOT NULL DEFAULT 0, 
	[IsCanShowResultAnswer] Bit not null default 0,
    CONSTRAINT [PKWorkTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKWorkTests_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
);
Go

CREATE TABLE [WorkTestQuestions] (
    [Id]             BIGINT IDENTITY (1, 1) NOT NULL,
    [WorkTestId]     BIGINT NOT NULL,
    [TestQuestionId] BIGINT NOT NULL,
    [Name] NVARCHAR(200) NOT NULL, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PKWorkTestQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKWorkTestQuestions_TestQuestions_TestQuestionId] FOREIGN KEY ([TestQuestionId]) REFERENCES [TestQuestions] ([Id]),
    CONSTRAINT [FKWorkTestQuestions_WorkTests_WorkTestId] FOREIGN KEY ([WorkTestId]) REFERENCES [WorkTests] ([Id]) 
);
go

CREATE TABLE [WorkTestAnswers] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [WorkTestQuestionId]      BIGINT         NOT NULL,
    [TestAnswerId]        BIGINT         NOT NULL,
    [IsAnswered]          BIT            DEFAULT ((0)) NOT NULL,
    [DateAnswered]        DATETIME       NULL,
    [Text]                NVARCHAR (200) NOT NULL,
    [isRight]             BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WorkTestAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkTestAnswersWorkTestQuestions_WorkTestQuestion_Id] FOREIGN KEY ([WorkTestQuestionId]) REFERENCES [WorkTestQuestions] ([Id]),
    CONSTRAINT [FK_WorkTestAnswers_TestAnswers_TestAnswerId] FOREIGN KEY ([TestAnswerId]) REFERENCES [TestAnswers] ([Id])
);


go


insert into [WorkTests] (TestId,UserId,EndDate,Name,Result,StartDate) 
SELECT i.TestId,i.AccountId,i.Date,t.Name,i.Result, '2000-08-01' from TestHistories i inner join Tests t on i.TestId=t.Id

 Drop table [TestHistories];
 go
 Drop table [__MigrationHistory];
 go

 INSERT INTO [Client] ([Id], [Secret], [Name], [ApplicationType], [Active], [RefreshTokenLifeTime], [AllowedOrigin]) VALUES (N'ngAuthApp', N'IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw', N'Angular', 0, 1, 14400, N'*')
go

