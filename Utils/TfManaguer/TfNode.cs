using UnityEngine;

namespace ROSUnityCore {
    public class TfNode : MonoBehaviour {

        private void Start() {
            TFSystem.instance.RegisterNode(transform);        
        }
    }
}