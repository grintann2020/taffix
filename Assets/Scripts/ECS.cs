using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using UnityEngine;

namespace CCCC
{
    public class ECS
    {
        private World _world;
        private EntityManager _entityManager;
        public EntityManager EntityManager
        {
            get { return this._entityManager; }
        }
        private Dictionary<Archetype, EntityArchetype> archetypeDictionary = new Dictionary<Archetype, EntityArchetype>();
        private Dictionary<EntityCategory, List<Entity>> _entityDictionary = new Dictionary<EntityCategory, List<Entity>>();
        public Dictionary<EntityCategory, List<Entity>> EntityDictionary
        {
            get { return _entityDictionary; }
        }

        public void Init()
        {
            this._world = World.DefaultGameObjectInjectionWorld;
            this._entityManager = this._world.EntityManager;
            foreach (Archetype archetype in Enum.GetValues(typeof(Archetype)))
            {
                this.archetypeDictionary.Add(archetype, CreateArchetype(archetype));
            }
        }

        private EntityArchetype CreateArchetype(Archetype archetype)
        {
            switch (archetype)
            {
                case Archetype.Basic:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                case Archetype.Rotation:
                    return
                        this._entityManager.CreateArchetype(
                            typeof(Translation),
                            typeof(Rotation),
                            typeof(RenderMesh),
                            typeof(RenderBounds),
                            typeof(LocalToWorld)
                        );
                default:
                    return
                        this._entityManager.CreateArchetype();
            }
        }

        public void Create(EntityCategory entityCategory, Archetype archetype, Mesh[] meshs, Material[] materials)
        {
            List<Entity> newEntityList = new List<Entity>();
            if (this._entityDictionary.ContainsKey(entityCategory))
            {
                this._entityDictionary[entityCategory].Clear();
                this._entityDictionary.Remove(entityCategory);
            }
            this._entityDictionary.Add(entityCategory, newEntityList);

            for (int i = 0; i < meshs.Length; i++)
            {
                for (int j = 0; j < materials.Length; j++)
                {
                    Entity newEntity = this._entityManager.CreateEntity(this.archetypeDictionary[archetype]);
                    this._entityManager.AddSharedComponentData(newEntity, new RenderMesh
                    {
                        mesh = meshs[i],
                        material = materials[j]
                    });
                    this._entityManager.SetComponentData(newEntity, new Translation
                    {
                        Value = new float3(0.0f, 1000.0f, 0.0f)
                    });
                    this._entityDictionary[entityCategory].Add(newEntity);
                }
            }
        }
    }
}