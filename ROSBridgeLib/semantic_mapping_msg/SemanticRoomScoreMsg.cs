using SimpleJSON;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.geometry_msgs;
using System;

namespace ROSBridgeLib
{
    namespace semantic_mapping
    {
        public class SemanticRoomScoreMsg : ROSBridgeMsg
        {
            private String _type;
            private double _score;


            public SemanticRoomScoreMsg(JSONNode msg)
            {
                _type = msg["type"];
                _score = double.Parse(msg["score"], System.Globalization.CultureInfo.InvariantCulture);
            }

            public SemanticRoomScoreMsg(String type, float score)
            {
                _type = type;
                _score = score;
            }

            public static string GetMessageType()
            {
                return "semantic_mapping/SemanticRoomScore";
            }

            public String GetTypeObject()
            {
                return _type;
            }

            public double GetAccuracyEstimation()
            {
                return _score;
            }

            public override string ToString()
            {

                return "Detection [ type=" + _type + ", score=" + _score + "]";
            }

            public override string ToYAMLString()
            {
                return "{\"type\" : \"" + _type + "\", \"score\" : " + _score.ToString("N", System.Globalization.CultureInfo.InvariantCulture) + "}";
            }
        }
    }
}
