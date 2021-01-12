using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.semantic_mapping;

namespace ROSUnityCore {
    public class ObjectsInRoom_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/vimantic/object_in_room";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticObjects";
        }

        public static string ToYAMLString(SemanticObjectsMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
