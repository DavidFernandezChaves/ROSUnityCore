using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using System.Collections.Generic;


namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace nav_msgs {

            public class OdometryMsg : ROSBridgeMsg {
                private HeaderMsg _header;
                private string _child_frame_id;
                private PoseWithCovarianceMsg _pose;
                private TwistWithCovarianceMsg _twist;

                public OdometryMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _child_frame_id = msg["child_frame_id"];
                    _pose = new PoseWithCovarianceMsg(msg["pose"]);
                    _twist = new TwistWithCovarianceMsg(msg["twist"]);
                }

                public OdometryMsg(HeaderMsg header, string child_frame_id, PoseWithCovarianceMsg pose, TwistWithCovarianceMsg twist) {
                    _header = header;
                    _child_frame_id = child_frame_id;
                    _pose = pose;
                    _twist = twist;
                }

                public static string GetMessageType() {
                    return "nav_msgs/Odometry";
                }

                public HeaderMsg GetHeader() {
                    return _header;
                }

                public string GetChild_frame_id() {
                    return _child_frame_id;
                }

                public PoseWithCovarianceMsg GetPose() {
                    return _pose;
                }

                public TwistWithCovarianceMsg GetTwist() {
                    return _twist;
                }


                public override string ToString() {
                    return "Twist [header=" + _header.ToString() + ", child_frame_id=" + _child_frame_id + ", pose=" + _pose.ToString() + ", twist=" + _twist.ToString() + "]";
                }

                public override string ToYAMLString() {
                    return "{\"header\" : " + _header.ToYAMLString() + ", \"child_frame_id\" : \"" + _child_frame_id + "\", \"pose\" : " + _pose.ToYAMLString() + ", \"twist\" : " + _twist.ToYAMLString() + "}";
                }
            }
        }
    }
}