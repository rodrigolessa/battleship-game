CREATE TABLE [eventstore].[GameSnapshot] (
    [GameId] CHAR (36) NOT NULL,
    [SnapshotKey] CHAR(36) NOT NULL,
    [SnapshotType] VARCHAR (MAX) NULL,
    [SnapshotData] VARBINARY (MAX) NULL,
    [SequenceOfLastEvent] INT NOT NULL, -- Trace the last event sequence
    [SnapshotExpireAt] DATETIME2 (7) NULL,
    [SnapshotCommittedTimestamp] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_GameSnapshot] PRIMARY KEY CLUSTERED ([SnapshotKey] ASC)
);