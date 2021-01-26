using UnityEngine;

namespace T {
    [CreateAssetMenu(fileName = "HexItemSO", menuName = "ScriptableObject/HexItemSO", order = 2)]
    public class HexItemSO : ScriptableObject {
        public Mesh Mesh;
        public Material Material;
        public EHexLayer EHexlayer;
        public EHexItem EHexItem;
    }
}