using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Cost")]
public class CostEffectSO : EffectBaseSO
{
    public int manaCost = 40;

    public override IEnumerator Execute(EffectContext ctx)
    {
        var pr = ctx.Caster.GetComponent<PlayerResource>();
        if (pr == null)
        {
            Debug.LogWarning("PlayerResource 없음 → 비용 체크 건너뜀");
            yield break;
        }

        if (!pr.TrySpendMana(manaCost))
        {
            ctx.Cancelled = true;
            Debug.Log("마나 부족 → 스킬 취소");
        }
        yield break;
    }
}