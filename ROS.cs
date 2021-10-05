using UnityEngine;
using ROSUnityCore.ROSBridgeLib;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ROSUnityCore.Utils;

namespace ROSUnityCore {

    public class ROS : MonoBehaviour {

        public static string pathClass = "ROSUnityCore.";

        [Header("General")]
        [Tooltip("The log level to use")]
        public LogLevel LogLevel = LogLevel.Normal;

        public bool autoConnect = false;
        public string robotName = "VirtualRobot";

        private ROSBridgeWebSocketConnection ros;
        public string ip;

        #region Unity Functions
        private void Start() {
            if (autoConnect) {
                Connect(ip);
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
            if(autoConnect) {
                Disconnect();
                Connect(ip);
            }
        }
        #endregion

        #region Public Functions
        public void Disconnect() {
            if (ros != null) {
                ros.Disconnect();
                GameObject.FindGameObjectsWithTag("ROSListener").ToList().ForEach(G => G.SendMessage("Disconnected", this, SendMessageOptions.DontRequireReceiver));
                gameObject.BroadcastMessage("Disconnected", this, SendMessageOptions.DontRequireReceiver);
            }
        }

        public void Connect(string ip, int port = 9090) {
            this.ip = ip;
            autoConnect = false;

            ros = new ROSBridgeWebSocketConnection("ws://" + ip, port);
            Log("Connecting...",LogLevel.Developer);
            ros.AddSubscriber(Type.GetType(pathClass + "Client_Count_sub"));
            if (LogLevel == LogLevel.Developer) {
                ros.SetDebug(true);
            }

            try {
                ros.Connect();
            } catch {
                Log("Fault when connecting to " + ip,LogLevel.Error,true);
                Destroy(this.gameObject);
            }

            StartCoroutine(InitialPackage());
        }

        public void RegisterSubPackage(string _package, int Throttle_rate = 0) {

            if(Throttle_rate != 0 && !ros._connected) {
                Log("Throttle_rate requires to be connected.",LogLevel.Error,true);
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
        IEnumerator StartPing() {
            WaitForSeconds f = new WaitForSeconds(0.05f);
            while (ros._connected) {
                Ping p = new Ping(ip);
                while (p.isDone == false) {
                    yield return f;
                }
                Log("Ping: " + p.time.ToString() + "ms",LogLevel.Developer);
            }     
        }

        private IEnumerator InitialPackage() {
            int i = 0;
            while (!ros._connected) {
                yield return new WaitForSeconds(0.5f);
                i++;
                if ((i % 11) == 10) {
                    Log("ROS does not respond.",LogLevel.Error);
                }
            }
            gameObject.name = robotName;
            Debug.Log(ip+" - Connected");            
            GameObject.FindGameObjectsWithTag("ROSListener").ToList().ForEach(G => G.SendMessage("Connected", this, SendMessageOptions.DontRequireReceiver));
            gameObject.BroadcastMessage("Connected", this, SendMessageOptions.DontRequireReceiver);
            
            if (LogLevel == LogLevel.Developer) {
                StartCoroutine(StartPing());
            }
        }

        private void Log(string _msg, LogLevel lvl, bool Warning = false)
        {
            if (LogLevel <= lvl && LogLevel != LogLevel.Nothing)
            {
                if (Warning)
                {
                    Debug.LogWarning("[ROS -" + ip + "]: " + _msg);
                }
                else
                {
                    Debug.Log("[ROS -" + ip + "]: " + _msg);
                }
            }
        }
        #endregion
    }

}
    