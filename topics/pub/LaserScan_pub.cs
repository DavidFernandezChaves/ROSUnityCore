using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;

namespace ROSUnityCore {
    public class LaserScan_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/RobotAtVirtualHome/scan";
        }

        public new static string GetMessageType() {
            return "sensor_msgs/LaserScan";
        }

        public static string ToYAMLString(LaserScanMsg msg) {
            return msg.ToYAMLString();
        }

    }
}