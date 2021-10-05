using UnityEngine;

namespace ROSUnityCore.Utils {
    public class TfNode : MonoBehaviour {

        private void Start() {
            TFSystem.instance.RegisterNode(this.transform);        
        }
    }
}