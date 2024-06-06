using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_LevelUp : IGameMode
{
    public TypeGameModes TypeGameModes => TypeGameModes.LevelUp;
    public int AmmountEnemy { get; set; }
    public int AmmountScore { get ; set ; }

    public GameMode_LevelUp(int ammountEnemy)
    {
        AmmountEnemy = ammountEnemy;

    }
    public bool CheckVictory()
    {
        if (AmmountEnemy <= 0)
            return true;

        return false;
    }

    public void Initialize(int quantityEnemies)
    {
        AmmountEnemy = quantityEnemies;
    }

    public void UpdateValues(int value)
    {
        AmmountEnemy--;
    }

    public void StartGame()
    {

    }
     
    public (string, string) GetTextVictory(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string result = "Победа. Вы прошли " + level + " уровень";
        string info = "Вы уничтожили " + quantityKill + " роботов";
        return (result, info);
    }

    public (string, string) GetTextDefeat(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string result = "Поражение. Попробуйте еще раз";
        string info = "Вы уничтожили " + quantityKill + " роботов из " + ammountEnemy;
        return (result, info);
    }

    public string GetTextUI(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {    
        string textUI = quantityKill + " / " + ammountEnemy;
        return textUI;
    }

    public (string, string) GetTextPause(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string textResult = "Пауза. " + level + " уровень.";
        string textInfo = "Осталось победить " + ammountEnemy + " роботов";

        return (textResult, textInfo);
    }
}
