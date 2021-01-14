using UnityEngine;

namespace T
{
    public class Program : MonoBehaviour
    {
        public Size size;
        public GameObject TestHex;
        public Mesh[] hexMeshs = null;
        public Material[] hexMaterials = null;

        private Control control = new Control();
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
            this.eCS.Init();
            this.eCS.Create(EntityEnum.Hex1, ArchetypeEnum.Static, this.hexMeshs, this.hexMaterials);
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