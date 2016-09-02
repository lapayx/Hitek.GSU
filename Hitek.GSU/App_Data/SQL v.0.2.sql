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