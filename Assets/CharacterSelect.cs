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
    [SerializeField] public bool locked = true;
    public bool selected = false;
    public Image character;

    // Update is called once per frame
    void Update()
    {

        if (locked) character.color = Color.black;
        else character.color = Color.white;
    }

    public void LoadData(GameData data)
    {
        data.charactersUnlocked.TryGetValue(id, out locked);
        if(locked) character.color = Color.black;
        else character.color = Color.white;
    }

    public void SaveData(GameData data)
    {
        if(data.charactersUnlocked.ContainsKey(id))
        {
            data.charactersUnlocked.Remove(id);
        }
        data.charactersUnlocked.Add(id, locked);
    }
}
