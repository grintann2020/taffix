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
        // public const float HEX_RADIAN = 1.0472f;
        // public Hexagon[,] HexArr = null;
        // private ECS _ecs = null;
        // private HexagonCalculator _hexCalc = null;

        // public void Init()
        // {

        // }

        // public void Bind(ECS ecs, HexagonCalculator hexCalc)
        // {
        //     _ecs = ecs;
        //     _hexCalc = hexCalc;
        // }

        // // public void Create(int colsLength, int rowsLength, Size unitSize)
        // public void Construct(int[,] hexData, ESize unitSize)
        // {
        //     int rowsLength = hexData.GetLength(0);
        //     int colsLength = hexData.GetLength(1);
        //     this.HexArr = new Hexagon[rowsLength, colsLength];
        //     // float size = 1.0f;
        //     // float unitWidth = (HEXSCALE * size) / 2;
        //     // float unitHeight = (2 * size) / 4;
        //     // float rowSpacing = unitWidth * 2;
        //     // float colSpacing = unitHeight * 3;
        //     (float colSpacing, float rowSpacing) = _hexCalc.DistributionDistance((byte)unitSize);
        //     Coord startingPosition = _hexCalc.CenterPosition(colsLength, rowsLength, colSpacing, rowSpacing);
        //     for (int row = 0; row < rowsLength; row++)
        //     {
        //         for (int col = 0; col < colsLength; col++)
        //         {
        //             if (hexData[row, col] == -1)
        //             {
        //                 this.HexArr[row, col] = null;
        //             }
        //             else
        //             {
        //                 float x = startingPosition.X + col * colSpacing;
        //                 float y = startingPosition.Y + (float)hexData[row, col];
        //                 float z = startingPosition.Z - row * rowSpacing;
        //                 // x += _hexCalculator.UnitWidth((float)unitSize) * (row & 1);
        //                 if (row % 2 != 0)
        //                 {
        //                     x += _hexCalc.UnitWidth((float)unitSize);
        //                 }
        //                 this.HexArr[row, col] = new Hexagon(new Grid(row, col), new Coord(x, y, z));
        //             }
        //         }
        //     }

        //     for (int row = 0; row < rowsLength; row++)
        //     {
        //         for (int col = 0; col < colsLength; col++)
        //         {
        //             if (this.HexArr[row, col] == null)
        //             {
        //                 continue;
        //             }

        //             if (col < colsLength - 1)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.East);
        //                 this.HexArr[row, col].East = this.HexArr[row + direct[0], col + direct[1]];
        //             }

        //             if (row > 0 && col < colsLength - 1)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.NorthEast);
        //                 this.HexArr[row, col].NorthEast = this.HexArr[row + direct[0], col + direct[1]];
        //             }

        //             if (row > 0 && col > 0)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.NorthWest);
        //                 this.HexArr[row, col].NorthWest = this.HexArr[row + direct[0], col + direct[1]];
        //             }

        //             if (col > 0)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.West);
        //                 this.HexArr[row, col].West = this.HexArr[row + direct[0], col + direct[1]];
        //             }

        //             if (row < rowsLength - 1 && col > 0)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.SouthWest);
        //                 this.HexArr[row, col].SouthWest = this.HexArr[row + direct[0], col + direct[1]];
        //             }

        //             if (row < rowsLength - 1 && col < colsLength - 1)
        //             {
        //                 int[] direct = _hexCalc.Adjacency(row, (int)EHexagonDirection.SouthEast);
        //                 this.HexArr[row, col].SouthEast = this.HexArr[row + direct[0], col + direct[1]];
        //             }
        //         }
        //     }
        // }

        // public void Instantiate()
        // {
        //     NativeArray<Entity> hexArr = new NativeArray<Entity>(this.HexArr.Length, Allocator.Temp);
        //     NativeArray<Entity> hexSideArr = new NativeArray<Entity>(this.HexArr.Length * 6, Allocator.Temp);
        //     for (int row = 0; row < this.HexArr.GetLength(0); row++)
        //     {
        //         for (int col = 0; col < this.HexArr.GetLength(1); col++)
        //         {
        //             if (this.HexArr[row, col] == null)
        //             {
        //                 continue;
        //             }

        //             // if (this.HexArr[row, col].Y < 1) // For Test
        //             // {

        //                 int indexOfGrid = (row * this.HexArr.GetLength(1)) + col;
        //                 hexArr[indexOfGrid] = _ecs.EntityManager.Instantiate(
        //                     _ecs.EntityDict[EEntities.Hexagon_A_01][
        //                         UnityEngine.Random.Range(0, _ecs.EntityDict[EEntities.Hexagon_A_01].Count)
        //                     ]
        //                 );
        //                 _ecs.EntityManager.SetComponentData(hexArr[indexOfGrid], new Translation
        //                 {
        //                     Value = new float3(
        //                         this.HexArr[row, col].X,
        //                         this.HexArr[row, col].Y,
        //                         this.HexArr[row, col].Z
        //                     )
        //                 }); 
                        
        //             // }

        //             if (this.HexArr[row, col].Y == 0)
        //             {
        //                 continue;
        //             }

        //             for (int adj = 0; adj < 6; adj++)
        //             {
        //                 if (this.HexArr[row, col].Adjacencies[adj] == null)
        //                 {
        //                     continue;
        //                 }
        //                 if (this.HexArr[row, col].Y <= this.HexArr[row, col].Adjacencies[adj].Y)
        //                 {
        //                     continue;
        //                 }
        //                 int indexOfSide = (row * this.HexArr.GetLength(1)) + col * 6 + adj;
        //                 // if (row == 2 && col == 2)
        //                 // Debug.Log(
        //                 //     "[" + row + ", " + col + "].Y = " + this.HexArr[row, col].Y + " --- " +
        //                 //     adj + "[" + this.HexArr[row, col].Adjacencies[adj].Row + ", " + this.HexArr[row, col].Adjacencies[adj].Col + "].Y = " + this.HexArr[row, col].Adjacencies[adj].Y +
        //                 //     // ", Coord[" + this.HexArr[row, col].Adjacencies[adj].Grid.row +", " + this.HexArr[row, col].Adjacencies[adj].Grid.col + "]" + 
        //                 //     " ---> " +
        //                 //     (EEntities)(this.HexArr[row, col].Y - this.HexArr[row, col].Adjacencies[adj].Y)
        //                 // );
        //                 EEntities hexSide = (EEntities)(this.HexArr[row, col].Y - this.HexArr[row, col].Adjacencies[adj].Y);

        //                 hexSideArr[indexOfSide] = _ecs.EntityManager.Instantiate(
        //                     _ecs.EntityDict[hexSide][
        //                         UnityEngine.Random.Range(0, _ecs.EntityDict[hexSide].Count)
        //                         // adj
        //                     ]
        //                 );
        //                 _ecs.EntityManager.SetComponentData(hexSideArr[indexOfSide], new Translation
        //                 {
        //                     Value = new float3(
        //                         this.HexArr[row, col].X,
        //                         this.HexArr[row, col].Y,
        //                         this.HexArr[row, col].Z
        //                     )
        //                 });
        //                 _ecs.EntityManager.SetComponentData(hexSideArr[indexOfSide], new Rotation
        //                 {
        //                     Value = quaternion.RotateY(-adj * HEX_RADIAN)
        //                 });
        //             }
        //         }
        //     }
        //     hexArr.Dispose();
        //     hexSideArr.Dispose();
        // }
    }
}