using UnityEngine;
using UnityEngine.UI;
using static CodeMonkey.Utils.UI_TextComplex;

public class ToggleSpriteSwap : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public Image targetImage; // Set this in the Inspector

    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(toggle.isOn);
    }

    void OnToggleValueChanged(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.sprite = isOn ? onSprite : offSprite;
        }
    }
}
