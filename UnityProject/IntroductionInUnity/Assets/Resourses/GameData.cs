using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Game Data/Game Data")] public class GameData : ScriptableObject {

    #region Fields

    public float ScrapDamage = 10.0f;
    public float LandmineDamage = 150.0f;

    public float ZombieMaxHealth = 20.0f;
    public float ZombieStunTime = 4.0f;
    public float ZombieBiteDamage = 25.0f;

    public float fireDamagePerSecond = 50.0f;

    public int ScrapsInPile = 10;

    #endregion
}
