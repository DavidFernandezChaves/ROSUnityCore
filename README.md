# ROSUnity
This is a library to connect [Unity3D](https://unity.com/) to [ROS](https://www.ros.org/) by websocket protocol.

## Requirements
- ROS Kinetic Kame or higher
- The ROS package: [RosBridge_suit](http://wiki.ros.org/rosbridge_suite).
- Unity 5.0 or higher.

## Installation
Import the package into your Unity project.

## Method of use
1. Instantiate a Gameobject with the "ROS" script attached.
2. Configure this script with the IP and port used by rosbridget.
3. Call the function "Connect".

## Custom setting
- ROS topic data is received from the "Callback" functions of the classes that implement "ROSBridgeSubscriber" so you can create your own class to access the data you need.
- In the same way, to publish in a topic it is necessary to create a class that implements "ROSBridgePublisher".
- In the package you can find some examples to start from in the "topic" folder.

## Features
This package adds some utilities to speed up development:
- TfManaguer: A system that synchronizes the ROS Tfs tree with the Hierarchy used in Unity. In order to use it, some gameobject must have the TfManaguer script that takes care of the synchronization. For a gameobject to be taken into account in ROS, it is enough to attach the TFNode script.
- MapConstructor: This system stamps the occupation map used in ROS on a Unity terrain.
- LaserScaner: Simulates in Unity a laser scanner.

This work is an expanded and modified version of [ROSBridgeLib](https://github.com/MathiasCiarlo/ROSBridgeLib).
