using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "HexagonSO", menuName = "ScriptableObject/HexagonSO", order = 2)]
    public class HexagonSO : ScriptableObject {
        public Mesh Mesh;
        public Material Material;
        public byte layer = 0;
    }
}