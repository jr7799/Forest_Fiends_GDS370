using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRiseIntro : MonoBehaviour
{
    public RectTransform[] buttons;       
    public Vector2[] targetPositions;     
    public float moveSpeed = 500f;
    public float delayBetweenButtons = 0.3f;
    private void Awake()
    {
        targetPositions = new Vector2[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            Vector2 startPos = buttons[i].anchoredPosition;
            // Raise each button 150 units higher than its start
            targetPositions[i] = startPos + new Vector2(0, 150);
        }

        StartCoroutine(MoveButtonsSequentially());
    }
    void Start()
    {
        
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
