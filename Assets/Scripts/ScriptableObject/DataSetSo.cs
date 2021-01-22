using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "DataSetSO", menuName = "ScriptableObject/DataSetSO", order = 1)]
    public class DataSetSO : ScriptableObject {
        public ScriptableObject[] ScriptableObjectArray;

        public void Initialize() {
            for (int i = 0; i < ScriptableObjectArray.Length; i++) {
                ((IData)ScriptableObjectArray[i]).Initialize();
            }
        }
    }
}