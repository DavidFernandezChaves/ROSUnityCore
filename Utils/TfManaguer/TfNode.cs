using UnityEngine;

namespace ROSUnityCore {
    public class TfNode : MonoBehaviour {

        private void Start() {
            TFController.instance.RegisterNode(transform);        
        }
    }
}