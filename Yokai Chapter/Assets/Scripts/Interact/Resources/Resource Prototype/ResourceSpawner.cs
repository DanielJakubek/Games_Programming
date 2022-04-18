using System.Collections;
using UnityEngine;

/*
    Deals with spawning in an item such as health, armour etc. every few
    seconds after it has been picked up
*/
public class ResourceSpawner : MonoBehaviour
{     
    public ResourceSpawnTemplate item; //holds information on what item to spawn
    private bool spawned = false; //if the item has been spawned yet

    //Called once every frame
    private void Update() {
        
        /*  checks if the object has no children (if no resource) 
            and then spawns the resource */
        if(transform.childCount <= 0){
            spawned = false;
            StartCoroutine(WaitRespawnResource());  
        }
    }

    /*
        Deals with getting the resource to spawn and checking if it exists, if it
        does exists, spawn it and make it a parent of this object.
    */
    private void RespawnResource(){

        GameObject resourceToSpawn = Prototype.prototypeInstance.GetItem(item.resourceName);

        if(resourceToSpawn != null){
            var temp = Instantiate(resourceToSpawn, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            spawned = true;
        }
    }
    
    //Waits for x seconds before respawning object
    IEnumerator WaitRespawnResource(){

        yield return new WaitForSeconds(item.waitFor); //wait for x seconds and then respawn
        if(!spawned)
            RespawnResource();
    }
}
