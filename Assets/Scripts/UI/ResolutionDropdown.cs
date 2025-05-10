using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown screenmodeDropDown;
    private FullScreenMode screenmode = FullScreenMode.ExclusiveFullScreen;

    void Start()
    {
        if (screenmodeDropDown != null)
        {
            screenmodeDropDown.onValueChanged.AddListener(setScreenMode);
            // Match current screen mode
            int screenModeIndex = 0;
            switch (Screen.fullScreenMode)
            {
                case FullScreenMode.Windowed:
                    screenModeIndex = 0;
                    break;
                case FullScreenMode.ExclusiveFullScreen:
                    screenModeIndex = 1;
                    break;
            }

            screenmodeDropDown.value = screenModeIndex;
            setScreenMode(screenModeIndex);
        }
        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.AddListener(SetResolution);

            // Match current screen resolution
            int resolutionIndex = 0;
            if (Screen.width == 1280 && Screen.height == 720) resolutionIndex = 0;
            else if (Screen.width == 1366 && Screen.height == 768) resolutionIndex = 1;
            else if (Screen.width == 1920 && Screen.height == 1080) resolutionIndex = 2;
            else if (Screen.width == 2560 && Screen.height == 1080) resolutionIndex = 3;
            else if (Screen.width == 2560 && Screen.height == 1440) resolutionIndex = 4;

            resolutionDropdown.value = resolutionIndex;
            SetResolution(resolutionIndex);
        }
    }

    void setScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                screenmode = FullScreenMode.Windowed;
                break;
            case 1:
                screenmode = FullScreenMode.ExclusiveFullScreen;
                break;
            default:
                Debug.LogWarning("Unhandled screen mode index: " + index);
                break;
        }

        // Optional: apply current resolution again to apply new screenmode
        SetResolution(resolutionDropdown.value);
    }

    void SetResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1280, 720, screenmode);
                break;
            case 1:
                Screen.SetResolution(1366, 768, screenmode);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, screenmode);
                break;
            case 3:
                Screen.SetResolution(2560, 1080, screenmode);
                break;
            case 4:
                Screen.SetResolution(2560, 1440, screenmode);
                break;
            default:
                Debug.LogWarning("Unhandled resolution index: " + index);
                return;
        }

        Debug.Log($"Resolution set to: {Screen.width} x {Screen.height} ({screenmode})");
    }
}
