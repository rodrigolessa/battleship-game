CREATE VIEW [eventstore].[GameData]
AS
SELECT
    e.[GameId],
    e.[EventKey],
    e.[EventType],
    e.[EventVersion],
    e.[EventSequence],
    e.[EventCommittedTimestamp],
    d.[EventData]
FROM [eventstore].[GameEvent] e
INNER JOIN [eventstore].[GameEventData] d ON (e.EventKey = d.EventKey)