using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "HexagonSampleSO", menuName = "ScriptableObject/HexagonSampleSO", order = 2)]
    public class HexagonSampleSO : ScriptableObject {
        public Mesh Mesh;
        public Material Material;
        public EHexagonLayer EHexlayer;
        public EHexagonSample EHexSample;
    }
}