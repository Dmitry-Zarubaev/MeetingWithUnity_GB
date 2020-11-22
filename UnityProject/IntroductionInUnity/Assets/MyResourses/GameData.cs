using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Game Data/Game Data")] public class GameData : ScriptableObject {

    #region Fields

    public float ScrapDamage = 10.0f;
    public float ScrapLifeTime = 2.0f;
    public float ScrapInitialSpeed = 5.0f;
    public int ScrapInitialCount = 5;
    public int ScrapsInPile = 10;

    public float ZombieMaxHealth = 20.0f;
    public float ZombieStunTime = 4.0f;
    public float ZombieBiteDamage = 25.0f;

    public float FireDamagePerSecond = 50.0f;

    public float LandmineDamage = 150.0f;
    public float LandmineEngagingTime = 1.0f;
    public int LandminesInPile = 5;
    public int LandmineInitialCount = 1;

    public int SelfDestructionTime = 60;

    #endregion
}
