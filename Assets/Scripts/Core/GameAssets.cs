using UnityEngine;
//using System.Reflection;
//using UnityEditor;

public class GameAssets : MonoBehaviour
{
    public Transform pfDamagePopup;
    public GameObject bulletPop;

    private static GameAssets _i;
    public static GameAssets i {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;

        }
    }

}
