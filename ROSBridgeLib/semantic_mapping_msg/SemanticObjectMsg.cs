using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using System;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace semantic_mapping {

            public class SemanticObjectMsg : ROSBridgeMsg {
                private String _id;
                private String _idRoom;
                private String _type;
                private double _score;
                private PointCloudMsg _pointCloud;
                private PoseMsg _pose;
                private Vector3Msg _scale;


                public SemanticObjectMsg(JSONNode msg) {
                    _id = msg["id"];
                    _idRoom = msg["idRoom"];
                    _type = msg["type"];
                    _score = double.Parse(msg["score"], System.Globalization.CultureInfo.InvariantCulture);
                    _pointCloud = new PointCloudMsg(msg["pointCloud"]);
                    _pose = new PoseMsg(msg["pose"]);
                    _scale = new Vector3Msg(msg["scale"]);
                }

                public SemanticObjectMsg(String id, String idRoom, String type, float score, PointCloudMsg pointCloud, PoseMsg position, Vector3Msg scale) {
                    _id = id;
                    _idRoom = idRoom;
                    _type = type;
                    _score = score;
                    _pointCloud = pointCloud;
                    _pose = position;
                    _scale = scale;
                }

                public SemanticObjectMsg(SemanticObject obj) {
                    _id = obj.ontologyID;
                    _idRoom = obj.GetIdRoom();
                    _type = obj.type;
                    _score = obj.confidenceScore;
                    _pose = new PoseMsg(obj.pose, obj.rotation);
                    _scale = new Vector3Msg(obj.size);


                    Point32Msg[] points = new Point32Msg[0];
                    ChannelFloat32[] chanels = new ChannelFloat32[0];
                    HeaderMsg header = new HeaderMsg(0, new TimeMsg(0, 0), "null");
                    _pointCloud = new PointCloudMsg(header, points, chanels);

                }

                public static string GetMessageType() {
                    return "semantic_mapping/SemanticObject";
                }

                public String GetId() {
                    return _id;
                }

                public String GetIdRoom() {
                    return _idRoom;
                }

                public String GetTypeObject() {
                    return _type;
                }

                public double GetConfidenceScore() {
                    return _score;
                }

                public PointCloudMsg GetPointCloud() {
                    return _pointCloud;
                }

                public PoseMsg GetPose() {
                    return _pose;
                }

                public Vector3Msg GetScale() {
                    return _scale;
                }

                public override string ToString() {
                    return "Detection [id=" + _id + ", idRoom=" + _idRoom + ", type=" + _type + ", score=" + _score + ", pointCloud=" + _pointCloud.ToString() + ", pose=" + _pose.ToString() + ", scale=" + _scale.ToString() + "]";
                }

                public override string ToYAMLString() {
                    return "{\"id\" : \"" + _id + "\", \"idRoom\" : \"" + _idRoom + "\", \"type\" : \"" + _type + "\", \"score\" : " + _score.ToString("N", System.Globalization.CultureInfo.InvariantCulture) + ", \"pointCloud\" : " + _pointCloud.ToYAMLString() + ", \"pose\" : " + _pose.ToYAMLString() + ", \"scale\" : " + _scale.ToYAMLString() + "}";
                }
            }
        }
    }
}