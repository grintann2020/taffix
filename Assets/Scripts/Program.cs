using System;
using UnityEngine;

namespace T {
    public class Program : MonoBehaviour {
        public DataSetSO DataSetSO = null;
        public Mesh[] hexMeshs = null;
        public Material[] hexMaterials = null;
        public ESize size;
        private Control _control = new Control();
        private Space _space = new Space();
        private ECS _eCS = new ECS();
        private HexagonCalculator hexCalculator = new HexagonCalculator();

        void Start() {
            double a = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            this.Init();
            double b = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            Debug.Log("Init Done (ms) --> " + (b - a));
        }

        void Update() {

        }

        public void Init() {
            DataSetSO.Init();

            // Debug.Log(colorSchemes[0].colorDictionary[EColor.Red].levels[0].linear);

            _eCS.Init();
            _eCS.Create(EEntities.Hexagon_A_01, EArchetype.Static, hexMeshs, hexMaterials);
            _space.Init();
            _space.Bind(_eCS, hexCalculator);

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

        public void Excute() {

            _space.Instantiate();

        }
    }
}