using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using UnityEngine;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace sensor_msgs {

            public class ImageMsg : ROSBridgeMsg {

                private HeaderMsg _header;
                private uint _height;
                private uint _width;
                private string _encoding;
                private byte _is_bigendian;
                private uint _step;
                private byte[] _data;

                WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();


                public ImageMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _height = uint.Parse(msg["height"]);
                    _width = uint.Parse(msg["width"]);
                    _encoding = msg["encoding"];
                    _is_bigendian = byte.Parse(msg["is_bigendian"]);
                    _step = uint.Parse(msg["step"]);
                    _data = new byte[msg["data"].Count];
                    for (int i = 0; i < _data.Length; i++) {
                        _data[i] = byte.Parse(msg["data"][i]);
                    }
                }

                public ImageMsg(HeaderMsg header, uint height, uint width, string encoding, byte is_bigendian, uint step, byte[] data) {
                    _header = header;
                    _height = height;
                    _width = width;
                    _encoding = encoding;
                    _is_bigendian = is_bigendian;
                    _step = step;
                    _data = data;
                }

                public static string GetMessageType() {
                    return "sensor_msgs/Image";
                }

                public override string ToString() {
                    string data = ", data=[";
                    for (int i = 0; i < _data.Length; i++) {
                        data += _data[i].ToString();
                        if (i < (_data.Length - 1))
                            data += ",";
                    }
                    return "Image [header=" + _header.ToString() + ", height=" + _height + ", width=" + _width + ", encoding=" + _encoding + ", is_bigendian=" + _is_bigendian + ", step=" + _step + data + "]]";
                }

                public override string ToYAMLString() {
                    string data = ",  \"data\" : [";
                    for (int i = 0; i < _data.Length; i++) {
                        data += _data[i];
                        if (i < (_data.Length - 1))
                            data += ",";
                    }
                    return "{\"header\" : " + _header.ToYAMLString() + ", \"height\" : " + _height + ", \"width\" : " + _width + ", \"encoding\" : \"" + _encoding + "\", \"is_bigendian\" : " + _is_bigendian + ", \"step\" : " + _step + data + "]}";
                }
            }
        }
    }
}