/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using System.Collections;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
	/// <summary>
	/// 'AEnergeticWeapon' - class. FlameGun
	/// </summary>
	public class FlameThrower : AEnergeticWeapon
	{
		#region Serialized variables

		[SerializeField]
		private float _damagePerSecond;

		[SerializeField, Header("Necessary objects")]
		private ParticleSystem flameRenderer;

		#endregion

		#region Private variables

		private float _partCountInit = 0;
		private IEnumerator coroutine;
		private bool _isPlaying;

		#endregion

		#region Override methods

		public override void Init()
		{
			damagePerSecond = _damagePerSecond;
			if(flameRenderer) flameRenderer.emissionRate = 0;
			//_flameRenderer.Play();

			if (!flameRenderer.isStopped)
				flameRenderer.Stop();
			if (!effect.isStopped)
				effect.Stop();
			if (audioSource.isPlaying)
				audioSource.Stop();

			IsActive = true;
		}

		public override void WeaponUpdate()
		{
			delay.TimerUpdate();
			CoreChecks();
			LocalChecks();
			if (IsActive == false) effect.Stop();
		}

		public override void Fire(SceneData sceneData)
		{
			delay.Start(0.005f);
			if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
			{
				if (hit.distance <= maxRange && IsActive == true)
				{
					WeaponMethod(hit);
					FireUp(240);
				}

				else FireUp(0);
			}
		}

		protected override void WeaponMethod(RaycastHit hit)
		{
			if (hit.transform.GetComponent<AEnemy>())
			{
				if (hit.distance <= maxRange)
				{
					HitTheEnemy(hit);
					effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
					coroutine = Effect(2.0f, hit);

					if (!_isPlaying)
					{
						_isPlaying = true;
						StartCoroutine(coroutine);
					}
				}
			}
		}

		protected override void LocalChecks()
		{

		}
		#endregion

		#region Private methods

		private void FireUp(float particleCount)
		{
			if (particleCount != _partCountInit)
			{
				flameRenderer.Play();
				flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
				_partCountInit = flameRenderer.emissionRate;
			}

			if (particleCount == 0)
			{
				flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
				_partCountInit = flameRenderer.emissionRate;
				//if (_partCountInit == 0) _flameRenderer.Stop();
			}
		}

		protected void HitTheEnemy(RaycastHit hit)
		{
			hit.transform.GetComponent<AEnemy>().SetDamage(damagePerSecond * Time.deltaTime);
			CurrentAmmoInClip -= energyPerSecond * Time.deltaTime;
		}
		private IEnumerator Effect(float waitTime, RaycastHit hit)
		{
			effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
			if (!effect.isPlaying)
				effect.Play();
			if (!audioSource.isPlaying)
				audioSource.Play();

			yield return new WaitForSeconds(audioSource.clip.length);

			_isPlaying = false;
		}

		#endregion

	}
}