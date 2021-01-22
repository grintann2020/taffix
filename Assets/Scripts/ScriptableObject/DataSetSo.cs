using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "DataSetSO", menuName = "ScriptableObject/DataSetSO", order = 1)]
    public class DataSetSO : ScriptableObject
    {
        public ScriptableObject[] ScriptableObjectArray;
        public void Initialize()
        {
            ((IData)ScriptableObjectArray[2]).Initialize();

        }
    }
}