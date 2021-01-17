using System.Collections.Generic;
using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorSchemeSO", menuName = "ScriptableObject/ColorSchemeSO", order = 1)]
    public class ColorSchemeSO : ScriptableObject
    {
        public ColorSO[] colors;
        public Dictionary<EColor, ColorSO> colorDictionary = new Dictionary<EColor, ColorSO>();

        public void Init()
        {
            

            for (int c = 0; c < colors.Length; c++)
            {
                colorDictionary.Add(colors[c].eColor, colors[c]);
            }
        }
    }
}