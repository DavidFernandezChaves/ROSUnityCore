using SimpleJSON;

namespace ROSUnityCore {
	namespace ROSBridgeLib {
		namespace geometry_msgs {

			public class TwistWithCovarianceMsg : ROSBridgeMsg {
				private TwistMsg _twist;
				private double[] _covariance;

				public TwistWithCovarianceMsg(JSONNode msg) {
					_twist = new TwistMsg(msg["twist"]);
					_covariance = new double[msg["covariance"].Count];
					for (int i = 0; i < _covariance.Length; i++) {
						_covariance[i] = double.Parse(msg["covariance"][i], System.Globalization.CultureInfo.InvariantCulture);
					}
				}

				public TwistWithCovarianceMsg(TwistMsg twist, double[] covariance) {
					_twist = twist;
					
					if(covariance.Length == 36) {
						_covariance = covariance;
					} else {
						_covariance = new double[36];
                    }	
				}

				public static string GetMessageType() {
					return "geometry_msgs/TwistWithCovariance";
				}

				public TwistMsg GetTwist() {
					return _twist;
				}

				public double[] GetCovariance() {
					return _covariance;
				}

				public override string ToString() {
					string array = "[";
					for (int i = 0; i < _covariance.Length; i++) {
						array = array + _covariance[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < _covariance.Length - 1)
							array += ",";
					}
					array += "]";
					return "TwistWithCovariance [twist=" + _twist.ToString() + ", covariance=" + array + "]";
				}

				public override string ToYAMLString() {
					string array = "[";
					for (int i = 0; i < _covariance.Length; i++) {
						array = array + _covariance[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < _covariance.Length - 1)
							array += ",";
					}
					array += "]";
					
					return "{\"twist\" : " + _twist.ToYAMLString() + ", \"covariance\" : " + array + "}";
				}
			}
		}
	}
}