using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Ammo
{
    public class SimpleBullet : AAmmo
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
            try
            {
                collision.gameObject.GetComponent<AEnemy>().SetDamage(damage);
                Die(true);
            }
            catch { };
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
            Destroy(this.gameObject, 0.3f);
        }
    }

}
