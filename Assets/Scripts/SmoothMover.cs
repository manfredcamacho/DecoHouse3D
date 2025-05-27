using UnityEngine;
using System.Collections;

public class SmoothMover : MonoBehaviour
{
    public float moveDuration = 1.5f;
    public Vector3 offsetFromTarget = new Vector3(0, 1.5f, -2f);

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Coroutine currentRoutine;

    public void MoveToTarget(Transform target)
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        if (currentRoutine != null) StopCoroutine(currentRoutine);

        // Calculamos la posición frente al objeto
        Vector3 targetPosition = target.position - target.forward * offsetFromTarget.z + target.up * offsetFromTarget.y + target.right * offsetFromTarget.x;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - targetPosition);

        currentRoutine = StartCoroutine(SmoothTransition(targetPosition, targetRotation));
    }

    public void ReturnToOriginalPosition()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(SmoothTransition(originalPosition, originalRotation));
    }

    private IEnumerator SmoothTransition(Vector3 targetPos, Quaternion targetRot)
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}
