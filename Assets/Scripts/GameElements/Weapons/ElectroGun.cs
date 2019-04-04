/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// commented

using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'AEnergeticWeapon' - class
    /// </summary>
    public class ElectroGun : AEnergeticWeapon
    {
        #region Serialized variables

        [SerializeField]
        private float radiusElectro;
        [SerializeField]
        private LineRenderer LR;

        #endregion

        #region Private variables

        private List<GameObject> allEnemies;
        private List<AEnemy> nearestEnemies;
        private IEnumerator coroutine;
        private bool isPlaying;

        #endregion

        #region Override methods


        /// <summary>
        /// ElectroGun 'Init'
        /// </summary>
        public override void Init()
        {
            nearestEnemies = new List<AEnemy>();
            LR.enabled = false;
        }
        /// <summary>
        /// ElectroGun 'WeaponUpdate'
        /// </summary>
        public override void WeaponUpdate()
        {
            delay.TimerUpdate();
            CoreChecks();
            LocalChecks();
        }
        /// <summary>
        /// ElectroGun 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Fire(SceneData sceneData)
        {
            allEnemies = sceneData.Enemies;

            if (State != WeaponState.Inactive)
                return;

            State = WeaponState.Active;
            delay.Start(0.005f); //MAGIC NUMBERS!!!
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= maxRange && hit.transform.tag == "Enemy")
                {
                    WeaponMethod(hit);
                }
            }
        }

        protected override void WeaponMethod(RaycastHit hit)
        {
            FindTheNearestEnemies(hit.transform.GetComponent<AEnemy>());
            HitTheEnemies();
            SetLRToTarget(hit);

            coroutine = Effect(2.0f, hit);

            if (!isPlaying)
            {
                isPlaying = true;
                StartCoroutine(coroutine);
            }
        }

        protected override void LocalChecks()
        {
            if (State != WeaponState.Active)
            {
                LR.positionCount = 2;
                nearestEnemies.Clear();

                if (!effect.isStopped)
                    effect.Stop();
            }

            if (State != WeaponState.Active)
            {
                if (LR)
                    LR.enabled = false;
            }
        }



        #endregion

        #region Private methods

        private void SetLRToTarget(RaycastHit hit)
        {
            var enemyPosition = hit.point;
            LR.enabled = true;

            LR.SetPosition(0, muzzle.position);
            LR.SetPosition(1, enemyPosition);

            LR.positionCount = nearestEnemies.Count;
            if (nearestEnemies.Count > 0)
            {
                foreach (var enemy in nearestEnemies)
                {
                    LR.positionCount++;
                    LR.SetPosition(LR.positionCount - 1, new Vector3(enemy.transform.position.x,
                                                                               enemy.transform.position.y + 3,
                                                                               enemy.transform.position.z));
                }
            }
        }

        private void HitTheEnemies()
        {
            if (nearestEnemies.Count > 0)
            {
                foreach (var enemy in nearestEnemies)
                {
                    enemy.SetDamage(damagePerSecond * Time.deltaTime);
                    CurrentAmmoInClip -= energyPerSecond * Time.deltaTime;
                }
            }
        }

        private void FindTheNearestEnemies(AEnemy firstEnemy)
        {
            foreach (var enemy in allEnemies)
            {
                var tempEnemy = enemy?.GetComponent<AEnemy>();
                var tempDistance = Vector3.Distance(enemy.transform.position, firstEnemy.transform.position); //Если эта операция будет сильно напрягать память, можно считать дистанцию вручную.

                if (radiusElectro >= tempDistance && !nearestEnemies.Contains(tempEnemy))
                    nearestEnemies.Add(tempEnemy);
                if (radiusElectro < tempDistance && nearestEnemies.Contains(tempEnemy))
                    nearestEnemies.Remove(tempEnemy);
            }
        }

        private IEnumerator Effect(float waitTime, RaycastHit hit)
        {
            effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
            if (!effect.isPlaying)
                effect.Play();
            if (!audioSource.isPlaying)
                audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            isPlaying = false;
        }

        #endregion
    }
}