using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.ViMantic_msgs;

namespace ROSUnityCore {
    public class ObjectsInRoom_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/ViMantic/objectsInRoom";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticObjectArray";
        }

        public static string ToYAMLString(SemanticObjectArrayMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
