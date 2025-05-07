using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public int cost;
    public bool locked = true;
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

    public void LoadData(GameData data)
    {
        data.charactersUnlocked.TryGetValue(id, out locked);
        if(!locked)
        {
            character.color = Color.white;
            locked = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        if(data.charactersUnlocked.ContainsKey(id))
        {
            data.charactersUnlocked.Remove(id);
        }
        data.charactersUnlocked.Add(id, locked);
    }
}
