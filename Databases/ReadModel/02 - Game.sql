CREATE TABLE [battleship].[Game] (
    [Id] CHAR (36) NOT NULL,
    [Status] TINYINT NULL,
    [CreatedAt] DATETIME2 (7) NOT NULL,
    [UpdatedAt] DATETIME2 (7) NULL,
    CONSTRAINT [PK_GameId] PRIMARY KEY CLUSTERED ([Id] ASC)
);