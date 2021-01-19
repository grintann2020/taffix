using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSO", menuName = "ScriptableObject/ColorSO", order = 1)]
    public class ColorSO : ScriptableObject
    {   
        public EColorSet eColorSet = EColorSet.Clear;
        public EColor eColor = EColor.Clear;
        public Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        public byte r, g, b = 0;
        public SRGB SRGB
        {
            get
            {
                return new SRGB(r, g, b);
            }
            set
            {
                this.r = value.r;
                this.g = value.g;
                this.b = value.b;
            }
        }
        public byte level = 0;
    }
}