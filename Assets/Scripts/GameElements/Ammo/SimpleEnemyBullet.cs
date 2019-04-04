/// Limerence Games
/// The Last Hope
/// Curator: Ilya Mosckinskov
/// Author: Dmitri Kuzmin
/// to be commented


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Player;

namespace TheLastHope.Ammo
{
    public class SimpleEnemyBullet : AAmmo
    {
        public override void OnPopulate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnDepopulate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnTriggerEnter(Collider collision)
        {
			if (collision.tag == "Player")
			{
				try
				{
					collision.gameObject.GetComponent<ABaseObject>().SetDamage(damage);
					Die(true);
				}
				catch { };
			}
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((!collision.gameObject.GetComponent<AEnemy>()) && (!collision.gameObject.GetComponent<AAmmo>())) Die(false);
        }

        private void Die(bool withSnd)
        {
            var _explosion = this.gameObject.transform.GetChild(0);
            _explosion.gameObject.SetActive(true);
            var snd = this.GetComponent<AudioSource>();
            this.gameObject.GetComponent<Renderer>().enabled = false;
            if (snd)
            {
                if (withSnd) snd.Play();
            }
            Destroy(this.gameObject, 0.0f);
        }
    }
}


