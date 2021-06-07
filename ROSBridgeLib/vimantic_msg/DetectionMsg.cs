using System;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class DetectionMsg : ROSBridgeMsg {
                public ObjectHypothesisMsg[] scores { get; private set; }                
                public PointMsg[] corners { get; private set; }
                public byte fixed_corners { get; private set; }


                public DetectionMsg(JSONNode msg) {
                    scores = new ObjectHypothesisMsg[msg["scores"].Count];
                    for (int i = 0; i < scores.Length; i++) {
                        scores[i] = new ObjectHypothesisMsg(msg["scores"][i]);
                    }
                    corners = new PointMsg[msg["corners"].Count];
                    for (int i = 0; i < corners.Length; i++) {
                        corners[i] = new PointMsg(msg["corners"][i]);
                    }
                    fixed_corners = byte.Parse(msg["data"], System.Globalization.CultureInfo.InvariantCulture);
                }

                public DetectionMsg(ObjectHypothesisMsg[] scores, PointMsg[] corners, byte fixed_corners) {
                    this.scores = scores;
                    this.corners = corners;
                    this.fixed_corners = fixed_corners;
                }

                public Dictionary<string, float> GetScores() {
                    Dictionary<string, float> result = new Dictionary<string, float>();
                    foreach (ObjectHypothesisMsg score in scores) {
                        if (!result.ContainsKey(score._id))
                            result.Add(score._id, score._score);
                    }
                    return result;
                }

                public List<Vector3> GetCorners() {
                    List<Vector3> result = new List<Vector3>();
                    foreach(PointMsg pt in corners) {
                        result.Add(pt.GetPoint());
                    }

                    return result;
                }

                public static string GetMessageType() {
                    return "vimantic/Detection";
                }

                public override string ToString() {
                    string result1 = "[";
                    for (int i = 0; i < scores.Length; i++) {
                        result1 += scores[i].ToString();
                        if (i < (scores.Length - 1))
                            result1 += ",";
                    }
                    result1 += "]";

                    string result2 = "[";
                    for (int i = 0; i < corners.Length; i++) {
                        result2 += corners[i].ToString();
                        if (i < (corners.Length - 1))
                            result2 += ",";
                    }
                    result2 += "]";

                    return "SemanticObject [scores=" + result1
                        + ", corners=" + result2
                        + ", fixed_corners="+ fixed_corners + "]";
                }

                public override string ToYAMLString() {
                    string result1 = "[";
                    for (int i = 0; i < scores.Length; i++) {
                        result1 += scores[i].ToYAMLString();
                        if (i < (scores.Length - 1))
                            result1 += ",";
                    }
                    result1 += "]";
                    string result2 = "[";
                    for (int i = 0; i < corners.Length; i++) {
                        result2 += corners[i].ToYAMLString();
                        if (i < (corners.Length - 1))
                            result2 += ",";
                    }
                    result2 += "]";

                    return "{\"scores\" : " + result1
                        + ", \"corners\" : " + result2
                        + ", \"fixed_corners\" : " + fixed_corners + "}";
                }
            }
        }
    }
}