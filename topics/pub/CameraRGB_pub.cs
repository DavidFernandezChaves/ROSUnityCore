using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;

namespace ROSUnityCore {
    public class CameraRGB_pub : ROSBridgePublisher {

        public new static string GetMessageTopic() {
            return "/vimantic/virtualCameraRGBD";
        }

        public new static string GetMessageType() {
            return "sensor_msgs/CompressedImage";
        }

        public static string ToYAMLString(CompressedImageMsg msg) {
            return msg.ToYAMLString();
        }

    }
}
