using SimpleJSON;
using UnityEngine;

namespace ROSUnityCore {
	namespace ROSBridgeLib {
		namespace geometry_msgs {

			public class PoseMsg : ROSBridgeMsg {
				private PointMsg position;
				private QuaternionMsg orientation;

				public PoseMsg(JSONNode msg) {
					position = new PointMsg(msg["position"]);
					orientation = new QuaternionMsg(msg["orientation"]);
				}

				public PoseMsg(PointMsg _translation, QuaternionMsg _rotation) {
					position = _translation;
					orientation = _rotation;
				}

				public PoseMsg(Transform _tf) {
					this.position = new PointMsg(_tf.position, true);
					this.orientation = new QuaternionMsg(_tf.localRotation, true);
				}

				public PoseMsg(Vector3 _position, Quaternion _orientation, bool _fromUnity = false) {
					position = new PointMsg(_position, _fromUnity);
					orientation = new QuaternionMsg(_orientation, _fromUnity);
									
				}

				public static string GetMessageType() {
					return "geometry_msgs/Pose";
				}

				public PointMsg GetTranslation() {
					return position;
				}


				public QuaternionMsg GetRotation() {
					return orientation;
				}

				public Vector3 GetPositionUnity() {
					Vector3 p = position.GetPoint();
					return new Vector3(p.x, p.z, p.y);
				}

				public Quaternion GetRotationUnity() {
					return orientation.GetQuaternionUnity();
				}

				public override string ToString() {
					return "Pose [position=" + position.ToString() + ",  orientation=" + orientation.ToString() + "]";
				}

				public override string ToYAMLString() {
					return "{\"position\" : " + position.ToYAMLString() + ", \"orientation\" : " + orientation.ToYAMLString() + "}";
				}
			}
		}
	}
}