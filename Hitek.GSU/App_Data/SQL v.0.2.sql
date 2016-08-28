CREATE TABLE [RefreshToken] (
    [Id]        NVARCHAR    NOT NULL,
    [Subject]    NVARCHAR(50)     NOT NULL,
    [ClientId]    NVARCHAR(50)   NOT NULL,
    [AccountId] BIGINT   NOT NULL,
    [IssuedUtc]      DATETIME DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
	[ExpiresUtc]      DATETIME DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
	[ProtectedTicket]        NVARCHAR    NOT NULL,
    CONSTRAINT [PK_dbo.RefreshToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE TABLE [Client] (
    [Id]        NVARCHAR    NOT NULL,
    [Secret]    NVARCHAR(50)     NOT NULL,
    [Name]    NVARCHAR(100)   NOT NULL,
    [ApplicationType] int   NOT NULL,
    [Active]      BIT NOT NULL,
	[RefreshTokenLifeTime]     int NOT NULL,
	[AllowedOrigin]        NVARCHAR(100)    NOT NULL,
    CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO