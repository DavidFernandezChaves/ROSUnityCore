using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;

namespace ROSUnityCore {
    public class CameraDepth_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/RobotAtVirtualHome/VirtualCameraDepth";
        }

        public new static string GetMessageType() {
            return "sensor_msgs/CompressedImage";
        }

        public static string ToYAMLString(CompressedImageMsg msg) {
            return msg.ToYAMLString();
        }

    }

}