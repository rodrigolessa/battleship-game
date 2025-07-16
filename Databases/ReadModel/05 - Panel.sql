CREATE TABLE [battleship].[Panel] (
    [Id] CHAR (36) NOT NULL,
    [GameBoardId] CHAR (36) NOT NULL,
    [OccupationType] TINYINT NULL,
    [CoordinateRow] TINYINT NULL,
    [CoordinateColumn] TINYINT NULL,
    CONSTRAINT [PK_PanelId] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_GameBoardId_Panel] FOREIGN KEY ([GameBoardId]) REFERENCES [battleship].[GameBoard]([Id])
);
