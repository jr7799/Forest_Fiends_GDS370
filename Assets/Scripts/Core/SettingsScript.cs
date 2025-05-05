using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SettingsScript : MonoBehaviour
{

    [Header("Sliders")]
    public Slider speedSlider;
    public Slider dashSlider;
    public Slider duartionSlider;

    [Header("Slider Ranges")]
    public float minSpeed = 1f;
    public float maxSpeed = 10f;

    public float minDash = 10f;
    public float maxDash = 25f;

    public float minDuration = 0.1f;
    public float maxDuration = 1f;

    [Header("Player Settings")]
    public PlayerController player;

    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject whipMenu;
    public GameObject bibleMenu;
    public GameObject rewardsMenu;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        settingsMenu.SetActive(false);
    }

    public void SettingsMenu(InputAction.CallbackContext ctx)
    {
        if(pauseMenu.activeSelf == false && whipMenu.activeSelf == false && bibleMenu.activeSelf == false && rewardsMenu.activeSelf == false)
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            Time.timeScale = settingsMenu.activeSelf ? 0 : 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePlayerSettings();
    }

    public void UpdatePlayerSettings()
    {
        player.moveSpeed = speedSlider.value;
        player.dashSpeed = dashSlider.value;
        player.dashDuration = duartionSlider.value;
    }
}
