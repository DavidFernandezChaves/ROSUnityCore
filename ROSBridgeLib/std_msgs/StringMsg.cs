using SimpleJSON;

namespace ROSUnityCore {
	namespace ROSBridgeLib {
		namespace std_msgs {

			public class StringMsg : ROSBridgeMsg {
				private string _data;

				public StringMsg(JSONNode msg) {
					//_data = msg["data"];
					_data = msg;
				}

				public StringMsg(string data) {
					_data = data;
				}

				public static string GetMessageType() {
					return "std_msgs/String";
				}

				public string GetData() {
					return _data;
				}

				public override string ToString() {
					return "String [data=" + _data + "]";
				}

				public override string ToYAMLString() {
					return "{\"data\" : " + _data + "}";
				}
			}
		}
	}
}