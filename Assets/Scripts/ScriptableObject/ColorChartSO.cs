using System.Collections.Generic;
using UnityEngine;

namespace T
{
    [CreateAssetMenu(fileName = "ColorChartSO", menuName = "ScriptableObject/ColorChartSO", order = 1)]
    public class ColorChartSO : ScriptableObject
    {
        public ColorSchemeSO[] schemes;
        public Dictionary<EColor, ColorSO> colorDictionary = new Dictionary<EColor, ColorSO>();

        public void Init()
        {
            

            // for (int c = 0; c < colors.Length; c++)
            // {
            //     colorDictionary.Add(colors[c].eColor, colors[c]);
            // }
        }
    }
}