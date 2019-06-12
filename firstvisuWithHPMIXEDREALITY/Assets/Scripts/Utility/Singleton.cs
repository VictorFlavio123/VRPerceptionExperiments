using UnityEngine;

/* Singleton
 * This script specifies a singleton script.Classes of type singleton
 * are unique objects and can be invoked with <script>.Instance
 *
 * Class reviewed by Leonardo Pavanatto on 05/2019.

 */
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Singleton owns an instance of itself,
    // being acessible anywhere on the project
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<T>();
            }
            return _Instance;
        }
    }
}
