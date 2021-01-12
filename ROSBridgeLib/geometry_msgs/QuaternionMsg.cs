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

                //public QuaternionMsg(float x, float y, float z, float w) {
                //    _x = x;
                //    _y = y;
                //    _z = z;
                //    _w = w;
                //}

                //public QuaternionMsg(float yaw, float pitch, float roll) {
                //    float halfYaw = yaw * 0.5f;
                //    float halfPitch = pitch * 0.5f;
                //    float halfRoll = roll * 0.5f;
                //    float cosYaw = Mathf.Cos(halfYaw);
                //    float sinYaw = Mathf.Sin(halfYaw);
                //    float cosPitch = Mathf.Cos(halfPitch);
                //    float sinPitch = Mathf.Sin(halfPitch);
                //    float cosRoll = Mathf.Cos(halfRoll);
                //    float sinRoll = Mathf.Sin(halfRoll);

                //    _x = sinRoll * cosPitch * cosYaw - cosRoll * sinPitch * sinYaw;
                //    _y = cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw;
                //    _z = cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw;
                //    _w = cosRoll * cosPitch * cosYaw + sinRoll * sinPitch * sinYaw;

                //}

                //public QuaternionMsg(Quaternion q) : this(-(q.eulerAngles * Mathf.Deg2Rad).y,
                //                                        (q.eulerAngles * Mathf.Deg2Rad).x,
                //                                        (q.eulerAngles * Mathf.Deg2Rad).z) { 
                //    q = Quaternion.Normalize(q);
                //    Debug.LogWarning("KK"+q.ToString("F6") + "/"+ new Quaternion(_y, _z, -_x, -_w).ToString("F6"));
                //}

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


                //public Vector3 GetRotationEulerROS(int solution) {
                //    return TfUtils.GetEulerYPR(new Quaternion((float)_x, (float)_y, (float)_z, (float)_w), solution);
                //}

                //public Vector3 GetRotationEulerUnity(int solution) {
                //    Vector3 rotationEuler = TfUtils.GetEulerYPR(new Quaternion((float)_x, (float)_y, (float)_z, (float)_w), solution) * Mathf.Rad2Deg;
                //    return new Vector3(-rotationEuler.y, -rotationEuler.x, rotationEuler.z);
                //}

                //public Quaternion GetQuaternionUnity(int solution) {
                //    //Debug.LogWarning(new Quaternion(_y, _z, -_x, -_w)+"/"+ Quaternion.Euler(GetRotationEulerUnity(solution)));
                //    return new Quaternion(-_y, -_z, _x, _w);
                //    //return Quaternion.Euler(GetRotationEulerUnity(solution));
                //}

                //public float GetX() {
                //    return (float)_x;
                //}

                //public float GetY() {
                //    return (float)_y;
                //}

                //public float GetZ() {
                //    return (float)_z;
                //}

                //public float GetW() {
                //    return (float)_w;
                //}

                public override string ToString() {
                    return "Quaternion [x=" + _x.ToString().Replace(",", ".") + ",  y=" + _y.ToString().Replace(",", ".") + ",  z=" + _z.ToString().Replace(",", ".") + ",  w=" + _w.ToString().Replace(",", ".") + "]";
                }

                public override string ToYAMLString() {
                    return "{\"x\" : " + _x.ToString().Replace(",", ".") + ", \"y\" : " + _y.ToString().Replace(",", ".") + ", \"z\" : " + _z.ToString().Replace(",", ".") + ", \"w\" : " + _w.ToString().Replace(",", ".") + "}";
                }
            }
        }
    }
}