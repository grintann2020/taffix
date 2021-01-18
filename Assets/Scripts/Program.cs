using UnityEngine;

namespace T
{
    public class Program : MonoBehaviour
    {
        public ColorSO colorSO = null;
        public ColorSchemeSO[] colorSchemes;

        public Mesh[] hexMeshs = null;
        public Material[] hexMaterials = null;

        public ESize size;

        private Data data = new Data();
        private Control control = new Control();
        private Space space = new Space();
        private ECS eCS = new ECS();
        private HexagonCalculator hexCalculator = new HexagonCalculator();

        void Start()
        {
            // colorSO = new ColorSO();
            Debug.Log("rgb -- " + colorSO.r + ", " + colorSO.g + ", " + colorSO.b);
            Debug.Log(colorSO.SRGB.r + ", " + colorSO.SRGB.g + ", " + colorSO.SRGB.b);
            Init();
        }

        void Update()
        {

        }

        public void Init()
        {
            colorSchemes[0].Init();

            // Debug.Log(colorSchemes[0].colorDictionary[EColor.Red].levels[0].linear);

            this.data.Init();
            this.eCS.Init();
            this.eCS.Create(EEntities.Hexagon_A_01, EArchetype.Static, this.hexMeshs, this.hexMaterials);
            this.space.Init();
            this.space.Bind(this.eCS, this.hexCalculator);

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

            space.Construct(testArr, size);
            Excute();
        }

        public void Excute()
        {
            space.Instantiate();
        }
    }
}