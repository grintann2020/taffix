using UnityEngine;

namespace THEX
{
    public class Program : MonoBehaviour
    {
        public Size size;
        public Mesh[] hexMeshs = null;
        public Material[] hexMaterials = null;
        public Mesh[] hexSide1Meshs = null;
        public Material[] hexSide1Materials = null;
        public Mesh[] hexSide2Meshs = null;
        public Material[] hexSide2Materials = null;
        public Mesh[] hexSide3Meshs = null;
        public Material[] hexSide3Materials = null;
        private Space space = new Space();
        private ECS eCS = new ECS();
        private HexCalculator hexCalculator = new HexCalculator();

        void Start()
        {
            Init();
        }

        void Update()
        {

        }

        public void Init()
        {
            eCS.Init();
            eCS.Create(EntityCategory.Hex3, Archetype.Basic, hexMeshs, hexMaterials);
            eCS.Create(EntityCategory.Hex3Side1, Archetype.Rotation, hexSide1Meshs, hexSide1Materials);
            eCS.Create(EntityCategory.Hex3Side2, Archetype.Rotation, hexSide2Meshs, hexSide2Materials);
            eCS.Create(EntityCategory.Hex3Side3, Archetype.Rotation, hexSide3Meshs, hexSide3Materials);
            space.Init();
            space.Bind(eCS, hexCalculator);
            int[,] hArr = new int[10, 14] {
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 0, 0},
                {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0},
                {0, 0, 1, 2, 3, 1, 3, 2, 3, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0},
                {0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0},
                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            };
            space.Construct(hArr, size);
            Excute();
        }

        public void Excute()
        {
            space.Realize();
        }
    }
}