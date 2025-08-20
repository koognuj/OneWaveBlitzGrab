using System.Collections;
using UnityEngine;

public class Pullable : MonoBehaviour
{
    public IEnumerator PullTo(Vector3 targetPos, float speed, float timeout)
    {
        float t = 0f;
        var rb = GetComponent<Rigidbody>();

        while (t < timeout)
        {
            t += Time.deltaTime;
            var pos = transform.position;
            var next = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);

            transform.position = next;

            if ((next - targetPos).sqrMagnitude <= 0.0004f) yield break;
            yield return null;
        }

        Destroy(this);
    }
}