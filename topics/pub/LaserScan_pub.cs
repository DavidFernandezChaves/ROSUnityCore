using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;

public class LaserScan_pub : ROSBridgePublisher {
    
    public new static string GetMessageTopic() {
        return "/scan";
    }
    
    public new static string GetMessageType() {
        return "sensor_msgs/LaserScan";
    }
    
    public static string ToYAMLString(LaserScanMsg msg) {
        return msg.ToYAMLString();
    }
    
}