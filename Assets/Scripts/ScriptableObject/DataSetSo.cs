using System;
using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "DataSetSO", menuName = "ScriptableObject/DataSetSO", order = 1)]
    public class DataSetSO : ScriptableObject {
        public ScriptableObject[] SOArr;
        private Dictionary<EData, IData> _dataDict = new Dictionary<EData, IData>();

        public void Init() {
            for (int i = 0; i < SOArr.Length; i++) {
                ((IData)SOArr[i]).Init();
                _dataDict.Add(((IData)SOArr[i]).EData, ((IData)SOArr[i]));
            }
        }

        public IData GetData(EData eData) {
            return _dataDict[eData];
        }
    }
}