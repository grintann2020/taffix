using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

namespace THEX
{
    public class Space
    {
        private ECS eCS = null;
        private HexCalculator hexCalculator = null;
        public Hex[,] hexs = null;
        public List<HexSide> hexSideList = new List<HexSide>();

        // private int[,,] hexDirections = {
        //     {{+1,  0}, { 0, +1}, {-1, +1}, {-1,  0}, {-1, -1}, { 0, -1}},
        //     {{+1,  0}, {+1, +1}, { 0, +1}, {-1,  0}, { 0, -1}, {+1, -1}}
        // };

        public void Init()
        {

        }

        public void Bind(ECS eCS, HexCalculator hexCalculator)
        {
            this.eCS = eCS;
            this.hexCalculator = hexCalculator;
        }

        // public void Create(int colsLength, int rowsLength, Size unitSize)
        public void Construct(int[,] hexData, Size unitSize)
        {
            int rowsLength = hexData.GetLength(0);
            int colsLength = hexData.GetLength(1);
            this.hexs = new Hex[rowsLength, colsLength];

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
                        this.hexs[row, col] = null;
                    }
                    else
                    {
                        float x = startingPosition.x + col * colSpacing;
                        float y = startingPosition.y + (float)hexData[row, col];
                        float z = startingPosition.z - row * rowSpacing;

                        // x += this.hexCalculator.UnitWidth((float)unitSize) * (row & 1);
                        if (row % 2 != 0)
                        {
                            x += this.hexCalculator.UnitWidth((float)unitSize);
                        }
                        this.hexs[row, col] = new Hex(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rowsLength; row++)
            // for (int row = rowsLength - 1; row >= 0; row--)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    Debug.Log("row -> " + row);
                    if (this.hexs[row, col] == null)
                    {
                        continue;
                    }

                    int[] direct = new int[2];

                    if (col < colsLength - 1)
                    {
                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.East);
                        this.hexs[row, col].East = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col < colsLength - 1)
                    {

                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.NorthEast);
                        Debug.Log(
                            " direct = " + (int)HexDirection.NorthEast + " --- " +
                            " row(" + row + " + " + direct[0] + ") = " + (row + direct[0]) + " | " +
                            " col(" + col + " + " + direct[1] + ") = " + (col + direct[1])
                        );
                        this.hexs[row, col].NorthEast = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row > 0 && col > 0)
                    {
                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.NorthWest);
                        Debug.Log(
                            " direct = " + (int)HexDirection.NorthWest + " --- " +
                            " row(" + row + " + " + direct[0] + ") = " + (row + direct[0]) + " | " +
                            " col(" + col + " + " + direct[1] + ") = " + (col + direct[1])
                        );
                        this.hexs[row, col].NorthWest = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (col > 0)
                    {
                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.West);
                        this.hexs[row, col].West = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col > 0)
                    {
                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.SouthWest);
                        this.hexs[row, col].SouthWest = this.hexs[row + direct[0], col + direct[1]];
                    }

                    if (row < rowsLength - 1 && col < colsLength - 1)
                    {
                        direct = this.hexCalculator.Adjacency(row, (int)HexDirection.SouthEast);
                        Debug.Log(
                            " direct = " + (int)HexDirection.SouthEast + " --- " +
                            " row(" + row + " + " + direct[0] + ") = " + (row + direct[0]) + " | " +
                            " col(" + col + " + " + direct[1] + ") = " + (col + direct[1])
                        );
                        this.hexs[row, col].SouthEast = this.hexs[row + direct[0], col + direct[1]];
                    }

                    // if (row < rowsLength - 1)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.East);
                    //     this.hexs[row, col].East = this.hexs[row + direct[0], col + direct[1]];
                    // }
                    // if (row < rowsLength - 1 && col < colsLength - 1)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.NorthEast);
                    //     this.hexs[row, col].NorthEast = this.hexs[row + direct[0], col + direct[1]];
                    // }
                    // if (row > 0 && col < colsLength - 1)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.NorthWest);
                    //     this.hexs[row, col].NorthWest = this.hexs[row + direct[0], col + direct[1]];
                    // }
                    // if (row > 0)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.West);
                    //     this.hexs[row, col].West = this.hexs[row + direct[0], col + direct[1]];
                    // }
                    // if (row > 0 && col > 0)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.SouthWest);
                    //     this.hexs[row, col].SouthWest = this.hexs[row + direct[0], col + direct[1]];
                    // }
                    // if (row < rowsLength - 1 && col > 0)
                    // {
                    //     direct = this.hexCalculator.Adjacency(row, (int)HexDirection.SouthEast);
                    //     this.hexs[row, col].SouthEast = this.hexs[row + direct[0], col + direct[1]];
                    // }



                    // for (int dir = 0; dir < 6; dir++)
                    // {
                    //     this.hexs[row, col].Adjacencies[dir] = this.hexCalculator.Adjacency(this.hexs, this.hexs[row, col], dir);
                    // }
                }
            }
        }

        public void Realize()
        {
            NativeArray<Entity> hexArray = new NativeArray<Entity>(this.hexs.Length, Allocator.Temp);
            NativeArray<Entity> hexSideArray = new NativeArray<Entity>(this.hexs.Length * 6, Allocator.Temp);
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
                        this.eCS.EntityDictionary[EntityCategory.Hex3][
                            UnityEngine.Random.Range(0, this.eCS.EntityDictionary[EntityCategory.Hex3].Count)
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
                    // if (row < this.hexs.GetLength(0) - 1)
                    // {
                    //     Debug.Log(row + ", " + col + "--" + this.hexs[row, col].Adjacencies[0]);
                    //     if (this.hexs[row, col].Y == 0 || this.hexs[row, col].Y < this.hexs[row, col].Adjacencies[0].Y)
                    //     {
                    //         continue;
                    //     }
                    // }
                    if (this.hexs[row, col].Y == 0)
                    {
                        continue;
                    }

                    // if (this.hexs[row, col].Adjacencies[0] != null && this.hexs[row, col].Y < this.hexs[row, col].Adjacencies[0].Y)
                    // {
                    //     continue;
                    // }

                    for (int adj = 0; adj < 6; adj++)
                    {
                        // Debug.Log( row * this.hexs.GetLength(0) + row  * this.hexs.GetLength(1) + adj);
                        if (this.hexs[row, col].Adjacencies[adj] == null)
                        {
                            continue;
                        }
                        int indexOfSide = row * this.hexs.GetLength(0) + col * 6 + adj;
                        //----------
                        EntityCategory hexSide = (EntityCategory)this.hexs[row, col].Y;
                        // Debug.Log((EntityCategory)hexSide);
                        //----------
                        hexSideArray[indexOfSide] = this.eCS.EntityManager.Instantiate(
                            this.eCS.EntityDictionary[hexSide][
                                UnityEngine.Random.Range(0, this.eCS.EntityDictionary[hexSide].Count)
                            ]
                        );
                        this.eCS.EntityManager.SetComponentData(hexSideArray[indexOfSide], new Translation
                        {
                            Value = new float3(
                                this.hexs[row, col].X,
                                this.hexs[row, col].Y,
                                this.hexs[row, col].Z
                            )
                        });
                        this.eCS.EntityManager.SetComponentData(hexSideArray[indexOfSide], new Rotation
                        {
                            Value = quaternion.RotateY(adj * 1.0472f)
                        });
                    }
                }
            }
            hexArray.Dispose();
            hexSideArray.Dispose();
        }
    }
}