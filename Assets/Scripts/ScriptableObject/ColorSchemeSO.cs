using System.Collections.Generic;
using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSchemeSo", menuName = "ScriptableObject/ColorSchemeSo", order = 1)]
    public class ColorSchemeSo : ScriptableObject
    {
        public EColorScheme EColorScheme;
        public ColorSetSo[] colorSetSoArray;
    }
}