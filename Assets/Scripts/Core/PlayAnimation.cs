using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    Animator anim;
    public string animTrigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(animTrigger, true);
    }
}
