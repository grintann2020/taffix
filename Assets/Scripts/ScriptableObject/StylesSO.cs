using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StylesSO", menuName = "ScriptableObject/StylesSO", order = 1)]
    public class StylesSO : ScriptableObject, IData {
        public EData EData {
            get { return _eData; }
        }
        private EData _eData = EData.Style;
        [SerializeField] private StyleSchemeSO[] _schemeArr;
        private Dictionary<EStyleScheme, StyleSchemeSO> _schemeDict = new Dictionary<EStyleScheme, StyleSchemeSO>();
        private Dictionary<EStyleSet, StyleSetSO> _setDict = new Dictionary<EStyleSet, StyleSetSO>();
        private Dictionary<EStyleSample, StyleSampleSO> _sampleDict = new Dictionary<EStyleSample, StyleSampleSO>();
        
        public void Init() {
            for (int i = 0; i < _schemeArr.Length; i++) {
                _schemeDict.Add(_schemeArr[i].EStyleScheme, _schemeArr[i]);
                for (int j = 0; j < _schemeArr[i].SetArr.Length; j++) {
                    _setDict.Add(_schemeArr[i].SetArr[j].EStyleSet, _schemeArr[i].SetArr[j]);
                    for (int k = 0; k < _schemeArr[i].SetArr[j].SampleArr.Length; k++) {
                        _sampleDict.Add(_schemeArr[i].SetArr[j].SampleArr[k].EStyleSample, _schemeArr[i].SetArr[j].SampleArr[k]);
                    }
                }
            }
        }

        public StyleSchemeSO GetScheme(EStyleScheme eScheme) {
            return _schemeDict[eScheme];
        }

        public StyleSetSO GetSet(EStyleSet eSet) {
            return _setDict[eSet];
        }

        public StyleSampleSO GetSample(EStyleSample eSample) {
            return _sampleDict[eSample];
        }
    }
}