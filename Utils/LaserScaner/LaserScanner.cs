using ROSUnityCore;
using ROSUnityCore.ROSBridgeLib.sensor_msgs;
using ROSUnityCore.ROSBridgeLib.std_msgs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LaserScanner : MonoBehaviour {

    public int verbose;    
    public int samples = 314;
    public float updateRate = 1f;
    public double angle_min = 0;
    public double angle_max = 6.28f;
    public double angle_increment = 0.02f;
    public double time_increment = 0;
    public double scan_time = 0;
    public double range_min = 0.12f;
    public double range_max = 12.0f;
    public double[] ranges;
    public double[] intensities;

    private ROS ros;
    private Ray[] rays;
    private RaycastHit[] raycastHits;
    
    
    
    public void Start() {
        ranges = new double[samples];
        rays = new Ray[samples];
        raycastHits = new RaycastHit[samples];

        if (ros == null) {
            ros = transform.root.GetComponentInChildren<ROS>();
        }
        if (ros != null) {
            ros.RegisterPublishPackage("CameraRGB_pub");
            StartCoroutine("PublishMeasure");
        }
        
    }


    #region Private Functions
    public IEnumerator PublishMeasure() {
        while (Application.isPlaying) {
            if (ros.IsConnected()) {
                MeasureDistance();
                HeaderMsg _head = new HeaderMsg(0, new TimeMsg(ros.epochStart.Second, 0), transform.name);
                LaserScanMsg scan = new LaserScanMsg(_head, angle_min, angle_max, angle_increment, time_increment, scan_time, range_min, range_max, ranges, intensities);
                ros.Publish(LaserScan_pub.GetMessageTopic(), scan);

            }
            yield return new WaitForSeconds(updateRate);
        }
    }

    private void MeasureDistance() {
        rays = new Ray[samples];
        raycastHits = new RaycastHit[samples];
        ranges = new double[samples];

        for (int i = 0; i < samples; i++) {
            rays[i] = new Ray(transform.position, GetRayRotation(i) * transform.forward);

            raycastHits[i] = new RaycastHit();
            if (Physics.Raycast(rays[i], out raycastHits[i], (float)range_max)) {
                if (raycastHits[i].distance >= range_min && raycastHits[i].distance <= range_max) { ranges[i] = raycastHits[i].distance; }
            } else {
                ranges[i] = 0;
            }
        }
    }


    private Quaternion GetRayRotation(int sample) {
        double eulerAngleInRadians = angle_max - (angle_increment * sample);
        double eulerAngleInDegrees = eulerAngleInRadians * 180 / Mathf.PI;

        return Quaternion.Euler(new Vector3(0, (float)eulerAngleInDegrees+90, 0));
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