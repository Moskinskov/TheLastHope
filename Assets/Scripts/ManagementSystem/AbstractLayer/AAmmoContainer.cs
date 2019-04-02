using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract ammunition container class.
	/// </summary>
	/// <remarks>
	/// Use this class to create new boxes for ammunition storage.
	/// </remarks>

	public abstract class AAmmoContainer : MonoBehaviour
    {
		#region Private variables
		/// <summary>
		/// Type and quantity of ammunition stored.
		/// </summary>
		private Dictionary<AmmoType, int> ammo;
		/// <summary>
		/// Slots quantity that the ammunition type occupies.
		/// </summary>
		private Dictionary<AmmoType, int> ammoSize;
		#endregion

		#region Public properties
		public Dictionary<AmmoType, int> Ammo { get => ammo; set => ammo = value; }
		public Dictionary<AmmoType, int> AmmoSize { get => ammoSize; set => ammoSize = value; }
		#endregion

		#region Abstracts methods
		/// <summary>
		/// Ammunition container initialization.
		/// </summary>
		public abstract void Init();
		#endregion

		#region Virtual methods
		/// <summary>
		/// Method that checks whether the amount of selected type
		/// is enough for the action (e.g. Reload).
		/// </summary>
		/// <param name="type"> Type of ammunition being checked.</param>
		/// <param name="amount"> Quantity of ammunition being checked.</param>
		public virtual bool GetAmmo(AmmoType type, int amount)
        {
            if(Ammo[type] >= amount)
            {
                Ammo[type] -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
		#endregion
	}
}

