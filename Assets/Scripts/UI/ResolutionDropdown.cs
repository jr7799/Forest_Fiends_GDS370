using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
        else
        {
            Debug.LogWarning("Resolution dropdown not assigned in the Inspector!");
        }
    }

    void SetResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(1366, 768, FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, FullScreenMode.Windowed);
                break;
            default:
                Debug.LogWarning("Unhandled resolution index: " + index);
                return;
        }

        // Correct way to log actual in-game resolution
        Debug.Log($"Game window resolution set to: {Screen.width} x {Screen.height}");
    }
}
