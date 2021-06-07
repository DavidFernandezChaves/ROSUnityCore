using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class DetectionArrayMsg : ROSBridgeMsg {
                public HeaderMsg header { get; private set; }
                public PoseMsg origin { get; private set; }
                public DetectionMsg[] detections { get; private set; }


                public DetectionArrayMsg(JSONNode msg) {
                    header = new HeaderMsg(msg["header"]);
                    origin = new PoseMsg(msg["origin"]);
                    detections = new DetectionMsg[msg["detections"].Count];
                    for (int i = 0; i < detections.Length; i++) {
                        detections[i] = new DetectionMsg(msg["detections"][i]);
                    }
                }

                public DetectionArrayMsg(HeaderMsg header, PoseMsg origin, DetectionMsg[] semanticObjects) {
                    this.header = header;
                    this.origin = origin;
                    detections = semanticObjects;
                }

                public static string GetMessageType() {
                    return "vimantic/DetectionArray";
                }

                public override string ToString() {
                    string result = ", detections=[";
                    for (int i = 0; i < detections.Length; i++) {
                        result += detections[i].ToString();
                        if (i < (detections.Length - 1))
                            result += ",";
                    }
                    return "Detection [header=" + header.ToString()
                        + ", origin=" + origin.ToString()
                        + result + "]]";
                }

                public override string ToYAMLString() {
                    string result = ",  \"detections\" : [";
                    for (int i = 0; i < detections.Length; i++) {
                        result += detections[i].ToYAMLString();
                        if (i < (detections.Length - 1))
                            result += ",";
                    }
                    result += "]";

                    return "{\"header\" : " + header.ToYAMLString()
                        + ", \"origin\" : " + origin.ToYAMLString() 
                        + result + "}";
                }
            }
        }
    }
}