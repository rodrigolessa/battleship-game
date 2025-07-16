CREATE TABLE [battleship].[Player] (
    [Id] CHAR (36) NOT NULL,
    [GameId] CHAR (36) NOT NULL,
    [Name] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_PlayerId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GameId_Player] FOREIGN KEY ([GameId]) REFERENCES [battleship].[Game]([Id])
);
