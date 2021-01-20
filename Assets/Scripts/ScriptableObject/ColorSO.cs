using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSo", menuName = "ScriptableObject/ColorSo", order = 1)]
    public class ColorSo : ScriptableObject
    {   
        public EColorScheme EColorScheme = EColorScheme.None;
        public EColorSet EColorSet = EColorSet.None;
        public EColor EColor = EColor.None;
        public Color Color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        public byte R, G, B = 0;
        public SRgb SRgb
        {
            get
            {
                return new SRgb(R, G, B);
            }
            set
            {
                this.R = value.R;
                this.G = value.G;
                this.B = value.B;
            }
        }
        public byte level = 0;
    }
}