using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.nav_msgs;

namespace ROSUnityCore {
    public class Path_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/path";
        }

        public new static string GetMessageType() {
            return "nav_msgs/Path";
        }

        public static string ToYAMLString(PathMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
