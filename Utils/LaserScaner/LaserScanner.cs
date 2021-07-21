using ROSUnityCore;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LaserScanner : MonoBehaviour {

    public int verbose;

    public float ScanFrecuency = 0.5f;

    public bool sendScanToROS;
    public float ROSFrecuency = 1f;    

    public double angle_min = 0;
    public double angle_max = 360f;
    public double angle_increment = 0.5f;

    public double range_min = 0.12f;
    public double range_max = 12.0f;

    public double[] ranges { get; private set; }

    private int layerMask;

    #region Unity Functions
    private void Start() {
        // This would cast rays only against colliders in layer N.
        // But instead we want to collide against everything except layer N. The ~ operator does this, it inverts a bitmask.
        layerMask = ~((1 << 1) | 1 << 2 | 1 << 10);
        int samples = (int)((angle_max - angle_min) / angle_increment);
        ranges = new double[samples];
        StartCoroutine(Scan());
    }
    #endregion

    #region Public Functions
    public void Connected(ROS ros) {
        if (sendScanToROS) {
            ros.RegisterPubPackage("LaserScan_pub");
            StartCoroutine(SendLaser(ros));
        }
    }
    #endregion

    #region Private Functions
    private IEnumerator SendLaser(ROS ros) {
        Log("Sending laser to ros.");
        while (ros.IsConnected()) {
            yield return new WaitForEndOfFrame();
            HeaderMsg _head = new HeaderMsg(0, new TimeMsg(DateTime.Now.Second, 0), transform.name);
            LaserScanMsg scan = new LaserScanMsg(_head, angle_min*Mathf.Deg2Rad, angle_max * Mathf.Deg2Rad, angle_increment * Mathf.Deg2Rad, 0, 0, range_min, range_max, ranges, new double[0]);
            ros.Publish(LaserScan_pub.GetMessageTopic(), scan);
            yield return new WaitForSeconds(ROSFrecuency);
        }
    }

    private IEnumerator Scan() {
        while (Application.isPlaying) {
            Ray ray;
            for (double i = angle_min; i < angle_max; i += angle_increment) {
                ray = new Ray(transform.position, Quaternion.Euler(0, 90 + (float)-i, 0) * transform.forward);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, (float)range_max, layerMask)) {

                    if (raycastHit.distance >= range_min && raycastHit.distance <= range_max) {
                        ranges[(int)(i / angle_increment)] = raycastHit.distance;
                    }
                }
            }
            yield return new WaitForSeconds(ScanFrecuency);
        }
    }

    private void Log(string _msg) {
        if (verbose > 1)
            Debug.Log("[Laser Scanner]: " + _msg);
    }

    private void LogWarning(string _msg) {
        if (verbose > 0)
            Debug.LogWarning("[Laser Scanner]: " + _msg);
    }
    #endregion
}