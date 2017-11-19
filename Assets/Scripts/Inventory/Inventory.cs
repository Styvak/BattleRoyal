using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory :MonoBehaviour{

    [SerializeField] private int _maxWeaponsNumber;
    [SerializeField] private Transform _weaponParent;
    private int _currentWeapon;
    private List<Tuple< Weapon, GameObject>> _weaponsList = new List<Tuple<Weapon, GameObject>>();
    private List<Ammo> _ammoList = new List<Ammo>();

    public void AddItem(Weapon item){
        if (_weaponsList.Count == _maxWeaponsNumber - 1){
            DropItem();
        }       
        var tmp = Instantiate(item.WeaponPrefab, _weaponParent) as GameObject;
        _weaponsList.Add(new Tuple<Weapon, GameObject>(item, tmp));
        if (_weaponsList.Count() > 1)
            tmp.SetActive(false);
    }

    public void DropItem()
    {
        Instantiate(_weaponsList[_currentWeapon].First, gameObject.transform.position, Quaternion.identity);
        Destroy(_weaponsList[_currentWeapon].Second);
        _weaponsList.Remove(_weaponsList[_currentWeapon]);
        _weaponsList[_currentWeapon].Second.SetActive(true);
    }

    public void AddItem(Ammo item)
    {
        if (_ammoList.Exists( x => x.WeaponType == item.WeaponType)){
            _ammoList[_ammoList.IndexOf(_ammoList.Single(i => i.WeaponType == item.WeaponType))].AddAmmo(item.Count);
            return;
        }
        _ammoList.Add(item);
    }


    public void GetNextWeapon(){
        _weaponsList[_currentWeapon].Second.SetActive(false);
        if (_currentWeapon == _maxWeaponsNumber - 1)
            _currentWeapon = 0;
        else
            _currentWeapon++;
        _weaponsList[_currentWeapon].Second.SetActive(true);
    }

    public void GetPrevWeapon()
    {
        _weaponsList[_currentWeapon].Second.SetActive(false);
        if (_currentWeapon == 0)
            _currentWeapon = _maxWeaponsNumber - 1;
        else
            _currentWeapon--;
        _weaponsList[_currentWeapon].Second.SetActive(true);
    }

    public void GetWeaponAt(int index)
    {
        _weaponsList[_currentWeapon].Second.SetActive(false);
        if (index < _weaponsList.Count)
            _weaponsList[_currentWeapon].Second.SetActive(true);
    }

    public int GetMaxWeaponNumber()
    {
        return (_maxWeaponsNumber);
    }
}
