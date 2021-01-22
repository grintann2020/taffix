using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "ColorSetSO", menuName = "ScriptableObject/ColorSetSO", order = 1)]
    public class ColorSetSO : ScriptableObject {
        public EColorSet EColorSet = EColorSet.None;
        public ColorSO[] ColorSOArray;
    }
}