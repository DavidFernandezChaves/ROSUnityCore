using ROSUnityCore.ROSBridgeLib;
using UnityEngine;
using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.ViMantic_msgs;
using ViMantic;

namespace ROSUnityCore {

    public class Vimantic_Detections_sub : ROSBridgeSubscriber {

        public new static string GetMessageTopic() {
            return "/ViMantic/Detections";
        }

        public new static string GetMessageType() {
            return "vimantic/DetectionArray";
        }

        public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
            return new DetectionArrayMsg(msg);
        }

        public new static void CallBack(ROSBridgeMsg msg, string ip) {
            Object.FindObjectOfType<VirtualObjectSystem>().DetectedObject((DetectionArrayMsg)msg, ip);
        }
    }
}