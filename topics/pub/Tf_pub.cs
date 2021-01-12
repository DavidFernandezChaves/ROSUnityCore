using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.move_base_msgs;
using ROSUnityCore.ROSBridgeLib.tf2_msgs;

namespace ROSUnityCore {
    public class Tf_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/tf";
        }

        public new static string GetMessageType() {
            return "tf2_msgs/TFMessage";
        }

        public static string ToYAMLString(TFMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
