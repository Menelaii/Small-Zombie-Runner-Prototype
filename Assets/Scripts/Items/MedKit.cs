using UnityEngine;

public class MedKit : PickUpItem
{
    [SerializeField] private int _healingPoints;

    protected override void UseOn(Human human)
    {
        human.Heal(_healingPoints);
    }
}
