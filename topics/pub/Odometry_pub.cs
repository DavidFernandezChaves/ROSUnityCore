using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.nav_msgs;

namespace ROSUnityCore {
    public class Odometry_pub : ROSBridgePublisher
    {
        public new static string GetMessageTopic() {
            return "RobotAtVirtualHome/odom";
        }

        public new static string GetMessageType() {
            return "nav_msgs/Odometry";
        }

        public static string ToYAMLString(OdometryMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
