using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : MonoBehaviour
{
    public int cost;
    public bool locked;
    public bool selected = false;
    public Image character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(locked) character.color = Color.black;
        else character.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) character.color = Color.black;
        else character.color = Color.white;
    }
}
