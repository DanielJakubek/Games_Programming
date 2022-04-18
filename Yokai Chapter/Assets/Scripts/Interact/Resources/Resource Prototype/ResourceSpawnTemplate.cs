using UnityEngine;

/*  
    Scriptable object that stores the information
    on resource drop spawns
*/
[CreateAssetMenu(fileName = "New Resource Spawn")]
public class ResourceSpawnTemplate : ScriptableObject
{   
    [Header("Resource stats")]
    public float waitFor; //How long before spwaning next one
    public string resourceName; //Name of the resource to spawn
}
