using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.ViMantic_msgs;

namespace ROSUnityCore {
    public class RoomScores_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/ViMantic/room_scores";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticRoom";
        }

        public static string ToYAMLString(SemanticRoomMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
