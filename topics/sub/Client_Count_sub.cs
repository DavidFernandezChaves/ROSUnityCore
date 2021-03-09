using ROSUnityCore.ROSBridgeLib;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using SimpleJSON;
using UnityEngine;

namespace ROSUnityCore {

    public class Client_Count_sub : ROSBridgeSubscriber {

        public new static string GetMessageTopic() {
            return "/client_count";
        }

        public new static string GetMessageType() {
            return "std_msgs/Int32";
        }

        public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
            return new Int32Msg(msg);
        }

        public new static void CallBack(ROSBridgeMsg msg, string ip) {
            GameObject.Find(ip).GetComponent<ROS>().UnSubcribe(typeof(Client_Count_sub));
            Debug.Log(ip + " connected.");
        }
    }
}