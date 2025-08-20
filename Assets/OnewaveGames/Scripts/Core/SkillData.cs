using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillId = "RocketGrab";
    public float cooldown = 5f;

    [Tooltip("위에서 아래 순서대로 실행됩니다")]
    public List<EffectBaseSO> effects = new();
}