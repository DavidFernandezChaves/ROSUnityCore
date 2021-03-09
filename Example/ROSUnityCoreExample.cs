using ROSUnityCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROSUnityCoreExample : MonoBehaviour
{

    public GameObject virtualRobotPrefab;
    public string ip;

    // Start is called before the first frame update
    void Start()
    {
        CreateVirtualRobot();
    }

    private void CreateVirtualRobot() {
        Transform virtualRobot = Instantiate(virtualRobotPrefab).transform;
        virtualRobot.GetComponent<ROS>().Connect(ip);
    }
}
