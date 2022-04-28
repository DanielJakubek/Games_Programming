using UnityEngine;

public class HitScanWeapon : Weapon
{

    public ParticleSystem muzzleFlash; 

    //Update is called once per frame
    private void Update(){
        getInput();

        //Disables the gun muzzle flash after it finishes playing
        if(muzzleFlash.isStopped)
            muzzleFlash.gameObject.SetActive(false);
    }

    /*
        Handles the gun operation, this specifically deals with
        the ammo, if there is no then shoot, otherwise, play a sound
        to indicate no more bullets.
    */
    public override void HandleWeapon(){

        //If there is ammo, shoot and subtract one ammo
        if(weaponTemplate.ammo > 0){
            weaponTemplate.ammo --;
            ShootGun();
        }else
            audioMng.PlaySound("NoAmmo", audioMng.sounds);

        EventManager.eventMngr.OnAmmoUse(weaponTemplate.ammo);
    }

    /*  
        Usese raycasting to project a ray infront of the player/player camera within a set range. If the ray hits
        something, it will be stored in the raycast variable hitTarget.
    */
    private void ShootGun(){

        weaponVFX();//Weapon sounds and animation

        //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitTarget, weaponTemplate.range))
            FindShotType(hitTarget.transform);
    }

    /*
        Particles that appear when then enemy dies
    */
    public override void ImpactParticleInstantiate(bool hitEnemy, GameObject targetHit){

        GameObject temp;

        if(hitEnemy){
            audioMng.PlaySound("EnemyHit", audioMng.sounds);
            temp = Instantiate(hitParticle, hitTarget.point, Quaternion.LookRotation(hitTarget.normal));
        }
        else
            temp = Instantiate(impactParticle, hitTarget.point, Quaternion.LookRotation(hitTarget.normal));

        Destroy(temp,1);
    }

    
    /*
        Deals with enabling the muzzle flash and playing it. Also, 
        deals with playing the pistol gun shot when clicked and playing corresponding 
        animation
    */
    private void weaponVFX(){
        //Plays the particle system of the muzzle flash
        muzzleFlash.gameObject.SetActive(true);
        muzzleFlash.Play();

        audioMng.PlaySound("Pistol", audioMng.sounds);

        //Plays the shooting animation and then extis that animation
        weaponAnimator.Play("Shoot");
        weaponAnimator.SetBool("isShooting", false);
    }
}
