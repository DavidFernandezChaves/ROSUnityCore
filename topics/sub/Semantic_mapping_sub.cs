using ROSUnityCore.ROSBridgeLib;
using UnityEngine;
using SimpleJSON;
using ROSUnityCore.ROSBridgeLib.semantic_mapping;

namespace ROSUnityCore {
    public class Semantic_mapping_sub : ROSBridgeSubscriber {

        public new static string GetMessageTopic() {
            return "/vimantic/SemanticObjects";
        }

        public new static string GetMessageType() {
            return "vimantic/SemanticObjects";
        }

        public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
            return new SemanticObjectsMsg(msg);
        }

        public new static void CallBack(ROSBridgeMsg msg, string host) {
            Object.FindObjectOfType<ObjectManager>().DetectedObject((SemanticObjectsMsg)msg, host);
        }
    }
}