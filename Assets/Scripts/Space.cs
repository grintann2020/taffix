using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
// using Unity.PhysicsCollider;

namespace T
{
    public class Space
    {
        // public const float HEX_RADIAN = 1.0472f;
        private Ecs _ecs = null;
        private HexagonCalculator _hexCalculator = null;
        public Hexagon[,] HexArray = null;

        public void Initialize()
        {

        }

        public void Bind(Ecs ecs, HexagonCalculator hexCalculator)
        {
            _ecs = ecs;
            _hexCalculator = hexCalculator;
        }

        // public void Create(int colsLength, int rowsLength, Size size)
        public void Construct(int[,] hexData, ESize size)
        {
            int rowsLength = hexData.GetLength(0);
            int colsLength = hexData.GetLength(1);
            HexArray = new Hexagon[rowsLength, colsLength];
            // float size = 1.0f;
            // float unitWidth = (HEXSCALE * size) / 2;
            // float unitHeight = (2 * size) / 4;
            // float rowSpacing = unitWidth * 2;
            // float colSpacing = unitHeight * 3;
            (float colSpacing, float rowSpacing) = _hexCalculator.DistributionDistance((float)size);
            Coord startingPosition = _hexCalculator.CenterPosition(colsLength, rowsLength, colSpacing, rowSpacing);
            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (hexData[row, col] == -1)
                    {
                        HexArray[row, col] = null;
                    }
                    else
                    {
                        float x = startingPosition.X + col * colSpacing;
                        float y = startingPosition.Y;// + (float)hexData[row, col];
                        float z = startingPosition.Z - row * rowSpacing;
                        // x += this.hexCalculator.UnitWidth((float)size) * (row & 1);
                        if (row % 2 != 0)
                        {
                            x += _hexCalculator.UnitWidth((float)size);
                        }
                        HexArray[row, col] = new Hexagon(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (HexArray[row, col] == null)
                    {
                        continue;
                    }

                    if (col < colsLength - 1)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.East);
                        HexArray[row, col].East = HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col < colsLength - 1)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthEast);
                        HexArray[row, col].NorthEast = HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col > 0)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthWest);
                        HexArray[row, col].NorthWest = HexArray[row + direct[0], col + direct[1]];
                    }

                    if (col > 0)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.West);
                        HexArray[row, col].West = HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col > 0)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthWest);
                        HexArray[row, col].SouthWest = HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col < colsLength - 1)
                    {
                        int[] direct = _hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthEast);
                        HexArray[row, col].SouthEast = HexArray[row + direct[0], col + direct[1]];
                    }
                }
            }
        }

        public void Instantiate()
        {
            NativeArray<Entity> hexArray = new NativeArray<Entity>(HexArray.Length, Allocator.Temp);

            for (int row = 0; row < HexArray.GetLength(0); row++)
            {
                for (int col = 0; col < HexArray.GetLength(1); col++)
                {
                    if (HexArray[row, col] == null)
                    {
                        continue;
                    }
                    int indexOfGrid = (row * HexArray.GetLength(1)) + col;
                    hexArray[indexOfGrid] = _ecs.EntityManager.Instantiate(
                        _ecs.EntityDictionary[EEntities.Hexagon_A_01][
                            UnityEngine.Random.Range(0, _ecs.EntityDictionary[EEntities.Hexagon_A_01].Count)
                        ]
                    );
                    _ecs.EntityManager.SetComponentData(hexArray[indexOfGrid], new Translation
                    {
                        Value = new float3(
                            HexArray[row, col].X,
                            HexArray[row, col].Y,
                            HexArray[row, col].Z
                        )
                    }); 
                }
            }
            hexArray.Dispose();
        }
    }
}