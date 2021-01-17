using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.Rendering;

namespace T
{
    public class ECS
    {
        private World _world;
        private EntityManager _entityManager;
        public EntityManager EntityManager
        {
            get { return this._entityManager; }
        }
        private Dictionary<EArchetype, EntityArchetype> archetypeDictionary = new Dictionary<EArchetype, EntityArchetype>();
        // private Dictionary<EntityCategory, Entity> entityDictionary = new Dictionary<EntityCategory, Entity>();
        private Dictionary<EEntities, List<Entity>> _entityDictionary = new Dictionary<EEntities, List<Entity>>();
        public Dictionary<EEntities, List<Entity>> EntityDictionary
        {
            get { return _entityDictionary; }
        }

        private ComponentType[] componentTypes;

        public void Init()
        {
            this._world = World.DefaultGameObjectInjectionWorld;
            this._entityManager = this._world.EntityManager;
            foreach (EArchetype archetype in Enum.GetValues(typeof(EArchetype)))
            {
                this.archetypeDictionary.Add(archetype, CreateArchetype(archetype));
            }
        }

        private EntityArchetype CreateArchetype(EArchetype archetype)
        {
            switch (archetype)
            {
                case EArchetype.None:
                    return
                        this._entityManager.CreateArchetype();
                case EArchetype.Static:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case EArchetype.Rotatable:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case EArchetype.Interactable:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld),
                            typeof(PhysicsCollider)
                        );
                default:
                    return
                        this._entityManager.CreateArchetype();
            }
        }

        public void Create(EEntities eEntities, EArchetype eArchetype, Mesh[] meshs, UnityEngine.Material[] materials)
        {
            List<Entity> newEntityList = new List<Entity>();
            if (this._entityDictionary.ContainsKey(eEntities))
            {
                this._entityDictionary[eEntities].Clear();
                this._entityDictionary.Remove(eEntities);
            }
            this._entityDictionary.Add(eEntities, newEntityList);

            for (int i = 0; i < meshs.Length; i++)
            {
                for (int j = 0; j < materials.Length; j++)
                {
                    Entity newEntity = this._entityManager.CreateEntity(this.archetypeDictionary[eArchetype]);
                    this._entityManager.AddSharedComponentData(newEntity, new RenderMesh
                    {
                        mesh = meshs[i],
                        material = materials[j],
                        castShadows = ShadowCastingMode.On,
                        receiveShadows = true
                    });
                    this._entityManager.SetComponentData(newEntity, new Translation
                    {
                        Value = new float3(0.0f, 1000.0f, 0.0f)
                    });
                    this._entityDictionary[eEntities].Add(newEntity);
                }
            }
        }
    }
}