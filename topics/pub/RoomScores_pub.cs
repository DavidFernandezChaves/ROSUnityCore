using ROSBridgeLib;
using ROSBridgeLib.semantic_mapping;

public class RoomScores_pub : ROSBridgePublisher
{

    public new static string GetMessageTopic()
    {
        return "/semantic_mapping/room_scores";
    }

    public new static string GetMessageType()
    {
        return "semantic_mapping/SemanticRoom";
    }

    public static string ToYAMLString(SemanticRoomMsg msg)
    {
        return msg.ToYAMLString();
    }

}

