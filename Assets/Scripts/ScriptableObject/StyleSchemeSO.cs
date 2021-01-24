using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StyleSchemeSO", menuName = "ScriptableObject/StyleSchemeSO", order = 1)]
    public class StyleSchemeSO : ScriptableObject {
        public EStyleScheme EStyleScheme;
        public StyleSetSO[] SetArr;
    }
}