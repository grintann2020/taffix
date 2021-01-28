using System;
using System.Collections.Generic;
using UnityEngine;
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
        private HexCalc _hexCalc = null;
        public Hex[,] HexArr = null;

        public void Init() {

        }

        public void Bind(ECS eCS, HexCalc hexCalc) {
            _eCS = eCS;
            _hexCalc = hexCalc;
        }

        // public void Create(int cols, int rows, Size size)
        public void Construct(int[,] hexData, ESize size) {
            int rows = hexData.GetLength(0);
            int cols = hexData.GetLength(1);
            HexArr = new Hex[rows, cols];
            // float size = 1.0f;
            // float unitWidth = (HEXSCALE * size) / 2;
            // float unitHeight = (2 * size) / 4;
            // float rowSpacing = unitWidth * 2;
            // float colSpacing = unitHeight * 3;
            (float colSpacing, float rowSpacing) = _hexCalc.DistributeDist((float)size);
            Coord startPos = _hexCalc.CenterPos(cols, rows, colSpacing, rowSpacing);
            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    if (hexData[row, col] == -1) {
                        HexArr[row, col] = null;
                    } else {
                        float x = startPos.X + col * colSpacing;
                        float y = startPos.Y;// + (float)hexData[row, col];
                        float z = startPos.Z - row * rowSpacing;
                        // x += this.hexCalc.UnitWidth((float)size) * (row & 1);
                        if (row % 2 != 0) {
                            x += _hexCalc.UnitW(_hexCalc.HexW((float)size));
                        }
                        HexArr[row, col] = new Hex(new Grid(row, col), new Coord(x, y, z));
                    }
                }
            }

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    if (HexArr[row, col] == null) {
                        continue;
                    }

                    if (col < cols - 1) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.E);
                        HexArr[row, col].E = HexArr[row + dir[0], col + dir[1]];
                    }

                    if (row > 0 && col < cols - 1) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.NE);
                        HexArr[row, col].NE = HexArr[row + dir[0], col + dir[1]];
                    }

                    if (row > 0 && col > 0) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.NW);
                        HexArr[row, col].NW = HexArr[row + dir[0], col + dir[1]];
                    }

                    if (col > 0) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.W);
                        HexArr[row, col].W = HexArr[row + dir[0], col + dir[1]];
                    }

                    if (row < rows - 1 && col > 0) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.SW);
                        HexArr[row, col].SW = HexArr[row + dir[0], col + dir[1]];
                    }

                    if (row < rows - 1 && col < cols - 1) {
                        int[] dir = _hexCalc.Adjacency(row, (int)EHexDir.SE);
                        HexArr[row, col].SE = HexArr[row + dir[0], col + dir[1]];
                    }
                }
            }
        }

        public void Instantiate() {
            NativeArray<Entity> entityArr = new NativeArray<Entity>(HexArr.Length, Allocator.Temp);

            for (int row = 0; row < HexArr.GetLength(0); row++) {
                for (int col = 0; col < HexArr.GetLength(1); col++) {
                    if (HexArr[row, col] == null) {
                        continue;
                    }
                    int indexOfGrid = (row * HexArr.GetLength(1)) + col;
                    entityArr[indexOfGrid] = _eCS.EntityManager.Instantiate(
                        _eCS.EntityDict[EEntity.Hex_0]
                    // _eCS.EntityDict[EEntity.Hex_0][
                    //     UnityEngine.Random.Range(0, _eCS.EntityDict[EEntity.Hex_0].Count)
                    // ]
                    );
                    _eCS.EntityManager.SetComponentData(entityArr[indexOfGrid], new Translation
                    {
                        Value = new float3(
                            HexArr[row, col].X,
                            HexArr[row, col].Y,
                            HexArr[row, col].Z
                        )
                    });
                    _eCS.EntityManager.SetComponentData(entityArr[indexOfGrid], new MaterialColor
                    {
                        Value = new float4(0.0f, 0.0f, 1000.0f, 1.0f)
                        
                    });
                    Debug.Log("MaterialColor");
                }
            }
            entityArr.Dispose();
        }
    }
}