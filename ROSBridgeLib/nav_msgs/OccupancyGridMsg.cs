using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System.IO;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using UnityEngine;

namespace ROSUnityCore {
    namespace ROSBridgeLib {
        namespace nav_msgs {

            public class OccupancyGridMsg : ROSBridgeMsg {
                private HeaderMsg _header;
                private MapMetaDataMsg _info;
                private sbyte[] _data;

                public OccupancyGridMsg(JSONNode msg) {
                    _header = new HeaderMsg(msg["header"]);
                    _info = new MapMetaDataMsg(msg["info"]);
                    _data = new sbyte[msg["data"].Count];
                    for (int i = 0; i < _data.Length; i++) {
                        _data[i] = sbyte.Parse(msg["data"][i]);
                    }
                }

                public OccupancyGridMsg(HeaderMsg header, MapMetaDataMsg info, sbyte[] data) {
                    _header = header;
                    _info = info;
                    _data = data;
                }

                public OccupancyGridMsg(HeaderMsg header, TextAsset pgmFile,float resolution, Vector3 origin) {
                    _header = header;
                    uint width = 0;
                    uint height = 0;
                    _data = LoadImage(pgmFile, out width, out height);
                    _info = new MapMetaDataMsg(new TimeMsg (0,0), resolution, width, height, new PoseMsg(origin, Quaternion.identity, false));
                    
                }

                public static string GetMessageType() {
                    return "nav_msgs/OccupancyGrid";
                }

                public HeaderMsg GetHeader() {
                    return _header;
                }

                public MapMetaDataMsg GetInfo() {
                    return _info;
                }

                public sbyte[] GetData() {
                    return _data;
                }

                public override string ToString() {
                    string array = "[";
                    for (int i = 0; i < _data.Length; i++) {
                        array = array + _data[i];
                        if (_data.Length - i <= 1)
                            array += ",";
                    }
                    array += "]";
                    return "OccupancyGrid [header=" + _header.ToString() + ",  info=" + _info.ToString() + ",  data=" + _data + "]";
                }

                public override string ToYAMLString() {
                    string array = "[";
                    for (int i = 0; i < _data.Length; i++) {
                        array = array + _data[i];
                        if ( i != _data.Length-1)
                            array += ",";
                    }
                    array += "]";
                    return "{\"header\" : " + _header.ToYAMLString() + ", \"info\" : " + _info.ToYAMLString() + ", \"data\" : " + array + "}";
                }

                public sbyte[] LoadImage(TextAsset asset, out uint width, out uint height) {
                    Stream s = new MemoryStream(asset.bytes);
                    BinaryReader br = new BinaryReader(s);

                    string magic = NextNonCommentLine(br);

                    string widthHeight = NextNonCommentLine(br);
                    string[] tokens = widthHeight.Split(' ');
                    width = uint.Parse(tokens[0]);
                    height = uint.Parse(tokens[1]);

                    string sMaxVal = NextNonCommentLine(br);
                    int maxVal = int.Parse(sMaxVal);

                    // read width * height pixel values . . .
                    sbyte[] pixels = new sbyte[width * height];
                    //for (int i = 0; i < (width * height); ++i) {
                    //    pixels[i] = (sbyte)br.ReadByte();
                    //}

                    br.Close(); s.Close();
                    return pixels;
                }

                static string NextAnyLine(BinaryReader br) {
                    string s = "";
                    byte b = 0; // dummy
                    while (b != 10) // newline
                    {
                        b = br.ReadByte();
                        char c = (char)b;
                        s += c;
                    }
                    return s.Trim();
                }

                static string NextNonCommentLine(BinaryReader br) {
                    string s = NextAnyLine(br);
                    while (s.StartsWith("#") || s == "")
                        s = NextAnyLine(br);
                    return s;
                }
            }
        }
    }
}