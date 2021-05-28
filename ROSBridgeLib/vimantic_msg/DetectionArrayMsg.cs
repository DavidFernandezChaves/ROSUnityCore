using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class DetectionArrayMsg : ROSBridgeMsg {
                public HeaderMsg _header { get; private set; }
                public PoseMsg _origin { get; private set; }
                public DetectionMsg[] _detections { get; private set; }


                public DetectionArrayMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _origin = new PoseMsg(msg["origin"]);
                    _detections = new DetectionMsg[msg["detections"].Count];
                    for (int i = 0; i < _detections.Length; i++) {
                        _detections[i] = new DetectionMsg(msg["detections"][i]);
                    }
                }

                public DetectionArrayMsg(HeaderMsg header, PoseMsg origin, DetectionMsg[] semanticObjects) {
                    _header = header;
                    _origin = origin;
                    _detections = semanticObjects;
                }

                public static string GetMessageType() {
                    return "vimantic/DetectionArray";
                }

                public override string ToString() {
                    string result = ", detections=[";
                    for (int i = 0; i < _detections.Length; i++) {
                        result += _detections[i].ToString();
                        if (i < (_detections.Length - 1))
                            result += ",";
                    }
                    return "Detection [header=" + _header.ToString()
                        + ", origin=" + _origin.ToString()
                        + result + "]]";
                }

                public override string ToYAMLString() {
                    string result = ",  \"detections\" : [";
                    for (int i = 0; i < _detections.Length; i++) {
                        result += _detections[i].ToYAMLString();
                        if (i < (_detections.Length - 1))
                            result += ",";
                    }
                    return "{\"header\" : " + _header.ToYAMLString()
                        + ", \"origin\" : " + _origin.ToYAMLString() 
                        + result + "]}";
                }
            }
        }
    }
}