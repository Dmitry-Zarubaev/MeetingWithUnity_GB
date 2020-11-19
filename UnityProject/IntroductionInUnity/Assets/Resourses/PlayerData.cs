using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Data/Player Data")] public class PlayerData : ScriptableObject {

    #region Fields

    public float MaxHealth = 100.0f;

    public float MovementSpeed = 150.0f;
    public float CrouchSpeed = 50.0f;
    public float RunSpeed = 200.0f;

    public float CrouchColliderHeight = 1.0f;
    public float NormalColliderHeight = 2.0f;

    public float RotationSpeed = 90.0f;
    public float RunRotationSpeed = 60.0f;
    public float CrouchRotationSpeed = 45.0f;

    public float JumpForce = 5.0f;
    public float RunJumpForce = 7.5f;

    #endregion
}
