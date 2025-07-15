CREATE TABLE [battleship].[Vessel] (
    [Id] CHAR(36) NOT NULL,
    [PlayerId] CHAR(36) NOT NULL,
    [OccupationType] TINYINT NULL,
    [Width] INT NOT NULL,
    [Hits] INT NOT NULL,
    CONSTRAINT [PK_VesselId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Player_VesselId] FOREIGN KEY ([PlayerId]) REFERENCES [battleship].[Player]([Id])
);