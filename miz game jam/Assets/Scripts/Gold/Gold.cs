using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gold : MonoBehaviour
{

    private int _amount;
    
    private bool _collected = false;


    public static Gold Create(Vector3 position, int amount)
    {
        var reference = Instantiate(GameAssets.instance.Gold, position, Quaternion.identity);
        Gold gold = reference.GetComponent<Gold>();
        gold.Setup(amount);
        return gold;
    }


    public void Setup(int amount)
    {
        _amount = amount;
    }
    
    private void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (_collected)
            {
                LookAtPlayer();
                transform.Translate(Vector3.right * 20 * Time.deltaTime);
            }

            if (Vector2.Distance(UtilsClass.GetPlayer().transform.position, transform.position) < .1f)
            {
                GotTheCoin();
            }
        }
    }

    void GotTheCoin()
    {
        AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.CoinSFX,.4f);
        DamagePopup.Create(UtilsClass.GetPlayer().transform.position, _amount.ToString(), true);
        CurrencyManager.Points += _amount;
        Destroy(this.gameObject);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _collected = true;
        }
    }
    
    private void LookAtPlayer()
    {
        var dir = UtilsClass.GetPlayer().transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
