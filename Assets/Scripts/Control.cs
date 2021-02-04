﻿using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;

namespace T {
    public class Control {
        private Camera _cam;
        private ECS _ecs;
        private EntityManager _entityMgr;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private CollisionWorld _collisionWorld;
        private byte RAYCAST_DISTANCE = 255;
        public void Init(Camera cam, ECS ecs) {
            _cam = cam;
            _ecs = ecs;
            _entityMgr = ecs.EntityMgr;
            // _buildPhysicsWorld = ecs.World.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            // _collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
        }

        public void Bind() {

        }

        public void InvokeUpdate() {
            if (Input.GetMouseButtonDown(0)) {
                // Debug.Log("GetMouseButtonDown --> " + Input.mousePosition);
                // var screenPointToRay = _cam.ScreenPointToRay(Input.mousePosition);
                // Entity eee = Raycast(screenPointToRay.origin, screenPointToRay.GetPoint(RAYCAST_DISTANCE));
                // Debug.Log(eee);
            }
        }

        // public Entity Raycast(float3 RayFrom, float3 RayTo) {
        //     RaycastInput input = new RaycastInput()
        //     {
        //         Start = RayFrom,
        //         End = RayTo,
        //         Filter = new CollisionFilter()
        //         {
        //             BelongsTo = ~0u,
        //             CollidesWith = ~0u, // all 1s, so all layers, collide with everything
        //             GroupIndex = 0
        //         }
        //     };
        //     Unity.Physics.RaycastHit hit = new Unity.Physics.RaycastHit();
        //     bool haveHit = _collisionWorld.CastRay(input, out hit);
        //     if (haveHit)
        //     {
        //         // see hit.Position
        //         // see hit.SurfaceNormal
        //         // Entity entity = _buildPhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
        //         // return entity;
        //     }
        //     return Entity.Null;
        // }
    }
}