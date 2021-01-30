using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace ViMantic_msgs {

            public class SemanticRoomMsg : ROSBridgeMsg {
                private HeaderMsg _header;
                private String _id;
                private SemanticRoomScoreMsg[] _probabilities;


                public SemanticRoomMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _id = msg["id"];
                    _probabilities = new SemanticRoomScoreMsg[msg["probabilities"].Count];
                    for (int i = 0; i < _probabilities.Length; i++) {
                        _probabilities[i] = new SemanticRoomScoreMsg(msg["probabilities"][i]);
                    }
                }

                public SemanticRoomMsg(HeaderMsg header, String id, SemanticRoomScoreMsg[] probabilities) {
                    _header = header;
                    _id = id;
                    _probabilities = probabilities;
                }

                public static string GetMessageType() {
                    return "semantic_mapping/SemanticRoom";
                }

                public HeaderMsg GetHeader() {
                    return _header;
                }

                public SemanticRoomScoreMsg[] GetProbabilities() {
                    return _probabilities;
                }

                public override string ToString() {
                    string prob = ", probabilities=[";
                    for (int i = 0; i < _probabilities.Length; i++) {
                        prob += _probabilities[i].ToString();
                        if (i < (_probabilities.Length - 1))
                            prob += ",";
                    }
                    return "Detection [header=" + _header.ToString() + ", id=" + _id + prob + "]]";
                }

                public override string ToYAMLString() {
                    string prob = ",  \"probabilities\" : [";
                    for (int i = 0; i < _probabilities.Length; i++) {
                        prob += _probabilities[i].ToYAMLString();
                        if (i < (_probabilities.Length - 1))
                            prob += ",";
                    }
                    return "{\"header\" : " + _header.ToYAMLString() + ", \"id\" : \"" + _id + "\"" + prob + "]}";
                }
            }
        }
    }
}