using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.semantic_mapping;

namespace ROSUnityCore {
    public class RoomScores_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/vimantic/room_scores";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticRoom";
        }

        public static string ToYAMLString(SemanticRoomMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
