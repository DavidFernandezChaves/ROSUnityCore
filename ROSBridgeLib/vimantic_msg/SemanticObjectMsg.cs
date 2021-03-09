using System;
using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using ROSUnityCore.ROSBridgeLib.vision_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class SemanticObjectMsg : ROSBridgeMsg {

                public ObjectHypothesisWithPoseMsg _object { get; private set; }
                public string _objectType { get; private set; }
                public string _roomId { get; private set; }
                private int _detections;
                public string _roomType { get; private set; }
                public Vector3Msg _size { get; private set; }


                public SemanticObjectMsg(JSONNode msg) {
                    _object = new ObjectHypothesisWithPoseMsg(msg["object"]);
                    _objectType = msg["objectType"];
                    _detections = int.Parse(msg["detections"]);
                    _roomId = msg["roomId"];
                    _roomType = msg["roomType"];
                    _size = new Vector3Msg(msg["size"]);
                }

                public SemanticObjectMsg(ObjectHypothesisWithPoseMsg object1, String objectType, int ndetections, String roomId, String roomType, Vector3Msg size) {
                    _object = object1;
                    _objectType = objectType;
                    _detections = ndetections;
                    _roomId = roomId;
                    _roomType = roomType;
                    _size = size;
                }

                public SemanticObjectMsg(SemanticObject obj) {
                    _object = new ObjectHypothesisWithPoseMsg(obj.ontologyId, obj.score, new PoseWithCovarianceMsg(new PoseMsg(obj.pose, obj.rotation), new double[0]));
                    _objectType = obj.type;
                    _detections = obj.nDetections;
                    _roomId = obj.GetIdRoom();
                    _roomType = obj.semanticRoom.roomType;
                    _size = new Vector3Msg(obj.size);
                }

                public static string GetMessageType() {
                    return "vimantic/SemanticObject";
                }

                public override string ToString() {
                    return "SemanticObject [object=" + _object.ToString() 
                        + ", objectType=" + _objectType 
                        + ", roomId=" + _roomId 
                        + ", detections=" + _detections
                        + ", roomType=" + _roomType 
                        + ", size=" + _size.ToString() + "]";
                }

                public override string ToYAMLString() {
                    return "{\"object\" : " + _object.ToYAMLString() 
                        + ", \"objectType\" : \"" + _objectType 
                        + "\", \"roomId\" : \"" + _roomId
                        + "\", \"detections\" : " + _detections
                        + ", \"roomType\" : \"" + _roomType 
                        + "\", \"size\" : " + _size.ToYAMLString() + "}";
                }
            }
        }
    }
}