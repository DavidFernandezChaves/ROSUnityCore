using System;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class SemanticObjectMsg : ROSBridgeMsg {

                public string _id { get; private set; }                
                public ObjectHypothesisMsg[] _scores { get; private set; }           
                public PoseMsg _pose { get; private set; }
                public string _roomId { get; private set; }
                public int _detections { get; private set; }
                public string _roomType { get; private set; }
                public Vector3Msg _size { get; private set; }


                public SemanticObjectMsg(JSONNode msg) {
                    _id = msg["id"];
                    _scores = new ObjectHypothesisMsg[msg["scores"].Count];
                    for (int i = 0; i < _scores.Length; i++) {
                        _scores[i] = new ObjectHypothesisMsg(msg["scores"][i]);
                    }
                    _pose = new PoseMsg(msg["pose"]);                    
                    _detections = int.Parse(msg["detections"]);
                    _roomId = msg["roomId"];
                    _roomType = msg["roomType"];
                    _size = new Vector3Msg(msg["size"]);
                }

                public SemanticObjectMsg(string id, ObjectHypothesisMsg[] scores, PoseMsg pose, int ndetections, String roomId, String roomType, Vector3Msg size) {
                    _id = id;
                    _scores = scores;
                    _pose = pose;
                    _detections = ndetections;
                    _roomId = roomId;
                    _roomType = roomType;
                    _size = size;
                }

                public SemanticObjectMsg(SemanticObject obj) {
                    _id = obj.id;
                    _scores = new ObjectHypothesisMsg[obj.scores.Count];
                    int i = 0;
                    foreach(KeyValuePair<string,float> score in obj.scores) {
                        _scores[i] = new ObjectHypothesisMsg(score.Key, score.Value);
                        i++;
                    }
                    _pose = new PoseMsg(obj.pose, obj.rotation);
                    _detections = obj.nDetections;
                    _roomId = obj.GetIdRoom();
                    _roomType = obj.room.roomType;
                    _size = new Vector3Msg(obj.size);
                }

                public Dictionary<string,float> GetScores() {
                    Dictionary<string, float> result = new Dictionary<string, float>();
                    foreach(ObjectHypothesisMsg score in _scores) {
                        if(!result.ContainsKey(score._id))
                            result.Add(score._id, score._score);
                    }
                    return result;
                }

                public static string GetMessageType() {
                    return "vimantic/SemanticObject";
                }

                public override string ToString() {
                    string result = ", scores=[";
                    for (int i = 0; i < _scores.Length; i++) {
                        result += _scores[i].ToString();
                        if (i < (_scores.Length - 1))
                            result += ",";
                    }

                    return "SemanticObject [id =" + _id
                        + ", scores=" + result
                        + ", pose=" + _pose.ToString()
                        + ", roomId=" + _roomId 
                        + ", detections=" + _detections
                        + ", roomType=" + _roomType 
                        + ", size=" + _size.ToString() + "]";
                }

                public override string ToYAMLString() {
                    string result = ",  \"scores\" : [";
                    for (int i = 0; i < _scores.Length; i++) {
                        result += _scores[i].ToYAMLString();
                        if (i < (_scores.Length - 1))
                            result += ",";
                    }
                    return "{\"id\" : \"" + _id 
                        + "\", \"scores\" : " + result
                        + ", \"pose\" : " + _pose.ToYAMLString()
                        + ", \"roomId\" : \"" + _roomId
                        + "\", \"detections\" : " + _detections
                        + ", \"roomType\" : \"" + _roomType 
                        + "\", \"size\" : " + _size.ToYAMLString() + "}";
                }
            }
        }
    }
}