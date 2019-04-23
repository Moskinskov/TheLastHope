using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class EnemyWeapon : ARangedWeapon
    {
        public virtual void Fire(SceneData sceneData)
        {


            if (WeaponState != WeaponState.ReadyToFire)
                return;

            weaponState = WeaponState.Firing;

            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= MaxRange)
                {
                    if (hit.transform.GetComponent<ATurret>())
                    {
                        WeaponMethod(hit);
                    }
                }
            }
        }

        protected override void WeaponMethod(RaycastHit hit)
        {
            currentAmmoInClip -= ammoPerShot;

            damageEffect.transform.SetPositionAndRotation(hit.transform.position, Quaternion.Euler(hit.normal));
            hit.transform.GetComponent<ATurret>().SetDamage(damage);
            delay.Start(rateOfFire);
            Effects();
        }

        protected override void Checks()
        {
            base.Checks();
            if (WeaponState == WeaponState.Empty)
            {
                currentAmmoInClip = 100;
            }
        }
    }
}