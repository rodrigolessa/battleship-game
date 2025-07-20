namespace frm.Infrastructure.Messaging.Enumerations;

// RabbitMQ can transport any type of message, as long as it's a binary payload (byte[]).
// That means:
// RabbitMQ is agnostic to the payload format — it does not inspect or validate the message body.
// You’re responsible for serializing and deserializing on both sides.

public enum MessageType
{
    Unknow = 0,
    PlainText = 1,
    Json = 2,
    Xml = 3,
    Protobuf = 4,
    Avro = 5,
    MsgPack = 6,
    RawBinary = 7
}