using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon = null;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if(player.Weapon != null)
                {
                    player.Weapon.transform.SetParent(null);
                    player.Weapon.gameObject.SetActive(false);
                }
                player.Weapon = weapon;
                weapon.transform.SetParent(player.transform);
                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.gameObject.SetActive(true);
                SpriteRenderer weaponSprite = weapon.GetComponent<SpriteRenderer>();
                TurnVisual(true);
            }
        }
    }

    void TurnVisual(bool on)
    {
        foreach (var component in weapon.GetComponents<Component>())
        {
            TurnVisual(on, weapon.GetComponent<Weapon>());
        }   
    }

    void TurnVisual(bool on, Weapon weapon)
    {   
        weapon.GetComponent<SpriteRenderer>().enabled = on;
        weapon.GetComponent<Animator>().enabled = on;
    }
}
