using UnityEngine;
using ROSUnityCore.ROSBridgeLib;
using System;
using System.Collections.Generic;
using System.Collections;

namespace ROSUnityCore {

    public class ROS : MonoBehaviour {

        public static string pathClass = "ROSUnityCore.";

        public int verbose;
        public string ip = "ws://localhost";
        public int pot = 9090;
        public bool viewfinder = false;
        public bool autoConnect = false;
        public List<string> subPackages;
        public List<string> pubPackages;
        public ROSBridgeWebSocketConnection ros { get; private set; }

        #region Unity Functions
        private void Start() {
            if (autoConnect) {
                Connect();
            }
        }

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

        public void SetIP(string _ip) {
            this.ip = "ws://" + _ip;
        }

        public void Disconnect() {
            if (ros != null) {
                ros.Disconnect();
            }
        }

        public void Connect() {
            ros = new ROSBridgeWebSocketConnection(ip, pot);
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

        public void Subcribe(Type _package, int frecuency) {
            ros.AddSubscriberOnline(_package, frecuency);
        }

        public void UnSubcribe(Type _package) {
            ros.UnSubcribe(_package);
        }

        public void RegisterPublishPackage(string _package) {
            if (!pubPackages.Contains(_package)) {
                if (IsConnected()) {
                    try {
                        ros.AddPublisherOnline(Type.GetType(pathClass + _package));
                    } catch {
                        LogWarning(pathClass + _package + " could not be registered.");
                    }
                } else {
                    pubPackages.Add(_package);
                }
            }

        }

        public void Publish(String _topic, ROSBridgeMsg _msg) {
            if (!viewfinder)
                ros.Publish(_topic, _msg);
        }

        public bool IsConnected() {
            return (ros._connected);
        }
         
        #endregion

        #region Private Functions
        private IEnumerator InitialPackage() {
            yield return new WaitForEndOfFrame();

            foreach (string _package in subPackages) {
                try {                    
                    ros.AddSubscriberOnline(Type.GetType(pathClass+_package));
                    Log(_package + " has been registered as a subcriber.");
                } catch {
                    LogWarning(_package + " could not be registered.");
                }
            }

            foreach (string _package in pubPackages) {
                try {
                    ros.AddPublisherOnline(Type.GetType(pathClass + _package));
                    Log(_package + " has been registered as a publisher.");
                } catch {
                    LogWarning(_package + " could not be registered.");
                }
            }

            while (!ros._connected) {
                yield return new WaitForEndOfFrame();
            }
            Log("Connected");
            gameObject.SendMessage("Connected", SendMessageOptions.DontRequireReceiver);

        }



        private void Log(string _msg) {
            if (verbose > 1)
                Debug.Log("[ROS-" + ip + "]: " + _msg);
        }

        private void LogWarning(string _msg) {
            if (verbose > 0)
                Debug.LogWarning("[ROS-" + ip + "]: " + _msg);
        }
        #endregion
    }

}
    