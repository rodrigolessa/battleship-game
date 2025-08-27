CREATE TABLE [eventstore].[GameEvent] (
    [EventKey] CHAR(36) NOT NULL, -- PRIMARY KEY
    [EventType] VARCHAR(MAX) NULL,
    [EventVersion] TINYINT NULL,
    [EventSequence] INT NOT NULL, -- Optimistic concurrency. To avoid dirty read and other problem of Race Conditions, we only persist data if the sequence is the same as we read before. Conditional UPDATE and in this case will be a conditional INSERT
    [EventCommittedTimestamp] DATETIME2 (7) NOT NULL,
    [AggregateId] CHAR (36) NOT NULL, -- Lexicographical ordering
    [IdempotencyKey] CHAR(36) NOT NULL, -- Duplicate event detection (idempotency key). Should be unique
    [CorrelationKey] CHAR(36) NULL
    [SagaProcessKey] CHAR(36) NULL
    CONSTRAINT [PK_GameEvent] PRIMARY KEY CLUSTERED ([EventKey] ASC)
);