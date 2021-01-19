using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSetSO", menuName = "ScriptableObject/ColorSetSO", order = 1)]
    public class ColorSetSO : ScriptableObject
    {
        public EColorSet eColorSet = EColorSet.None;
        public ColorSO[] colorSOs;
    }
}