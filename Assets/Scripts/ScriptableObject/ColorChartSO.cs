using System.Collections.Generic;
using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorChartSO", menuName = "ScriptableObject/ColorChartSO", order = 1)]
    public class ColorChartSo : ScriptableObject, IData
    {
        public ColorSchemeSo[] Schemes;
        // public Dictionary<EColorSet, ColorSetSO> colorSetSODictionary = new Dictionary<EColorSet, ColorSetSO>();
        public Dictionary<EColorSet, ColorSetSo> ColorSetSoDictionary = new Dictionary<EColorSet, ColorSetSo>();
        public Dictionary<EColor, ColorSo> ColorSoDictionary = new Dictionary<EColor, ColorSo>();
        
        
        public void Initialize()
        {
            

            // for (int c = 0; c < colors.Length; c++)
            // {
            //     colorDictionary.Add(colors[c].eColor, colors[c]);
            // }
        }
    }
}