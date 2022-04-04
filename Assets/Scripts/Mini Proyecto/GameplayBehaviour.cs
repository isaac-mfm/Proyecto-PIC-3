using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBehaviour : MonoBehaviour
{
    public static GameplayBehaviour INSTANCE = null;
    [SerializeField] public PlayerController player = null;
    [SerializeField] public AIController[] rocks = null;
    private void Awake()
    {
        INSTANCE = this;
    }
    public void FinishGame(bool playerWins)
    {
        if (playerWins) RocksUIController.INSTANCE.ShowFinishedGameMessage("CONGRATS", Color.green);
        else RocksUIController.INSTANCE.ShowFinishedGameMessage("YOU DIED", Color.red);
    }
}
