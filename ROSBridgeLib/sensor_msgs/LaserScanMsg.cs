using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System;

namespace ROSUnityCore {
	namespace ROSBridgeLib {
		namespace sensor_msgs {

			public class LaserScanMsg : ROSBridgeMsg {
				private HeaderMsg _header;
				private double _angle_min;
				private double _angle_max;
				private double _angle_increment;
				private double _time_increment;
				private double _scan_time;
				private double _range_min;
				private double _range_max;
				private double[] _ranges;
				private double[] _intensities;


				public LaserScanMsg(JSONNode msg) {
					_header = new HeaderMsg(msg["header"]);
					_angle_min = double.Parse(msg["angle_min"]);
					_angle_max = double.Parse(msg["angle_max"]);
					_angle_increment = double.Parse(msg["angle_increment"]);
					_time_increment = double.Parse(msg["time_increment"]);
					_scan_time = double.Parse(msg["scan_time"]);
					_range_min = double.Parse(msg["range_min"]);
					_range_max = double.Parse(msg["range_max"]);
					string datos = msg["ranges"].ToString();
					_ranges = Array.ConvertAll(datos.Split(','), double.Parse);
					datos = msg["intensities"].ToString();
					_intensities = Array.ConvertAll(datos.Split(','), double.Parse);
				}

				public LaserScanMsg(HeaderMsg header, double angle_min, double angle_max, double angle_increment, double time_increment, double scan_time, double range_min, double range_max, double[] ranges, double[] intensities) {
					_header = header;
					_angle_min = angle_min;
					_angle_max = angle_max;
					_angle_increment = angle_increment;
					_time_increment = time_increment;
					_scan_time = scan_time;
					_range_min = range_min;
					_range_max = range_max;
					_ranges = ranges;
					_intensities = intensities;
				}

				public static string GetMessageType() {
					return "sensor_msgs/LaserScan";
				}

				public override string ToString() {
					string resultado = "LaserScan [header=" + _header.ToString()
									+ ",  angle_min=" + _angle_min.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  angle_max=" + _angle_max.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  angle_increment=" + _angle_increment.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  time_increment=" + _time_increment.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  scan_time=" + _scan_time.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  range_min=" + _range_min.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ",  range_max=" + _range_max.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									   + ",  ranges=[";
					for (int i = 0; i < _ranges.Length; i++) {
						resultado += _ranges[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < (_ranges.Length - 1))
							resultado += ",";
					}
					resultado += "],intensities=[";
					for (int i = 0; i < _intensities.Length; i++) {
						resultado += _ranges[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < (_intensities.Length - 1))
							resultado += ",";
					}
					resultado += "]]";

					return resultado;
				}

				public override string ToYAMLString() {
					string resultado = "{\"header\" : " + _header.ToYAMLString()
									+ ", \"angle_min\" : " + _angle_min.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"angle_max\" : " + _angle_max.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"angle_increment\" : " + _angle_increment.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"time_increment\" : " + _time_increment.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"scan_time\" : " + _scan_time.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"range_min\" : " + _range_min.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									+ ", \"range_max\" : " + _range_max.ToString("G", System.Globalization.CultureInfo.InvariantCulture)
									   + ", \"ranges\" : [";

					for (int i = 0; i < _ranges.Length; i++) {
						resultado += _ranges[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < (_ranges.Length - 1))
							resultado += ",";
					}
					resultado += "], \"intensities\" : [";
					for (int i = 0; i < _intensities.Length; i++) {
						resultado += _intensities[i].ToString("G", System.Globalization.CultureInfo.InvariantCulture);
						if (i < (_intensities.Length - 1))
							resultado += ",";
					}
					resultado += "]}";
					return resultado;
				}
			}
		}
	}
}