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
    public class SpaceFruit
    {
        public const float HEX_RADIAN = 1.0472f;
        private ECS _ecs = null;
        private HexagonCalculator hexCalculator = null;
        public Hexagon[,] HexArray = null;

        public void Init()
        {

        }

        public void Bind(ECS ecs, HexagonCalculator hexCalculator)
        {
            _ecs = ecs;
            this.hexCalculator = hexCalculator;
        }

        // public void Create(int colsLength, int rowsLength, Size unitSize)
        public void Construct(int[,] hexData, ESize unitSize)
        {
            int rowsLength = hexData.GetLength(0);
            int colsLength = hexData.GetLength(1);
            this.HexArray = new Hexagon[rowsLength, colsLength];
            // float size = 1.0f;
            // float unitWidth = (HEXSCALE * size) / 2;
            // float unitHeight = (2 * size) / 4;
            // float rowSpacing = unitWidth * 2;
            // float colSpacing = unitHeight * 3;
            (float colSpacing, float rowSpacing) = this.hexCalculator.DistributionDistance((float)unitSize);
            Coord startingPosition = this.hexCalculator.CenterPosition(colsLength, rowsLength, colSpacing, rowSpacing);
            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (hexData[row, col] == -1)
                    {
                        this.HexArray[row, col] = null;
                    }
                    else
                    {
                        float x = startingPosition.X + col * colSpacing;
                        float y = startingPosition.Y + (float)hexData[row, col];
                        float z = startingPosition.Z - row * rowSpacing;
                        // x += this.hexCalculator.UnitWidth((float)unitSize) * (row & 1);
                        if (row % 2 != 0)
                        {
                            x += this.hexCalculator.UnitWidth((float)unitSize);
                        }
                        this.HexArray[row, col] = new Hexagon(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    if (this.HexArray[row, col] == null)
                    {
                        continue;
                    }

                    if (col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.East);
                        this.HexArray[row, col].East = this.HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthEast);
                        this.HexArray[row, col].NorthEast = this.HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.NorthWest);
                        this.HexArray[row, col].NorthWest = this.HexArray[row + direct[0], col + direct[1]];
                    }

                    if (col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.West);
                        this.HexArray[row, col].West = this.HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col > 0)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthWest);
                        this.HexArray[row, col].SouthWest = this.HexArray[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col < colsLength - 1)
                    {
                        int[] direct = this.hexCalculator.Adjacency(row, (int)EHexagonDirection.SouthEast);
                        this.HexArray[row, col].SouthEast = this.HexArray[row + direct[0], col + direct[1]];
                    }
                }
            }
        }

        public void Instantiate()
        {
            NativeArray<Entity> hexArray = new NativeArray<Entity>(this.HexArray.Length, Allocator.Temp);
            NativeArray<Entity> hexSideArray = new NativeArray<Entity>(this.HexArray.Length * 6, Allocator.Temp);
            for (int row = 0; row < this.HexArray.GetLength(0); row++)
            {
                for (int col = 0; col < this.HexArray.GetLength(1); col++)
                {
                    if (this.HexArray[row, col] == null)
                    {
                        continue;
                    }

                    // if (this.HexArray[row, col].Y < 1) // For Test
                    // {

                        int indexOfGrid = (row * this.HexArray.GetLength(1)) + col;
                        hexArray[indexOfGrid] = _ecs.EntityManager.Instantiate(
                            _ecs.EntityDictionary[EEntities.Hexagon_A_01][
                                UnityEngine.Random.Range(0, _ecs.EntityDictionary[EEntities.Hexagon_A_01].Count)
                            ]
                        );
                        _ecs.EntityManager.SetComponentData(hexArray[indexOfGrid], new Translation
                        {
                            Value = new float3(
                                this.HexArray[row, col].X,
                                this.HexArray[row, col].Y,
                                this.HexArray[row, col].Z
                            )
                        }); 
                        
                    // }

                    if (this.HexArray[row, col].Y == 0)
                    {
                        continue;
                    }

                    for (int adj = 0; adj < 6; adj++)
                    {
                        if (this.HexArray[row, col].Adjacencies[adj] == null)
                        {
                            continue;
                        }
                        if (this.HexArray[row, col].Y <= this.HexArray[row, col].Adjacencies[adj].Y)
                        {
                            continue;
                        }
                        int indexOfSide = (row * this.HexArray.GetLength(1)) + col * 6 + adj;
                        // if (row == 2 && col == 2)
                        // Debug.Log(
                        //     "[" + row + ", " + col + "].Y = " + this.HexArray[row, col].Y + " --- " +
                        //     adj + "[" + this.HexArray[row, col].Adjacencies[adj].Row + ", " + this.HexArray[row, col].Adjacencies[adj].Col + "].Y = " + this.HexArray[row, col].Adjacencies[adj].Y +
                        //     // ", Coord[" + this.HexArray[row, col].Adjacencies[adj].Grid.row +", " + this.HexArray[row, col].Adjacencies[adj].Grid.col + "]" + 
                        //     " ---> " +
                        //     (EEntities)(this.HexArray[row, col].Y - this.HexArray[row, col].Adjacencies[adj].Y)
                        // );
                        EEntities hexSide = (EEntities)(this.HexArray[row, col].Y - this.HexArray[row, col].Adjacencies[adj].Y);

                        hexSideArray[indexOfSide] = _ecs.EntityManager.Instantiate(
                            _ecs.EntityDictionary[hexSide][
                                UnityEngine.Random.Range(0, _ecs.EntityDictionary[hexSide].Count)
                                // adj
                            ]
                        );
                        _ecs.EntityManager.SetComponentData(hexSideArray[indexOfSide], new Translation
                        {
                            Value = new float3(
                                this.HexArray[row, col].X,
                                this.HexArray[row, col].Y,
                                this.HexArray[row, col].Z
                            )
                        });
                        _ecs.EntityManager.SetComponentData(hexSideArray[indexOfSide], new Rotation
                        {
                            Value = quaternion.RotateY(-adj * HEX_RADIAN)
                        });
                    }
                }
            }
            hexArray.Dispose();
            hexSideArray.Dispose();
        }
    }
}