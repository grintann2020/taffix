using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "HexDataSO", menuName = "ScriptableObject/HexDataSO", order = 2)]
    public class HexDataSO : ScriptableObject, IData {
        public EData EData {
            get { return _eData; }
        }
        private EData _eData = EData.Hex;
        [SerializeField] private HexItemSO[] _itemArr;
        private Dictionary<EHexItem, HexItemSO> _itemDict = new Dictionary<EHexItem, HexItemSO>();

        public void Init() {
            for (int i = 0; i < _itemArr.Length; i++) {
                _itemDict.Add(_itemArr[i].EHexItem, _itemArr[i]);
            }
        }

        public HexItemSO GetItem(EHexItem eItem) {
            return _itemDict[eItem];
        }
    }
}