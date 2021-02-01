using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[MaterialProperty("_Color", MaterialPropertyFormat.Float4, -1)]
public struct MyOwnColor : IComponentData
{
    public float4 Value;
}

public struct MyTime : IComponentData
{
    public float tt;
}

public class AnimateMyOwnColorSystem : SystemBase {
    protected override void OnUpdate() {
        // Entities.ForEach((ref MyOwnColor color/*, in MyAnimationTime t*/) => {
        Entities.ForEach((ref Translation trans, ref MyOwnColor color, ref MyTime t) => {
            t.tt += 0.01f;
            color.Value = new float4(
                // math.cos(t.Value + 1.0f),
                // math.cos(t.Value + 2.0f),
                // math.cos(t.Value + 3.0f),
                math.cos(trans.Value.x / 12.0f + t.tt * 1.0f),
                math.cos(trans.Value.z / 12.0f + t.tt * 1.5f),
                math.cos(trans.Value.y / 12.0f + t.tt * 2.0f),
                // math.cos(1.0f),
                // math.cos(2.0f),
                // math.cos(3.0f),
                1.0f);
        //}).WithoutBurst().Run();
        }).Schedule();
    }
}