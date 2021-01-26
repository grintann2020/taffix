using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StyleDataSO", menuName = "ScriptableObject/StyleDataSO", order = 1)]
    public class StyleDataSO : ScriptableObject, IData {
        public EData EData {
            get { return _eData; }
        }
        private EData _eData = EData.Style;
        [SerializeField] private StyleSchemeSO[] _schemeArr;
        private Dictionary<EStyleScheme, StyleSchemeSO> _schemeDict = new Dictionary<EStyleScheme, StyleSchemeSO>();
        private Dictionary<EStyleSet, StyleSetSO> _setDict = new Dictionary<EStyleSet, StyleSetSO>();
        private Dictionary<EStyleItem, StyleItemSO> _itemDict = new Dictionary<EStyleItem, StyleItemSO>();
        
        public void Init() {
            for (int i = 0; i < _schemeArr.Length; i++) {
                _schemeDict.Add(_schemeArr[i].EStyleScheme, _schemeArr[i]);
                for (int j = 0; j < _schemeArr[i].SetArr.Length; j++) {
                    _setDict.Add(_schemeArr[i].SetArr[j].EStyleSet, _schemeArr[i].SetArr[j]);
                    for (int k = 0; k < _schemeArr[i].SetArr[j].ItemArr.Length; k++) {
                        _itemDict.Add(_schemeArr[i].SetArr[j].ItemArr[k].EStyleItem, _schemeArr[i].SetArr[j].ItemArr[k]);
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

        public StyleItemSO GetItem(EStyleItem eItem) {
            return _itemDict[eItem];
        }
    }
}