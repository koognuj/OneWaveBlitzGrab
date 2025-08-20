using UnityEngine;

public class EffectContext
{
    public GameObject Caster;            
    public Transform CastPoint;          
    public Vector3 Direction;            
    public LayerMask HitMask;            
    public bool Cancelled;               

    public GameObject HitTarget;         
    public GameObject SpawnedProjectile; 
}
