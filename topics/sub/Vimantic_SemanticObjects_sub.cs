using ROSUnityCore.ROSBridgeLib;
using UnityEngine;
using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.ViMantic_msgs;

namespace ROSUnityCore {

    public class Vimantic_SemanticObjects_sub : ROSBridgeSubscriber {

        public new static string GetMessageTopic() {
            return "/ViMantic/SemanticObjects";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticObjectArray";
        }

        public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
            return new SemanticObjectArrayMsg(msg);
        }

        public new static void CallBack(ROSBridgeMsg msg, string ip) {
            Object.FindObjectOfType<ObjectManager>().DetectedObject((SemanticObjectArrayMsg)msg, ip);
        }
    }
}