# ROSUnity Core
This is a library to connect [Unity](https://unity.com/) to [ROS](https://www.ros.org/) by websocket protocol.

## Requirements
- To be able to connect the two systems, it is necessary to have installed [RosBridge_suit](http://wiki.ros.org/rosbridge_suite).
- Unity 5.0 or higher.

## Installation
Import the package into your Unity project.

## Method of use
1. Instantiate a Gameobject with the "ROS" script attached.
2. Configure it by adding the topics you want to subscribe to and the ones you want to publish. (It is also possible to do this during run time)
3. call the function "Connect"

## Custom setting
- ROS topic data is received from the "Callback" functions of the classes that implement "ROSBridgeSubscriber" so you can create your own class to access the data you need.
- In the same way, to publish in a topic it is necessary to create a class that implements "ROSBridgePublisher".
- In the package you can find some examples to start from in the "topic" folder.

This work is an expanded and modified version of [ROSBridgeLib](https://github.com/MathiasCiarlo/ROSBridgeLib).
