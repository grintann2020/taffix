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
        private ECS eCS = null;
        private HexagonCalculator hexCalculator = null;
        public Hexagon[,] hexs = null;

        public void Init()
        {

        }

        public void Bind(ECS eCS, HexagonCalculator hexCalculator)
        {
            this.eCS = eCS;
            this.hexCalculator = hexCalculator;
        }

        // public void Create(int colsLength, int rowsLength, Size size)
        public void Construct(int[,] hexData, ESize size)
        {
            int rowsLength = hexData.GetLength(0);
            int colsLength = hexData.GetLength(1);
            this.hexs = new Hexagon[rowsLength, colsLength];
            // float size = 1.0f;
            // float unitWidth = (HEXSCALE * size) / 2;
            // float unitHeight = (2 * size) / 4;
            // float rowSpacing = unitWidth * 2;
            // float colSpacing = unitHeight * 3;
            (float colSpacing, float rowSpacing) = this.hexCalculator.DistributionDistance((float)size);
            Coord startingPosition = this.hexCalculator.CenterPosition(colsLength, rowsLength, colSpacing, rowSpacing);
            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (hexData[row, col] == -1)
                    {
                        this.hexs[row, col] = null;
                    }
                    else
                    {
                        float x = startingPosition.x + col * colSpacing;
                        float y = startingPosition.y;// + (float)hexData[row, col];
                        float z = startingPosition.z - row * rowSpacing;
                        // x += this.hexCalculator.UnitWidth((float)size) * (row & 1);
                        if (row % 2 != 0)
                        {
                            x += this.hexCalculator.UnitWidth((float)size);
                        }
                        this.hexs[row, col] = new Hexagon(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (this.hexs[row, col] == null)
                    {
                        continue;
                    }

                    if (col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.East);
                        this.hexs[row, col].East = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthEast);
                        this.hexs[row, col].NorthEast = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthWest);
                        this.hexs[row, col].NorthWest = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.West);
                        this.hexs[row, col].West = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthWest);
                        this.hexs[row, col].SouthWest = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthEast);
                        this.hexs[row, col].SouthEast = this.hexs[row + direct[0], col + direct[1]];
                    }
                }
            }
        }

        public void Instantiate()
        {
            NativeArray<Entity> hexArray = new NativeArray<Entity>(this.hexs.Length, Allocator.Temp);

            for (int row = 0; row < this.hexs.GetLength(0); row++)
            {
                for (int col = 0; col < this.hexs.GetLength(1); col++)
                {
                    if (this.hexs[row, col] == null)
                    {
                        continue;
                    }
                    int indexOfGrid = (row * this.hexs.GetLength(1)) + col;
                    hexArray[indexOfGrid] = this.eCS.EntityManager.Instantiate(
                        this.eCS.EntityDictionary[EEntities.Hexagon_A_01][
                            UnityEngine.Random.Range(0, this.eCS.EntityDictionary[EEntities.Hexagon_A_01].Count)
                        ]
                    );
                    this.eCS.EntityManager.SetComponentData(hexArray[indexOfGrid], new Translation
                    {
                        Value = new float3(
                            this.hexs[row, col].X,
                            this.hexs[row, col].Y,
                            this.hexs[row, col].Z
                        )
                    }); 
                }
            }
            hexArray.Dispose();
        }
    }
}