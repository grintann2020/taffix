using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace T {
    public class ECS {
        public EntityManager EntityMgr {
            get { return _entityMgr; }
        }
        public Dictionary<EEntity, Entity> EntityDict {
            get { return _entityDict; }
        }
        public World World {
            get { return _world; }
        }
        private World _world;
        private EntityManager _entityMgr;
        private Dictionary<EArchetype, EntityArchetype> _archetypeDict = new Dictionary<EArchetype, EntityArchetype>();
        private Dictionary<EEntity, Entity> _entityDict = new Dictionary<EEntity, Entity>();
        // private Dictionary<EEntities, List<Entity>> _entityDict = new Dictionary<EEntities, List<Entity>>();
        // public Dictionary<EEntities, List<Entity>> EntityDict {
        //     get { return _entityDict; }
        // }
        private ComponentType[] componentTypes;

        public void Init() {
            _world = World.DefaultGameObjectInjectionWorld;
            _entityMgr = _world.EntityManager;
            foreach (EArchetype archetype in Enum.GetValues(typeof(EArchetype))) {
                _archetypeDict.Add(archetype, CreateArchetype(archetype));
            }
        }

        private EntityArchetype CreateArchetype(EArchetype archetype) {
            switch (archetype) {
                case EArchetype.None:
                    return
                        _entityMgr.CreateArchetype();
                case EArchetype.Static:
                    return
                        _entityMgr.CreateArchetype(
                            typeof(Translation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        // typeof(PhysicsCollider)
                        );
                case EArchetype.Rotatable:
                    return
                        _entityMgr.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        // typeof(PhysicsCollider)
                        );
                case EArchetype.Interactable:
                    return
                        _entityMgr.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld),
                            typeof(PhysicsCollider)
                        );
                default:
                    return
                        _entityMgr.CreateArchetype();
            }
        }

        public void Create(EEntity eEntity, EArchetype eArchetype, Mesh mesh, UnityEngine.Material material) {
            if (_entityDict.ContainsKey(eEntity)) {
                _entityDict.Remove(eEntity);
            }
            Entity entity = _entityMgr.CreateEntity(_archetypeDict[eArchetype]);
            _entityMgr.AddSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
                castShadows = ShadowCastingMode.On,
                receiveShadows = true
            });
            _entityMgr.AddComponentData(entity, new MyOwnColor
            {
                Value = new float4(0.0f, 0.0f, 0.0f, 0.0f)
            });
            _entityMgr.AddComponentData(entity, new MyTime
            {
                tt = 0.1f
            });

            // NativeArray<float3> vertexBuffer = mesh.GetNativeVertexBufferPtr(0);
            Vector3[] vertexArr = mesh.vertices;
            float3[] float3Arr = new float3[mesh.vertices.Length];
            for (int i = 0; i < vertexArr.Length; i++) {
                float3Arr[i] = vertexArr[i];
            }
            NativeArray<float3> vertexBuffer = new NativeArray<float3>(vertexArr.Length, Allocator.Temp);
            vertexBuffer.CopyFrom(float3Arr);

            int[] triangleArr = mesh.triangles;
            int3[] int3Arr = new int3[mesh.triangles.Length];
            for (int i = 0; i < triangleArr.Length; i++) {
                int3Arr[i] = triangleArr[i];
            }
            NativeArray<int3> triangleBuffer = new NativeArray<int3>(triangleArr.Length, Allocator.Temp);
            triangleBuffer.CopyFrom(int3Arr);

            BlobAssetReference<Unity.Physics.Collider> collider1 = Unity.Physics.MeshCollider.Create(
                vertexBuffer,
                triangleBuffer,
                CollisionFilter.Default
            );

            BlobAssetReference<Unity.Physics.Collider> collider2 = Unity.Physics.PolygonCollider.CreateQuad(
                new float3(-1.0f, 0.0f, 1.0f),
                new float3(1.0f, 0.0f, 1.0f),
                new float3(1.0f, 0.0f, -1.0f),
                new float3(-1.0f, 0.0f, -1.0f),
                CollisionFilter.Default
            );

            CylinderGeometry cylinderGeometry = new CylinderGeometry();
            cylinderGeometry.Center = new float3(0.0f, 0.0f, 0.0f);
            // cylinderGeometry.Orientation = new quaternion(); but quaternion(0.0f, 0.0f, 0.0f, 0.0f) is not vaild;
            cylinderGeometry.Orientation = quaternion.Euler(-1.5708f, 0, 1.5708f); // 1.5708 radian = 90 degree
            cylinderGeometry.Height = 0.1f;
            cylinderGeometry.Radius = 1.0f;
            cylinderGeometry.BevelRadius = 0.0f;
            cylinderGeometry.SideCount = 6;

            BlobAssetReference<Unity.Physics.Collider> collider3 = Unity.Physics.CylinderCollider.Create(
                cylinderGeometry,
                CollisionFilter.Default
            );

            _entityMgr.AddComponentData(entity, new PhysicsCollider
            {
                Value = collider3
            });

            _entityMgr.SetComponentData(entity, new Translation
            {
                Value = new float3(0.0f, 1000.0f, 0.0f)
            });
            // _entityMgr.AddComponentData(entity, new MaterialColor
            // {
            //     Value = new float4(0.0f, 0.0f, 0.0f, 0.0f)
            // });

            vertexBuffer.Dispose();
            triangleBuffer.Dispose();

            _entityDict.Add(eEntity, entity);
        }

        // public void Create(EEntities eEntity, EArchetype eArchetype, Mesh[] meshs, UnityEngine.Material[] materials) {
        //     List<Entity> newEntityList = new List<Entity>();
        //     if (_entityDict.ContainsKey(eEntity)) {
        //         _entityDict[eEntity].Clear();
        //         _entityDict.Remove(eEntity);
        //     }
        //     _entityDict.Add(eEntity, newEntityList);

        //     for (int i = 0; i < meshs.Length; i++) {
        //         for (int j = 0; j < materials.Length; j++) {
        //             Entity newEntity = _entityMgr.CreateEntity(_archetypeDict[eArchetype]);
        //             _entityMgr.AddSharedComponentData(newEntity, new RenderMesh
        //             {
        //                 mesh = meshs[i],
        //                 material = materials[j],
        //                 castShadows = ShadowCastingMode.On,
        //                 receiveShadows = true
        //             });
        //             _entityMgr.SetComponentData(newEntity, new Translation
        //             {
        //                 Value = new float3(0.0f, 1000.0f, 0.0f)
        //             });
        //             _entityDict[eEntity].Add(newEntity);
        //         }
        //     }
        // }
    }
}