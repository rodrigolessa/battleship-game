CREATE TABLE [eventstore].[GameEventData] (
    [EventKey] CHAR (36) NOT NULL,
    [EventData] VARBINARY (MAX) NULL, 
    CONSTRAINT [PK_GameEventData] PRIMARY KEY CLUSTERED ([EventKey] ASC),
    CONSTRAINT [FK_GameEventData_EventKey] FOREIGN KEY ([EventKey]) REFERENCES [eventstore].[GameEvent]([EventKey])
);
GO