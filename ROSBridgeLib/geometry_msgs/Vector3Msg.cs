using SimpleJSON;
using UnityEngine;

namespace ROSUnityCore {
	namespace ROSBridgeLib {
		namespace geometry_msgs {

			public class Vector3Msg : ROSBridgeMsg {
				private float _x;
				private float _y;
				private float _z;


				public Vector3Msg(JSONNode msg) {
					_x = float.Parse(msg["x"], System.Globalization.CultureInfo.InvariantCulture);
					_y = float.Parse(msg["y"], System.Globalization.CultureInfo.InvariantCulture);
					_z = float.Parse(msg["z"], System.Globalization.CultureInfo.InvariantCulture);					
				}

				public Vector3Msg(float x, float y, float z) {
					_x = x;
					_y = y;
					_z = z;
				}

				public Vector3Msg(Vector3 vector, bool fromUnityVector = false) {
                    if (fromUnityVector) {
						_x = vector.x;
						_y = vector.z;
						_z = vector.y;
					} else {
						_x = vector.x;
						_y = vector.y;
						_z = vector.z;
					}
				}

				public static string GetMessageType() {
					return "geometry_msgs/Vector3";
				}

				public float GetX() {
					return _x;
				}

				public float GetY() {
					return _y;
				}

				public float GetZ() {
					return _z;
				}

				public Vector3 GetVector3() {
					return new Vector3((float)_x, (float)_y, (float)_z);
				}

				public Vector3 GetVector3Unity() {
					return new Vector3(_x, _z, _y);
				}

				public override string ToString() {
					return "Vector3 [x=" + _x.ToString().Replace(",", ".") + ",  y=" + _y.ToString().Replace(",", ".") + ",  z=" + _z.ToString().Replace(",", ".") + "]";
				}

				public override string ToYAMLString() {
					return "{\"x\" : " + _x.ToString().Replace(",",".") + ", \"y\" : " + _y.ToString().Replace(",", ".") + ", \"z\" : " + _z.ToString().Replace(",", ".") + "}";
				}
			
			}
		}
	}
}