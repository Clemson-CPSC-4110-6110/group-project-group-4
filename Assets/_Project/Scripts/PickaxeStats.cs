using UnityEngine;

public class PickaxeStats : MonoBehaviour
{
    public int level = 1;

    public void UpgradeLevel()
    {
        level += 1;
    }
}