using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "DataSetSo", menuName = "ScriptableObject/DataSetSo", order = 1)]
    public class DataSetSo : ScriptableObject
    {
        public ScriptableObject[] scriptableObjectArray;
        public void Initialize()
        {
            // ((ColorChartSo)scriptableObjectArray[2]).

        }
    }
}