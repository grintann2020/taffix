using System;
using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "DataSetSO", menuName = "ScriptableObject/DataSetSO", order = 1)]
    public class DataSetSO : ScriptableObject {
        public ScriptableObject[] DataSOArr;
        private Dictionary<EData, IData> _dataDict = new Dictionary<EData, IData>();

        public void Init() {
            for (int i = 0; i < DataSOArr.Length; i++) {
                ((IData)DataSOArr[i]).Init();
                _dataDict.Add(((IData)DataSOArr[i]).EData, ((IData)DataSOArr[i]));
            }
        }

        public IData GetData(EData eData) {
            return _dataDict[eData];
        }
    }
}