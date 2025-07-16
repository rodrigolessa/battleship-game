CREATE TABLE [battleship].[GameBoard] (
    [Id] CHAR (36) NOT NULL,
    [PlayerId] CHAR (36) NOT NULL,
    [IsAFireBoard] BIT NULL,
    CONSTRAINT [PK_GameBoardId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlayerId_GameBoard] FOREIGN KEY ([PlayerId]) REFERENCES [battleship].[Player]([Id])
);