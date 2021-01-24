using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "HexagonsSO", menuName = "ScriptableObject/HexagonsSO", order = 2)]
    public class HexagonsSO : ScriptableObject, IData {
        public EData EData {
            get { return _eData; }
        }
        private EData _eData = EData.Hexagon;
        [SerializeField] private HexagonSampleSO[] _sampleArr;
        private Dictionary<EHexagonSample, HexagonSampleSO> _sampleDict = new Dictionary<EHexagonSample, HexagonSampleSO>();

        public void Init() {
            for (int i = 0; i < _sampleArr.Length; i++) {
                _sampleDict.Add(_sampleArr[i].EHexSample, _sampleArr[i]);
            }
        }

        public HexagonSampleSO GetSample(EHexagonSample eSample) {
            return _sampleDict[eSample];
        }
    }
}