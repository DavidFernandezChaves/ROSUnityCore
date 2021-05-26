using SimpleJSON;
using UnityEngine;
using System;


namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace geometry_msgs {

            public class QuaternionMsg : ROSBridgeMsg {
                private float _x;
                private float _y;
                private float _z;
                private float _w;

                public QuaternionMsg(JSONNode msg) {
                    _x = System.Convert.ToSingle(float.Parse(msg["x"], System.Globalization.CultureInfo.InvariantCulture));
                    _y = System.Convert.ToSingle(float.Parse(msg["y"], System.Globalization.CultureInfo.InvariantCulture));
                    _z = System.Convert.ToSingle(float.Parse(msg["z"], System.Globalization.CultureInfo.InvariantCulture));
                    _w = System.Convert.ToSingle(float.Parse(msg["w"], System.Globalization.CultureInfo.InvariantCulture));
                }               

                public QuaternionMsg(Quaternion q,bool fromUnity = false){
                    if (fromUnity) {
                        q = TfUtils.TransormQuaternionUnity2ROS(q);
                    }

                    _x = q.x;
                    _y = q.y;
                    _z = q.z;
                    _w = q.w;
                }


                public static string GetMessageType() {
                    return "geometry_msgs/Quaternion";
                }

                public Quaternion GetQuaternionUnity() {
                    return TfUtils.TransormQuaternionROS2Unity(new Quaternion(_x, _y, _z, _w));
                }

                public Quaternion GetQuaternionROS() {
                    return new Quaternion(_x, _y, _z, _w);
                }

                public override string ToString() {
                    return "Quaternion [x=" + _x.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ",  y=" + _y.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ",  z=" + _z.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ",  w=" + _w.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + "]";
                }

                public override string ToYAMLString() {
                    return "{\"x\" : " + _x.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ", \"y\" : " + _y.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ", \"z\" : " + _z.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + ", \"w\" : " + _w.ToString("G", System.Globalization.CultureInfo.InvariantCulture) + "}";
                }
            }
        }
    }
}