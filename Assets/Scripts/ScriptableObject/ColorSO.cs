using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "ColorSO", menuName = "ScriptableObject/ColorSO", order = 1)]
    public class ColorSO : ScriptableObject
    {
        public EColor eColor = EColor.Clear;
        public Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        public int level = 0;
    }
}