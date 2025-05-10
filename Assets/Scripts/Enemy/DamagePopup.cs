using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {
    public static DamagePopup Create(Vector3 position, float damageAmount, bool isCriticalHit) {
        Transform damagePopUpTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopUpTransform.GetComponent<DamagePopup>();
        damagePopup.SetUp(damageAmount, isCriticalHit);

        return damagePopup;
    }
    private static int SortOrder;

    private const float DISAPPEAR_TIMER_MAX = 0.3f;
    private TextMeshPro text;    
    private Color textColor;
    [SerializeField] private float moveX = .3f;
    [SerializeField] private float moveY = .5f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float moveSpeedSubtract = 8f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private float disappearTimer;
    [SerializeField] private float disappearSpeed = 2f;
    [SerializeField] private float increaseScaleAmount = 1f;
    [SerializeField] private float decreaseScaleAmount = 1f;


    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }
    public void SetUp(float damageAmount, bool isCriticalHit)
    {
        text.SetText(damageAmount.ToString("F0"));
        if(!isCriticalHit)
        {
            text.fontSize = 10;
            textColor = UtilsClass.GetColorFromString("E48326");
        }

        else
        {
            text.fontSize = 11.5f;
            textColor = UtilsClass.GetColorFromString("E42637");
        }
        text.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        SortOrder++;
        text.sortingOrder = SortOrder;
        moveVector = new Vector3(moveX, moveY) * moveSpeed;
    }
    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * moveSpeedSubtract * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            //first half of popup
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if(textColor.a < 0)
                Destroy(gameObject);          
        }
    }
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
