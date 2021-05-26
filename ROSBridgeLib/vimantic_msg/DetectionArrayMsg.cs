using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class DetectionArrayMsg : ROSBridgeMsg {
                private HeaderMsg _header;
                private DetectionMsg[] _detections;


                public DetectionArrayMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _detections = new DetectionMsg[msg["detections"].Count];
                    for (int i = 0; i < _detections.Length; i++) {
                        _detections[i] = new DetectionMsg(msg["detections"][i]);
                    }
                }

                public DetectionArrayMsg(HeaderMsg header, DetectionMsg[] semanticObjects) {
                    _header = header;
                    _detections = semanticObjects;
                }

                public static string GetMessageType() {
                    return "vimantic/DetectionArray";
                }

                public HeaderMsg GetHeader() {
                    return _header;
                }

                public DetectionMsg[] GetDetections() {
                    return _detections;
                }

                public override string ToString() {
                    string result = ", detections=[";
                    for (int i = 0; i < _detections.Length; i++) {
                        result += _detections[i].ToString();
                        if (i < (_detections.Length - 1))
                            result += ",";
                    }
                    return "Detection [header=" + _header.ToString() + result + "]]";
                }

                public override string ToYAMLString() {
                    string result = ",  \"detections\" : [";
                    for (int i = 0; i < _detections.Length; i++) {
                        result += _detections[i].ToYAMLString();
                        if (i < (_detections.Length - 1))
                            result += ",";
                    }
                    return "{\"header\" : " + _header.ToYAMLString() + result + "]}";
                }
            }
        }
    }
}