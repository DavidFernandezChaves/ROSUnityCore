using UnityEngine;
using ROSUnityCore.ROSBridgeLib;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ROSUnityCore {

    public class ROS : MonoBehaviour {

        public static string pathClass = "ROSUnityCore.";

        public int verbose;
        private ROSBridgeWebSocketConnection ros;

        #region Unity Functions
        void OnApplicationQuit() {
            if (ros != null) {
                ros.Disconnect();
            }
        }

        void Update() {
            if (ros != null)
                ros.Render();
        }
        #endregion

        #region Public Functions
        public void Disconnect() {
            if (ros != null) {
                ros.Disconnect();
            }
        }

        public void Connect(string ip = "ws://localhost", int port = 9090) {
            gameObject.name = "ws://"+ip;
            ros = new ROSBridgeWebSocketConnection("ws://"+ip, port);
            Log("Connecting...");
            ros.AddSubscriber(Type.GetType(pathClass + "Client_Count_sub"));
            if (verbose > 1) {
                ros.SetDebug(true);
            }

            try {
                ros.Connect();
            } catch {
                LogWarning("Fault when connecting to " + ip);
                Destroy(this.gameObject);
            }

            StartCoroutine(InitialPackage());
        }

        public void RegisterSubPackage(string _package, int Throttle_rate = 0) {

            if(Throttle_rate != 0 && !ros._connected) {
                LogWarning("Throttle_rate requires to be connected.");
                return;
            }

            ros.AddSubscriber(Type.GetType(pathClass + _package), Throttle_rate);            
        }

        public void UnSubcribe(Type _package) {
            ros.UnSubcribe(_package);
        }

        public void RegisterPubPackage(string _package) {
            ros.AddPublisher(Type.GetType(pathClass + _package));
        }

        public void Publish(String _topic, ROSBridgeMsg _msg) {
            if(ros._connected)
                ros.Publish(_topic, _msg);
        }

        public bool IsConnected() {
            return ros._connected;
        }
         
        #endregion

        #region Private Functions
        private IEnumerator InitialPackage() {
            int i = 0;
            while (!ros._connected) {
                yield return new WaitForSeconds(0.5f);
                i++;
                if ((i % 11) == 10) {
                    LogWarning("ROS does not respond.");
                }
            }
            Log("Connected");            
            gameObject.SendMessageUpwards("Connected",this, SendMessageOptions.DontRequireReceiver);
            GameObject.FindGameObjectsWithTag("GeneralScripts").ToList().ForEach(G => G.SendMessage("Connected", this, SendMessageOptions.DontRequireReceiver));
        }

        private void Log(string _msg) {
            if (verbose > 1)
                Debug.Log("[ROS " + gameObject.name + "]: " + _msg);
        }

        private void LogWarning(string _msg) {
            if (verbose > 0)
                Debug.LogWarning("[ROS " + gameObject.name + "]: " + _msg);
        }
        #endregion
    }

}
    