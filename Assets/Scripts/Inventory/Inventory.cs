using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

    [SerializeField] private int _maxWeaponsNumber;
    [SerializeField] private Transform _weaponParent;
    private int _currentWeapon = 0;
    private List<Tuple< Weapon, GameObject>> _weaponsList = new List<Tuple<Weapon, GameObject>>();
    private List<Ammo> _ammoList = new List<Ammo>();
    private InputController inputController;

    public void AddItem(Weapon item){
        if (_weaponsList.Count == _maxWeaponsNumber - 1){
            DropItem();
        }
        var tmp = Instantiate(item.WeaponPrefab, _weaponParent, false) as GameObject;
        tmp.transform.name = _weaponsList.Count.ToString();
        _weaponsList.Add(new Tuple<Weapon, GameObject>(item.Prefab, tmp));
        if (_weaponsList.Count() > 1)
        {
            CmdEnableObject(_weaponsList.Count() - 1, false);
            NetworkServer.Spawn(tmp);
        }
        else
        {
            _currentWeapon = 0;
            CmdEnableObject(_weaponsList.Count() - 1, true);
            NetworkServer.Spawn(tmp);
            Equip();
        }
        NetworkServer.Destroy(item.gameObject);
    }

    public void DropItem()
    {
        Instantiate(_weaponsList[_currentWeapon].First, gameObject.transform.position, Quaternion.identity);
        Destroy(_weaponsList[_currentWeapon].Second);
        _weaponsList.Remove(_weaponsList[_currentWeapon]);
        CmdEnableObject(_currentWeapon, true);
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
        CmdEnableObject(_currentWeapon, false);
        if (_currentWeapon == _weaponsList.Count - 1)
            _currentWeapon = 0;
        else
            _currentWeapon++;
        CmdEnableObject(_currentWeapon, true);
        Equip();
    }

    public void GetPrevWeapon()
    {
        CmdEnableObject(_currentWeapon, false);
        if (_currentWeapon == 0)
            _currentWeapon = _weaponsList.Count - 1;
        else
            _currentWeapon--;
        CmdEnableObject(_currentWeapon, true);
        Equip();
    }

    public void GetWeaponAt(int index)
    {
        if (index < _weaponsList.Count)
        {
            CmdEnableObject(_currentWeapon, false);
            CmdEnableObject(index, true);
            _currentWeapon = index;
            Equip();
        }
    }

    public int GetMaxWeaponNumber()
    {
        return (_maxWeaponsNumber);
    }

    void Equip()
    {
        StartCoroutine(EquipShooter(_currentWeapon));
    }

    IEnumerator EquipShooter(int idx)
    {
        while (!_weaponParent.Find(idx.ToString()))
            yield return null;
        GetComponent<PlayerShoot>().Equip(_weaponParent.Find(idx.ToString()).gameObject.GetComponent<Shooter>());
    }

    [Command]
    void CmdEnableObject(int idx, bool active)
    {
        RpcEnableObject(idx, active);
    }

    [ClientRpc]
    void RpcEnableObject(int idx, bool active)
    {
        if (_weaponParent.Find(idx.ToString()))
            _weaponParent.Find(idx.ToString()).gameObject.SetActive(active);
    }

    private void Start()
    {
        inputController = GameManager.Instance.InputController;
    }

    private void Update()
    {
        if (inputController.Num1) {
            GetWeaponAt(0);
        } else if (inputController.Num2) {
            GetWeaponAt(1);
        } else if (inputController.Num3) {
            GetWeaponAt(2);
        } else if (inputController.Num4) {
            GetWeaponAt(3);
        } else if (inputController.Num5) {
            GetWeaponAt(4);
        }
    }
}
