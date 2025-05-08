using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRiseIntro : MonoBehaviour
{
    public RectTransform[] buttons;       
    public Vector2[] targetPositions;     
    public float moveSpeed = 500f;
    public float delayBetweenButtons = 0.3f;

    void Start()
    {
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
