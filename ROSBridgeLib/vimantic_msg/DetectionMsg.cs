using System;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using ROSUnityCore.ROSBridgeLib.vision_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class DetectionMsg : ROSBridgeMsg {

                public ObjectHypothesisMsg[] _scores { get; private set; }
                public PoseMsg _origin { get; private set; }
                public PoseMsg _pose { get; private set; }
                public Vector3Msg _size { get; private set; }


                public DetectionMsg(JSONNode msg) {
                    _scores = new ObjectHypothesisMsg[msg["scores"].Count];
                    for (int i = 0; i < _scores.Length; i++) {
                        _scores[i] = new ObjectHypothesisMsg(msg["scores"][i]);
                    }
                    _origin = new PoseMsg(msg["origin"]);
                    _pose = new PoseMsg(msg["pose"]);
                    _size = new Vector3Msg(msg["size"]);
                }

                public DetectionMsg(ObjectHypothesisMsg[] scores, PoseMsg origin, PoseMsg pose, Vector3Msg size) {
                    _scores = scores;
                    _origin = origin;
                    _pose = pose;
                    _size = size;
                }

                public Dictionary<string, float> GetScores() {
                    Dictionary<string, float> result = new Dictionary<string, float>();
                    foreach (ObjectHypothesisMsg score in _scores) {
                        if (!result.ContainsKey(score._id))
                            result.Add(score._id, score._score);
                    }
                    return result;
                }

                public static string GetMessageType() {
                    return "vimantic/Detection";
                }

                public override string ToString() {
                    string result = ", scores=[";
                    for (int i = 0; i < _scores.Length; i++) {
                        result += _scores[i].ToString();
                        if (i < (_scores.Length - 1))
                            result += ",";
                    }

                    return "SemanticObject [scores=" + result
                        + ", origin=" + _origin.ToString()
                        + ", pose=" + _pose.ToString()
                        + ", size=" + _size.ToString() + "]";
                }

                public override string ToYAMLString() {
                    string result = ",  \"scores\" : [";
                    for (int i = 0; i < _scores.Length; i++) {
                        result += _scores[i].ToYAMLString();
                        if (i < (_scores.Length - 1))
                            result += ",";
                    }
                    return "{\"scores\" : " + result
                        + ", \"origin\" : " + _pose.ToYAMLString()
                        + ", \"pose\" : " + _pose.ToYAMLString()
                        + ", \"size\" : " + _size.ToYAMLString() + "}";
                }
            }
        }
    }
}