CREATE TABLE [eventstore].[GameEvent] (
    [GameId] CHAR (36) NOT NULL, -- Lexicographical ordering
    [EventKey] CHAR(36) NOT NULL, -- Duplicate event detection (idempotency key)
    [EventType] VARCHAR(MAX) NULL,
    [EventVersion] TINYINT NULL,
    [EventSequence] INT NOT NULL, -- Optimistic concurrency
    [EventCommittedTimestamp] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_GameEvent] PRIMARY KEY CLUSTERED ([EventKey] ASC)
);