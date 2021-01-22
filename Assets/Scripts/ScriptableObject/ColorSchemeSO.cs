using System.Collections.Generic;
using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "ColorSchemeSO", menuName = "ScriptableObject/ColorSchemeSO", order = 1)]
    public class ColorSchemeSO : ScriptableObject {
        public EColorScheme EColorScheme;
        public ColorSetSO[] ColorSetSOArray;
    }
}