using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int maxMana = 100;
    public int currentMana = 100;

    public bool TrySpendMana(int amount)
    {
        if (currentMana < amount) return false;
        currentMana -= amount;
        return true;
    }

}