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
            Debug.LogWarning("PlayerResource ���� �� ��� üũ �ǳʶ�");
            yield break;
        }

        if (!pr.TrySpendMana(manaCost))
        {
            ctx.Cancelled = true;
            Debug.Log("���� ���� �� ��ų ���");
        }
        yield break;
    }
}