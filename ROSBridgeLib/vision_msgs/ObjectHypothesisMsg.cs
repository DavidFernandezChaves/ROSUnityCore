using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using System;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace vision_msgs {

            public class ObjectHypothesisMsg : ROSBridgeMsg {

                public String _id { get; private set; }
                public float _score { get; private set; }


                public ObjectHypothesisMsg(JSONNode msg) {
                    _id = msg["id"];
                    _score = float.Parse(msg["score"], System.Globalization.CultureInfo.InvariantCulture);
                }

                public ObjectHypothesisMsg(String id, float score) {
                    _id = id;
                    _score = score;
                }

                public static string GetMessageType() {
                    return "vision_msgs/ObjectHypothesisWithPose";
                }

                public override string ToString() {
                    return "Object [id=" + _id + ", score=" + _score.ToString("G", System.Globalization.CultureInfo.InvariantCulture) +  "]";
                }

                public override string ToYAMLString() {
                    return "{\"id\" : \"" + _id + "\", \"score\" : " + _score.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + "}";
                }
            }
        }
    }
}