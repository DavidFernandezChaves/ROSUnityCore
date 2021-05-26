using UnityEngine;
using ROSUnityCore.ROSBridgeLib.tf2_msgs;
using ROSUnityCore.ROSBridgeLib.geometry_msgs;
using System.Collections;
using System.Collections.Generic;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System;

namespace ROSUnityCore {

    public class TFSystem : MonoBehaviour {

        
        public static TFSystem instance;
        public int verbose;
        public List<ROS> clients { get; private set; }
        
        public float updateRate = 1f;

        private List<Transform> tfNodes;
        private char[] specialChar = { '/', ' ' };

        #region Unity Functions
        private void Awake() {
            if (!instance) {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            tfNodes = new List<Transform>();
            clients = new List<ROS>();
            StartCoroutine("SendTfs");
        }

        public void Connected(ROS ros) {
            clients.Add(ros);
            ros.RegisterSubPackage("Tf_sub");
            ros.RegisterPubPackage("Tf_pub");
        }
            #endregion

            #region Public Functions

            public void NewTf(TFMsg _msg, string _ip) {
            TransformStampedMsg[] msgs = _msg.Gettransforms();
            foreach (TransformStampedMsg tf in msgs) {

                string nameParent = tf.Getheader().GetFrameId().Trim(specialChar);
                string nameTf = tf.GetChild_frame_id().Trim(specialChar);

                if (nameTf != null) {
                    GameObject go = GameObject.Find(nameTf);
                    if (go == null) {
                        CreateTf(nameTf, nameParent, tf.Gettransform());
                    } else {
                        if (go.transform.parent == null) {
                            GameObject parent = GameObject.Find(nameParent);
                            if (parent == null) {
                                go.transform.parent = CreateTf(nameParent);
                            } else {
                                go.transform.parent = parent.transform;
                            }
                        }

                        go.transform.localPosition = tf.Gettransform().GetTranslation().GetVector3Unity();
                        go.transform.localRotation = tf.Gettransform().GetRotation().GetQuaternionUnity();
   
                    }
                }
            }
        }

        Transform CreateTf(string name) {
            Transform newTf = new GameObject().transform;
            newTf.name = name;
            return newTf;
        }

        Transform CreateTf(string name, string nameParent, TransformMsg msg) {
            Transform newTf = new GameObject().transform;

            if (nameParent != "") {
                GameObject parent = GameObject.Find(nameParent);
                if (parent == null) {
                    newTf.parent = CreateTf(nameParent, "", Matrix4x4.identity);
                } else {
                    newTf.parent = parent.transform;
                }
            }

            newTf.name = name;
            newTf.position = msg.GetTranslation().GetVector3Unity();
            newTf.rotation = msg.GetRotation().GetQuaternionUnity();

            return newTf;
        }


        Transform CreateTf(string name, string nameParent, Matrix4x4 m) {
            Transform newTf = new GameObject().transform;

            if (nameParent != "") {
                GameObject parent = GameObject.Find(nameParent);
                if (parent == null) {
                    newTf.parent = CreateTf(nameParent, "", Matrix4x4.identity);
                } else {
                    newTf.parent = parent.transform;
                }
            }

            newTf.name = name;
            newTf.FromMatrix(m);

            return newTf;
        }

        public bool RegisterNode(Transform tf) {
            tfNodes.Add(tf);
            Log(tf.name + "Registered.");
            return true;
        }

        //public void CheckClients() {
        //    clients = new List<ROS>(FindObjectsOfType<ROS>());

        //    foreach (ROS ros in clients) {
        //        ros.RegisterSubPackage("Tf_sub");
        //        ros.RegisterPubPackage("Tf_pub");
        //    }
        //}
        #endregion


        #region Private Functions
        IEnumerator SendTfs() {
            while (Application.isPlaying) {
                foreach (ROS ros in clients) {
                    List<TransformStampedMsg> _transforms = new List<TransformStampedMsg>();
                    foreach (Transform tf in tfNodes) {
                        if (tf != null) {
                            string parent_name = "map";
                            if(tf.parent != null) {
                                parent_name = tf.parent.name;
                            }
                            _transforms.Add(new TransformStampedMsg(new HeaderMsg(0, new TimeMsg(DateTime.Now.Second, 0),
                                                                    parent_name),
                                                                    tf.name, new TransformMsg(tf)));
                        } else {
                            tfNodes.Remove(tf);
                        }

                    }

                    TFMsg msg = new TFMsg(_transforms.ToArray());
                    ros.Publish(Tf_pub.GetMessageTopic(), msg);
                }
                Log("Tfs updated.");
                yield return new WaitForSeconds(updateRate);
            }
        }

        private void Log(string _msg) {
            if (verbose > 1)
                Debug.Log("[TFController]: " + _msg);
        }

        private void LogWarning(string _msg) {
            if (verbose > 0)
                Debug.LogWarning("[TFController]: " + _msg);
        }

        #endregion
    }
}