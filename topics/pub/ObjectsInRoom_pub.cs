using ROSBridgeLib;
using ROSBridgeLib.semantic_mapping;

public class ObjectsInRoom_pub : ROSBridgePublisher
{

    public new static string GetMessageTopic()
    {
        return "/semantic_mapping/object_in_room";
    }

    public new static string GetMessageType()
    {
        return "semantic_mapping/SemanticObjects";
    }

    public static string ToYAMLString(SemanticObjectsMsg msg)
    {
        return msg.ToYAMLString();
    }

}

