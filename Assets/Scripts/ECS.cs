using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.Rendering;

namespace T {
    public class ECS {
        public EntityManager EntityManager {
            get { return _entityManager; }
        }
        public Dictionary<EEntity, Entity> EntityDict {
            get { return _entityDict; }
        }

        private World _world;
        private EntityManager _entityManager;
        private Dictionary<EArchetype, EntityArchetype> _archetypeDict = new Dictionary<EArchetype, EntityArchetype>();
        private Dictionary<EEntity, Entity> _entityDict = new Dictionary<EEntity, Entity>();
        // private Dictionary<EEntities, List<Entity>> _entityDict = new Dictionary<EEntities, List<Entity>>();
        // public Dictionary<EEntities, List<Entity>> EntityDict {
        //     get { return _entityDict; }
        // }
        private ComponentType[] componentTypes;

        public void Init() {
            _world = World.DefaultGameObjectInjectionWorld;
            _entityManager = _world.EntityManager;
            foreach (EArchetype archetype in Enum.GetValues(typeof(EArchetype))) {
                _archetypeDict.Add(archetype, CreateArchetype(archetype));
            }
        }

        private EntityArchetype CreateArchetype(EArchetype archetype) {
            switch (archetype) {
                case EArchetype.None:
                    return
                        _entityManager.CreateArchetype();
                case EArchetype.Static:
                    return
                        _entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case EArchetype.Rotatable:
                    return
                        _entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case EArchetype.Interactable:
                    return
                        _entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld),
                            typeof(PhysicsCollider)
                        );
                default:
                    return
                        _entityManager.CreateArchetype();
            }
        }

        public void Create(EEntity eEntity, EArchetype eArchetype, Mesh mesh, UnityEngine.Material material) {
            if (_entityDict.ContainsKey(eEntity)) {
                _entityDict.Remove(eEntity);
            }
            Entity entity = _entityManager.CreateEntity(_archetypeDict[eArchetype]);
            _entityManager.AddSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
                // castShadows = ShadowCastingMode.On,
                // receiveShadows = true
            });
            _entityManager.AddComponentData(entity, new MaterialColor
            {
                Value = new float4(0.0f, 0.0f, 0.0f, 1.0f)
            });
            // _entityManager.AddComponentData(entity, new URPMaterialPropertyBaseColor
            // {

            // });
            _entityManager.SetComponentData(entity, new Translation
            {
                Value = new float3(0.0f, 1000.0f, 0.0f)
            });

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
        //             Entity newEntity = _entityManager.CreateEntity(_archetypeDict[eArchetype]);
        //             _entityManager.AddSharedComponentData(newEntity, new RenderMesh
        //             {
        //                 mesh = meshs[i],
        //                 material = materials[j],
        //                 castShadows = ShadowCastingMode.On,
        //                 receiveShadows = true
        //             });
        //             _entityManager.SetComponentData(newEntity, new Translation
        //             {
        //                 Value = new float3(0.0f, 1000.0f, 0.0f)
        //             });
        //             _entityDict[eEntity].Add(newEntity);
        //         }
        //     }
        // }
    }
}