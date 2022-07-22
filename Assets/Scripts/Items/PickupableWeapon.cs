using UnityEngine;

public class PickupableWeapon : PickUpItem
{
    [SerializeField] private Weapon _weapon;

    protected override void UseOn(Human human)
    {
        human.PickUp(_weapon);
    }
}
