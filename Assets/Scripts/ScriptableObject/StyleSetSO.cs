using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StyleSetSO", menuName = "ScriptableObject/StyleSetSO", order = 1)]
    public class StyleSetSO : ScriptableObject {
        public EStyleSet EStyleSet;
        public StyleSampleSO[] SampleArr;
    }
}