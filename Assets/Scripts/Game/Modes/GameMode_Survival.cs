using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_Survival : IGameMode
{
    public TypeGameModes TypeGameModes => TypeGameModes.Survival;
    public int AmmountEnemy { get; set; }
    public int AmmountScore { get; set; }

    public GameMode_Survival(int ammountEnemy)
    {
        AmmountEnemy = ammountEnemy;
    }

    public bool CheckVictory()
    {      
        return false;
    }

    public void Initialize(int quantityEnemies)
    {
        AmmountEnemy = quantityEnemies;
    }

    public void UpdateValues(int value)
    {
        AmmountScore += 100;
    }

    public void StartGame()
    {
        
    }

    public (string, string) GetTextVictory(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string result = "������. �� ������ " + level + " �������";
        string info = "�� ���������� " + quantityKill + " �������";
        return (result, info);
    }

    public (string, string) GetTextDefeat(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string result = "���������. ���������� ��� ���";
        string info = "�� ���������� " + quantityKill + " �������! �� ������� " + AmmountScore + " �����";
        return (result, info);
    }

    public string GetTextUI(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string textUI = AmmountScore + " �����";
        return textUI;
    }

    public (string, string) GetTextPause(int level = 0, int ammountEnemy = 0, int quantityKill = 0)
    {
        string textResult = "�����";
        string textInfo = "������� " + AmmountScore + " �����";

        return (textResult, textInfo);
    }
}
