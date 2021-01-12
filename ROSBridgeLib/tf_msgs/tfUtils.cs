using UnityEngine;
using System;

namespace ROSUnityCore {
    namespace ROSBridgeLib {

        public class TfUtils {

            static public Vector3 EulerFromROSQuaternion(Quaternion q) {
                float t0 = 2 * (q.w * q.x + q.y * q.z);
                float t1 = 1 - 2 * (q.x * q.x + q.y * q.y);
                float rollX = Mathf.Atan2(t0, t1);

                float t2 = 2 * (q.w * q.y - q.z * q.x);
                t2 = Mathf.Clamp(t2, -1, 1);
                float pitchY = Mathf.Asin(t2);

                float t3 = 2 * (q.w * q.z + q.x * q.y);
                float t4 = 1 - 2 * (q.y * q.y + q.z * q.z);
                float yawZ = Mathf.Atan2(t3, t4);

                return new Vector3(rollX * Mathf.Rad2Deg, pitchY * Mathf.Rad2Deg, yawZ * Mathf.Rad2Deg);
            }

            static public Quaternion QuaternionROSFromEuler(float yaw, float pitch, float roll) {
                float halfYaw = yaw * 0.5f;
                float halfPitch = pitch * 0.5f;
                float halfRoll = roll * 0.5f;
                float cosYaw = Mathf.Cos(halfYaw);
                float sinYaw = Mathf.Sin(halfYaw);
                float cosPitch = Mathf.Cos(halfPitch);
                float sinPitch = Mathf.Sin(halfPitch);
                float cosRoll = Mathf.Cos(halfRoll);
                float sinRoll = Mathf.Sin(halfRoll);

                return new Quaternion(cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw,
                                        cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw,
                                        sinRoll * cosPitch * cosYaw - cosRoll * sinPitch * sinYaw,
                                        cosRoll * cosPitch * cosYaw + sinRoll * sinPitch * sinYaw);

            }

            public static Quaternion TransormQuaternionROS2Unity(Quaternion q) {
                return new Quaternion(q.x, -q.z, -q.y, q.w);
            }

            public static Quaternion TransormQuaternionUnity2ROS(Quaternion q) {
                return new Quaternion(q.x,-q.z,-q.y,q.w);
            }

            static public Vector3 GetEulerYPR(Quaternion q, int solution) {
                return GetEulerYPR(GetMatrix3x3Rotation(q), solution);
            }

            static public Vector3 GetEulerYPR(float[] matrix3x3, int solution) {
                Vector3 result1 = new Vector3();
                Vector3 result2 = new Vector3();

                if (matrix3x3[6] >= 1) {
                    result1.x = 0f;
                    result2.x = 0f;

                    if (matrix3x3[6] < 0) {
                        float delta = Mathf.Atan2(matrix3x3[1], matrix3x3[2]);
                        result1.y = Mathf.PI / 2;
                        result2.y = Mathf.PI / 2;
                        result1.z = delta;
                        result2.z = delta;
                    } else {
                        float delta = Mathf.Atan2(-matrix3x3[1], -matrix3x3[2]);
                        result1.y = -Mathf.PI / 2;
                        result2.y = -Mathf.PI / 2;
                        result1.z = delta;
                        result2.z = delta;
                    }
                } else {
                    result1.y = -Mathf.Asin(matrix3x3[6]);
                    result2.y = Mathf.PI - result1.y;

                    result1.z = Mathf.Atan2(matrix3x3[7] / Mathf.Cos(result1.y), matrix3x3[8] / Mathf.Cos(result1.y));
                    result2.z = Mathf.Atan2(matrix3x3[7] / Mathf.Cos(result2.y), matrix3x3[8] / Mathf.Cos(result2.y));

                    result1.x = Mathf.Atan2(matrix3x3[3] / Mathf.Cos(result1.y), matrix3x3[0] / Mathf.Cos(result1.y));
                    result2.x = Mathf.Atan2(matrix3x3[3] / Mathf.Cos(result2.y), matrix3x3[0] / Mathf.Cos(result2.y));
                }

                if (solution == 1)
                    return result1;
                else
                    return result2;
            }

            static public float[] GetMatrix3x3Rotation(Quaternion q) {
                float[] result = new float[9];

                float xs = q.x * 2;
                float ys = q.y * 2;
                float zs = q.z * 2;
                float wx = q.w * xs;
                float wy = q.w * ys;
                float wz = q.w * zs;
                float xx = q.x * xs;
                float xy = q.x * ys;
                float xz = q.x * zs;
                float yy = q.y * ys;
                float yz = q.y * zs;
                float zz = q.z * zs;

                result[0] = 1 - (yy + zz);
                result[1] = xy - wz;
                result[2] = xz + wy;
                result[3] = xy + wz;
                result[4] = 1 - (xx + zz);
                result[5] = yz - wx;
                result[6] = xz - wy;
                result[7] = yz + wx;
                result[8] = 1 - (xx + yy);

                return result;
            }

            static public float Length2(Quaternion q) {
                return q.x * q.x + q.y * q.y + q.z * q.z + q.w + q.w;
            }

        }
    }
}