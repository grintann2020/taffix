using UnityEngine;
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
        private Vector2 _pressPosition;
        private Vector2 _releasePosition;
        private float _pressTime;
        private float _pressDuration;
        private float _releaseTime;
        private Entity _pressEntity = Entity.Null;

        private byte RAYCAST_DISTANCE = 255;
        // private int RAYCAST_DISTANCE = 1000;


        public void Init(Camera cam, ECS ecs) {
            _cam = cam;
            _ecs = ecs;
            _entityMgr = ecs.EntityMgr;

            _buildPhysicsWorld = _ecs.World.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
        }

        public void InvokeUpdate() {
            // if (Input.touchCount == 0) {
            //     return;
            // }

            // Touch touch = Input.GetTouch(0);

            // if (touch.phase == TouchPhase.Began) {
            //     Debug.Log(touch.position);
            // }

            if (Input.GetMouseButtonDown(0)) {
                // Debug.Log("GetMouseButtonDown --> " + Input.mousePosition);
                _collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
                UnityEngine.Ray screenPointToRay = _cam.ScreenPointToRay(Input.mousePosition);
                _pressEntity = Raycast(screenPointToRay.origin, screenPointToRay.GetPoint(RAYCAST_DISTANCE));
                if (_pressEntity != Entity.Null) {
                    _pressTime = Time.time;
                    _pressPosition = Input.mousePosition;
                    // Debug.Log(_pressTime + " -- " + Time.realtimeSinceStartup);
                    // Debug.Log(_pressEntity);
                    Debug.Log(_pressPosition);
                }
            }
            if (Input.GetMouseButton(0)) {
                if (_pressEntity != Entity.Null) {
                    // Debug.Log(Time.time);

                }
            }
            if (Input.GetMouseButtonUp(0)) {
                _releaseTime = Time.time;
                _pressEntity = Entity.Null;
            }
        }

        public Entity Raycast(float3 RayFrom, float3 RayTo) {
            // Debug.Log("RayFrom -> " + RayFrom +" ----> RayTo -> "+ RayTo);
            RaycastInput input = new RaycastInput()
            {
                Start = RayFrom,
                End = RayTo,
                Filter = CollisionFilter.Default
                // Filter = new CollisionFilter()
                // {
                //     BelongsTo = ~0u,
                //     CollidesWith = ~0u, // all 1s, so all layers, collide with everything
                //     GroupIndex = 0
                // }
            };
            Unity.Physics.RaycastHit hit = new Unity.Physics.RaycastHit();
            bool isHit = _collisionWorld.CastRay(input, out hit);
            if (isHit) {
                // Debug.Log(hit.Position);
                // Debug.Log(hit.SurfaceNormal);
                return _buildPhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
            }
            return Entity.Null;
        }
    }
}