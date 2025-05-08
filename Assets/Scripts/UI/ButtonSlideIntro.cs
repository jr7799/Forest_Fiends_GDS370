using System.Collections;
using UnityEngine;

public class ButtonSlideIntro : MonoBehaviour
{
    public RectTransform[] buttons;            // Buttons to move
    public Vector2[] targetPositions;          // Target positions set in the Inspector
    public float moveSpeed = 1000f;
    public float delayBetweenButtons = 0.3f;

    void Start()
    {
        if (buttons.Length != targetPositions.Length)
        {
            Debug.LogError("Buttons and targetPositions arrays must be the same length!");
            return;
        }

        StartCoroutine(MoveButtonsSequentially());
    }

    IEnumerator MoveButtonsSequentially()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            StartCoroutine(MoveToPosition(buttons[i], targetPositions[i]));
            yield return new WaitForSeconds(delayBetweenButtons);
        }
    }

    IEnumerator MoveToPosition(RectTransform rect, Vector2 targetPos)
    {
        while (Vector2.Distance(rect.anchoredPosition, targetPos) > 1f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        rect.anchoredPosition = targetPos;
    }
}
