using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using System;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace vision_msgs {

            public class ObjectHypothesisWithPoseMsg : ROSBridgeMsg {

                public String _id { get; private set; }
                public float _score { get; private set; }
                public PoseWithCovarianceMsg _pose { get; private set; }


                public ObjectHypothesisWithPoseMsg(JSONNode msg) {
                    _id = msg["id"];
                    _score = float.Parse(msg["score"], System.Globalization.CultureInfo.InvariantCulture);
                    _pose = new PoseWithCovarianceMsg(msg["pose"]);
                }

                public ObjectHypothesisWithPoseMsg(String id, float score, PoseWithCovarianceMsg position) {
                    _id = id;
                    _score = score;
                    _pose = position;
                }

                public static string GetMessageType() {
                    return "vision_msgs/ObjectHypothesisWithPose";
                }

                public override string ToString() {
                    return "Object [id=" + _id + ", score=" + _score.ToString().Replace(",", ".") + ", pose=" + _pose.ToString() + "]";
                }

                public override string ToYAMLString() {
                    return "{\"id\" : \"" + _id + "\", \"score\" : " + _score.ToString().Replace(",", ".") + ", \"pose\" : " + _pose.ToYAMLString() + "}";
                }
            }
        }
    }
}