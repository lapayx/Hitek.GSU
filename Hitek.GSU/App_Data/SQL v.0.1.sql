CREATE TABLE [AspNetUsers] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

go

CREATE TABLE [AspNetRoles] (
    [Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO


CREATE TABLE [AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     BIGINT         NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] BIGINT NOT NULL,
    [RoleId] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

go


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[TestSubjects] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (200) NOT NULL,
    [ParentId] BIGINT         NULL,
    [IsHide]   BIT            NOT NULL,
    CONSTRAINT [PK_dbo.TestSubjects] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE TABLE [Tests] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [TestSubjectId]        BIGINT         NULL,
    [AutorId]              BIGINT         NOT NULL,
    [IsHide]               BIT            NOT NULL,
    [CountQuestionForShow] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Tests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Tests_dbo.TestSubjects_TestSubjectId] FOREIGN KEY ([TestSubjectId]) REFERENCES [TestSubjects] ([Id])
);


GO

CREATE TABLE [dbo].[TestQuestions] (
    [Id]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (200) NOT NULL,
    [Text]   NVARCHAR (MAX) NOT NULL,
    [TestId] BIGINT         NOT NULL,
    [IsHide] BIT            NOT NULL,
    CONSTRAINT [PK_dbo.TestQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TestQuestions_dbo.Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Tests] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [dbo].[TestAnswers] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Text]           NVARCHAR (200) NOT NULL,
    [IsRight]        BIT            NOT NULL,
    [TestQuestionId] BIGINT         NOT NULL,
    [IsHide]         BIT            NOT NULL,
    [AccountId]      BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.TestAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TestAnswers_dbo.TestQuestions_TestQuestionId] FOREIGN KEY ([TestQuestionId]) REFERENCES [dbo].[TestQuestions] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [dbo].[TestHistories] (
    [Id]        BIGINT   IDENTITY (1, 1) NOT NULL,
    [Result]    REAL     NOT NULL,
    [TestId]    BIGINT   NOT NULL,
    [AccountId] BIGINT   NOT NULL,
    [Date]      DATETIME DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.TestHistories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TestHistories_dbo.Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Tests] ([Id]) ON DELETE CASCADE
);


GO
