using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.nav_msgs;

namespace ROSUnityCore {
    public class Map_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/RobotAtVirtualHome/Map";
        }

        public new static string GetMessageType() {
            return "nav_msgs/OccupancyGrid";
        }

        public static string ToYAMLString(PathMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
