using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StyleSetSO", menuName = "ScriptableObject/StyleSetSO", order = 1)]
    public class StyleSetSO : ScriptableObject {
        public EStyleSet EStyleSet;
        public StyleSampleSO[] SampleArr {
            get { return _sampleArr; }
        }
        [SerializeField] private StyleSampleSO[] _sampleArr;
    }
}