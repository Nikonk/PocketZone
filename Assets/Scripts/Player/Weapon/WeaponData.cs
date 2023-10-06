using PocketZone.Inventory;
using UnityEngine; 

namespace PocketZone.Player
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        [Header("Shooting")]
        [SerializeField] private int _damage;
        [SerializeField] private float _range;

        [Header("Reloading")]
        [SerializeField] private ItemType _ammoType;
        [SerializeField] private int _clipSize;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _reloadTime;

        public int Damage => _damage;
        public float Range => _range;

        public ItemType AmmoType => _ammoType;
        public int ClipSize => _clipSize;
        public float FireRate => _fireRate;
        public float ReloadTime => _reloadTime;
    }
}
