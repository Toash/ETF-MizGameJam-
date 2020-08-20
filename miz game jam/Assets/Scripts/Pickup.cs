using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Pickup : MonoBehaviour
{
    [Header("Configurable")] 
    [SerializeField] private int _cost;

    [SerializeField] private int _upgrade = 0;

    private Stats _playerStats;

    private void Awake()
    {
        _playerStats = UtilsClass.GetPlayer().GetComponent<Stats>();
        if (_upgrade == 0)
        {
            _upgrade = Random.Range(1, 7);
        }
    }


    private void Update()
    {
        if (GetComponent<ChangeInteractiveText>().Entered)
        {
            InteractiveText.i.ChangeText($"press e to buy {_cost} coins");
            if (Input.GetKeyDown(KeyCode.E) && CurrencyManager.Points >= _cost)
            {
                Buy();
            }
        }
    }
    

    private void Buy()
    {
        AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.PowerupSFX,.5f);
        CurrencyManager.Points = Mathf.Max(0, CurrencyManager.Points -= _cost);
        SpawnManager.i.PickupCount--;
        switch (_upgrade)
        {
            case 1:
                _playerStats.Damage *= 1.25f;
                DamagePopup.Create(_playerStats.transform.position, $"increased damage! {_playerStats.Damage}",false,false,true);
                break;
            case 2:
                _playerStats.CriticalChance *= 1.25f;
                DamagePopup.Create(_playerStats.transform.position, $"increased critical chance! {_playerStats.CriticalChance}",false,false,true);
                break;
            case 3:
                _playerStats.CriticalMultiplier *= 1.25f;
                DamagePopup.Create(_playerStats.transform.position, $"increased critical damage! {_playerStats.CriticalMultiplier}",false,false,true);
                break;
            case 4:
                _playerStats.AttackSpeed *= 1.1f;
                DamagePopup.Create(_playerStats.transform.position, $"faster attack speed! {_playerStats.AttackSpeed}",false,false,true);
                break;
            case 5:
                _playerStats.BulletSpeed = Mathf.Min(26, _playerStats.BulletSpeed *= 1.1f);
                DamagePopup.Create(_playerStats.transform.position, $"faster bullet speed! {_playerStats.BulletSpeed}",false,false,true);
                break;
            case 6:
                _playerStats.BulletsPerShot += 1;
                DamagePopup.Create(_playerStats.transform.position, "fire more bullets!",false,false,true);
                break;
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        InteractiveText.i.ChangeText("");
    }
}
