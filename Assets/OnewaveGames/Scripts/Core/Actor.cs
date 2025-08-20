
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Actor : MonoBehaviour
{
    public SkillData skill;
    public Transform castPoint;

    [SerializeField] private LayerMask hitMask;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.05f;

    [SerializeField] private InputActionReference rightClickAction;     
    [SerializeField] private InputActionReference pointerPositionAction;
    [SerializeField] private InputActionReference fireSkillAction;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3? targetPos;
    [SerializeField] private bool isPlayer;
    [SerializeField] private float lastCast;

    private void OnEnable()
    {
        if (isPlayer)
        {
            rightClickAction.action.performed += OnMouseRightClick;
            rightClickAction.action.Enable();
            pointerPositionAction.action.Enable();
            fireSkillAction.action.performed += TryCast;
        }

        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        if (isPlayer)
        {
            rightClickAction.action.performed -= OnMouseRightClick;
            rightClickAction.action.Disable();
            pointerPositionAction.action.Disable();
            fireSkillAction.action.performed -= TryCast;
        }
    }

    private void OnMouseRightClick(InputAction.CallbackContext cbc)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        targetPos = GetMouseWorldPos();
    }

    public Vector3 GetMouseWorldPos()
    {
        var cam = Camera.main;
        Vector2 mousePos = pointerPositionAction.action.ReadValue<Vector2>();

        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return transform.position;
    }

    private void FixedUpdate()
    {
        if (!targetPos.HasValue) return;

        Vector3 pos = rb.position;
        Vector3 dest = targetPos.Value;

        if ((dest - pos).sqrMagnitude <= stopDistance * stopDistance)
        {
            rb.velocity = Vector2.zero;
            targetPos = null;
            return;
        }

        Vector3 vel = (dest - pos).normalized * moveSpeed;
        rb.MovePosition(pos + vel * Time.fixedDeltaTime);
    }


    void TryCast(InputAction.CallbackContext cbc)
    {
        if (skill == null) return;
        if (Time.time < lastCast + skill.cooldown)
        {
            var remainTime = lastCast + skill.cooldown - Time.time;
            Debug.LogWarning("½ºÅ³ Äð´Ù¿î : " + remainTime);
            return;
        }

        castPoint = castPoint != null ? castPoint : transform;

        var mouseWorld = GetMouseWorldPos();

        var direction = Vector3.ProjectOnPlane(mouseWorld - castPoint.position, Vector3.up).normalized;

        var ctx = new EffectContext
        {
            Caster = gameObject,
            CastPoint = castPoint,
            Direction = direction,
            HitMask = hitMask
        };

        StartCoroutine(RunSkill(ctx));
        lastCast = Time.time;
    }

    IEnumerator RunSkill(EffectContext ctx)
    {
        foreach (var e in skill.effects)
        {
            if (ctx.Cancelled) yield break;
            yield return StartCoroutine(e.Execute(ctx));
        }
    }
}
