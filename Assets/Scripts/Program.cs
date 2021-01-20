using UnityEngine;

namespace T
{
    public class Program : MonoBehaviour
    {
        public DataSetSo DataSetSo = null;
        public Mesh[] hexMeshs = null;
        public Material[] hexMaterials = null;

        public ESize size;

        
        private Control _control = new Control();
        private Space _space = new Space();
        private Ecs _ecs = new Ecs();
        private HexagonCalculator hexCalculator = new HexagonCalculator();

        void Start()
        {
            
            // colorSO = new ColorSO();
            // Debug.Log("rgb -- " + colorSO.r + ", " + colorSO.g + ", " + colorSO.b);
            // Debug.Log(colorSO.SRGB.r + ", " + colorSO.SRGB.g + ", " + colorSO.SRGB.b);
            this.Initialize();
        }

        void Update()
        {

        }

        public void Initialize()
        {
            DataSetSo.Initialize();

            // Debug.Log(colorSchemes[0].colorDictionary[EColor.Red].levels[0].linear);

            _ecs.Initialize();
            _ecs.Create(EEntities.Hexagon_A_01, EArchetype.Static, hexMeshs, hexMaterials);
            _space.Initialize();
            _space.Bind(_ecs, hexCalculator);

            int[,] testArr = new int[8, 8] {
                {3, 0, 3, 0, 3, 0, 3, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1},
                {3, 0, 1, 0, 3, 1, 0, 0},
                {0, 0, 0, 0, 0, 2, 0, 1},
                {3, 0, 1, 2, 3, 1, 3, 2},
                {0, 0, 0, 0, 0, 0, 2, 0},
                {0, 0, 2, 0, 0, 0, 0, 1}
            };

            int[,] hArr1 = new int[11, 13] {
                {3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 0},
                {3, 0, 1, 0, 3, 1, 0, 0, 3, 0, 0, 0, 3},
                {0, 0, 0, 0, 0, 2, 0, 1, 2, 1, 0, 1, 0},
                {3, 0, 1, 2, 3, 1, 3, 2, 3, 0, 0, 0, 3},
                {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0},
                {0, 0, 2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
                {0, 1, 3, 1, 0, 0, 0, 2, 3, 2, 0, 1, 3},
                {0, 0, 2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1},
            };

            int[,] hArr2 = new int[5, 5] {
                {0, 0, 0, 0, 0},
                {0, 1, 2, 0, 0},
                {0, 1, 3, 1, 0},
                {0, 1, 2, 0, 0},
                {0, 0, 0, 0, 0},
            };

            _space.Construct(testArr, size);
            Excute();
        }

        public void Excute()
        {
            _space.Instantiate();
        }
    }
}