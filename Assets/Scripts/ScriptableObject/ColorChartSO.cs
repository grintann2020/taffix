using System.Collections.Generic;
using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorChartSO", menuName = "ScriptableObject/ColorChartSO", order = 1)]
    public class ColorChartSO : ScriptableObject, IData
    {
        public ColorSchemeSO[] ColorSchemeArray;
        // public Dictionary<EColorSet, ColorSetSO> colorSetSODictionary = new Dictionary<EColorSet, ColorSetSO>();
        public Dictionary<EColorSet, ColorSetSO> ColorSetSoDictionary = new Dictionary<EColorSet, ColorSetSO>();
        public Dictionary<EColor, ColorSO> ColorSoDictionary = new Dictionary<EColor, ColorSO>();
        
        
        public void Initialize()
        {
            

            // for (int c = 0; c < colors.Length; c++)
            // {
            //     colorDictionary.Add(colors[c].eColor, colors[c]);
            // }
        }
    }
}