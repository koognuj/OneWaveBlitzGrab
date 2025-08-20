using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Projectile")]
public class ProjectileEffectSO : EffectBaseSO
{
    public GameObject projectilePrefab;
    public float speed = 30f;
    public float maxDistance = 25f;
    public float radius = 0.25f;

    public override IEnumerator Execute(EffectContext ctx)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab πÃ¡ˆ¡§");
            yield break;
        }

        var go = Object.Instantiate(projectilePrefab, ctx.CastPoint.position,
                    Quaternion.LookRotation(ctx.Direction, Vector3.up));
        ctx.SpawnedProjectile = go;

        var proj = go.GetComponent<Projectile>();
        if (proj == null) proj = go.AddComponent<Projectile>();

        proj.Initialize(ctx.Direction, speed, maxDistance, ctx.HitMask, OnHit: hit =>
        {
            if (ctx.HitTarget == null) ctx.HitTarget = hit.transform.parent.gameObject;
        }, radius: radius);

        while (proj != null && proj.IsAlive)
            yield return null;
    }
}