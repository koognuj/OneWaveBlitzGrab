using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillId = "RocketGrab";
    public float cooldown = 5f;

    [Tooltip("������ �Ʒ� ������� ����˴ϴ�")]
    public List<EffectBaseSO> effects = new();
}