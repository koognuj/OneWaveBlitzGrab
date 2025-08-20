using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Pull")]
public class PullEffectSO : EffectBaseSO
{
    public float pullSpeed = 25f;
    public float stopDistance = 2f;
    public float timeout = 2.5f;

    public override IEnumerator Execute(EffectContext ctx)
    {
        if (ctx.HitTarget == null) yield break;

        var target = ctx.HitTarget;
        var pullable = target.GetComponent<Pullable>();
        if (pullable == null) pullable = target.AddComponent<Pullable>();

        Vector3 dst = ctx.Caster.transform.position + ctx.Direction * stopDistance;
        yield return pullable.PullTo(dst, pullSpeed, timeout);
    }
}