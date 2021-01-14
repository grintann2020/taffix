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
        private Dictionary<ArchetypeEnum, EntityArchetype> archetypeDictionary = new Dictionary<ArchetypeEnum, EntityArchetype>();
        // private Dictionary<EntityCategory, Entity> entityDictionary = new Dictionary<EntityCategory, Entity>();
        private Dictionary<EntityEnum, List<Entity>> _entityDictionary = new Dictionary<EntityEnum, List<Entity>>();
        public Dictionary<EntityEnum, List<Entity>> EntityDictionary
        {
            get { return _entityDictionary; }
        }

        private ComponentType[] componentTypes;

        public void Init()
        {
            this._world = World.DefaultGameObjectInjectionWorld;
            this._entityManager = this._world.EntityManager;
            foreach (ArchetypeEnum archetype in Enum.GetValues(typeof(ArchetypeEnum)))
            {
                this.archetypeDictionary.Add(archetype, CreateArchetype(archetype));
            }
        }

        private EntityArchetype CreateArchetype(ArchetypeEnum archetype)
        {
            switch (archetype)
            {
                case ArchetypeEnum.None:
                    return
                        this._entityManager.CreateArchetype();
                case ArchetypeEnum.Static:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case ArchetypeEnum.Rotatable:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case ArchetypeEnum.Interactable:
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

        public void Create(EntityEnum entityEnum, ArchetypeEnum archetypeEnum, Mesh[] meshs, UnityEngine.Material[] materials)
        {
            List<Entity> newEntityList = new List<Entity>();
            if (this._entityDictionary.ContainsKey(entityEnum))
            {
                this._entityDictionary[entityEnum].Clear();
                this._entityDictionary.Remove(entityEnum);
            }
            this._entityDictionary.Add(entityEnum, newEntityList);

            for (int i = 0; i < meshs.Length; i++)
            {
                for (int j = 0; j < materials.Length; j++)
                {
                    Entity newEntity = this._entityManager.CreateEntity(this.archetypeDictionary[archetypeEnum]);
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
                    this._entityDictionary[entityEnum].Add(newEntity);
                }
            }
        }
    }
}