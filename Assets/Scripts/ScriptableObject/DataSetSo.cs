using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "DataSetSo", menuName = "ScriptableObject/DataSetSo", order = 1)]
    public class DataSetSo : ScriptableObject
    {       
        public IData[] IDataArray;
        public void Initialize()
        {

        }
    }
}