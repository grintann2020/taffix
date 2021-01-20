using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSetSo", menuName = "ScriptableObject/ColorSetSo", order = 1)]
    public class ColorSetSo : ScriptableObject
    {
        public EColorSet EColorSet = EColorSet.None;
        public ColorSo[] ColorSoArray;
    }
}