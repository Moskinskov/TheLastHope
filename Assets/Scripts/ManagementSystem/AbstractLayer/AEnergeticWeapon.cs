/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    ///  'AEnergeticWeapon' class
    ///  realizes basic bahaviour of the energetic weapon
    ///  Fire() calls WeaponMethod() method that is to be overriden in every 
    /// </summary>
    public abstract class AEnergeticWeapon : AWeapon
    {
        #region Protected variables

        [SerializeField]
        protected float maxRange;
        [SerializeField]
        protected float damagePerSecond;
        [SerializeField]
        protected float energyPerSecond;
        [SerializeField, Header("Necessary objects")]
        protected Transform muzzle;
        [SerializeField]
        protected ParticleSystem effect;

        #endregion

        #region Override methods


        #endregion

        #region Abstract Methods

        protected abstract void WeaponMethod(RaycastHit hit);

        #endregion
    }
}