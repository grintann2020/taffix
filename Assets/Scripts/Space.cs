using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
// using Unity.PhysicsCollider;

namespace T {
    public class Space {
        // public const float HEX_RADIAN = 1.0472f;
        private ECS _eCS = null;
        private HexagonCalculator _hexCalculator = null;
        public Hexagon[,] HexArr = null;

        public void Init() {

        }

        public void Bind(ECS eCS, HexagonCalculator hexCalculator) {
            _eCS = eCS;
            _hexCalculator = hexCalculator;
        }

        // public void Create(int colsLength, int rowsLength, Size size)
        public void Construct(int[,] hexData, ESize size) {
            int rowsLength = hexData.GetLength(0);
            int colsLength = hexData.GetLength(1);
            HexArr = new Hexagon[rowsLength, colsLength];
            // float size = 1.0f;
            // float unitWidth = (HEXSCALE * size) / 2;
            // float unitHeight = (2 * size) / 4;
            // float rowSpacing = unitWidth * 2;
            // float colSpacing = unitHeight * 3;
            (float colSpacing, float rowSpacing) = _hexCalculator.DistributionDistance((float)size);
            Coord startingPosition = _hexCalculator.CenterPosition(colsLength, rowsLength, colSpacing, rowSpacing);
            for (int row = 0; row < rowsLength; row++) {
                for (int col = 0; col < colsLength; col++) {
                    if (hexData[row, col] == -1) {
                        HexArr[row, col] = null;
                    } else {
                        float x = startingPosition.X + col * colSpacing;
                        float y = startingPosition.Y;// + (float)hexData[row, col];
                        float z = startingPosition.Z - row * rowSpacing;
                        // x += this.hexCalculator.UnitWidth((float)size) * (row & 1);
                        if (row % 2 != 0) {
                            x += _hexCalculator.UnitWidth(_hexCalculator.HexagonWidth((float)size));
                        }
                        HexArr[row, col] = new Hexagon(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rowsLength; row++) {
                for (int col = 0; col < colsLength; col++) {
                    if (HexArr[row, col] == null) {
                        continue;
                    }

                    if (col < colsLength - 1) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.East);
                        HexArr[row, col].East = HexArr[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col < colsLength - 1) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthEast);
                        HexArr[row, col].NorthEast = HexArr[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col > 0) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthWest);
                        HexArr[row, col].NorthWest = HexArr[row + direct[0], col + direct[1]];
                    }

                    if (col > 0) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.West);
                        HexArr[row, col].West = HexArr[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col > 0) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthWest);
                        HexArr[row, col].SouthWest = HexArr[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col < colsLength - 1) {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthEast);
                        HexArr[row, col].SouthEast = HexArr[row + direct[0], col + direct[1]];
                    }
                }
            }
        }

        public void Instantiate() {
            NativeArray<Entity> hexArray = new NativeArray<Entity>(HexArr.Length, Allocator.Temp);

            for (int row = 0; row < HexArr.GetLength(0); row++) {
                for (int col = 0; col < HexArr.GetLength(1); col++) {
                    if (HexArr[row, col] == null) {
                        continue;
                    }
                    int indexOfGrid = (row * HexArr.GetLength(1)) + col;
                    hexArray[indexOfGrid] = _eCS.EntityManager.Instantiate(
                        _eCS.EntityDict[EEntities.Hexagon_A_01][
                            UnityEngine.Random.Range(0, _eCS.EntityDict[EEntities.Hexagon_A_01].Count)
                        ]
                    );
                    _eCS.EntityManager.SetComponentData(hexArray[indexOfGrid], new Translation
                    {
                        Value = new float3(
                            HexArr[row, col].X,
                            HexArr[row, col].Y,
                            HexArr[row, col].Z
                        )
                    });
                }
            }
            hexArray.Dispose();
        }
    }
}