using System.Collections;
using UnityEngine;

public abstract class EffectBaseSO : ScriptableObject
{

    public abstract IEnumerator Execute(EffectContext ctx);
}