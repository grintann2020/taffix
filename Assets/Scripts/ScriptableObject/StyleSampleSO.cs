using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "StyleSO", menuName = "ScriptableObject/StyleSO", order = 1)]
    public class StyleSampleSO : ScriptableObject {
        public EStyleScheme EStyleScheme;
        public EStyleSet EStyleSet;
        public EStyleSample EStyleSample;
        public Color Color;
        public byte Level = 0;

        // public byte R, G, B = 0;
        // public SRGB SRGB {
        //     get {
        //         return new SRGB(R, G, B);
        //     }
        //     set {
        //         R = value.R;
        //         G = value.G;
        //         B = value.B;
        //     }
        // }
    }
}