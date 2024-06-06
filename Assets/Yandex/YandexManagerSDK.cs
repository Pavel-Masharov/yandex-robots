using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexManagerSDK : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void ShowAdv();

    [DllImport("__Internal")]
    public static extern void GetPlayerData();

    [DllImport("__Internal")]
    public static extern void OutputDebug();

    [DllImport("__Internal")]
    public static extern void HelloString(string str);

    [DllImport("__Internal")]
    public static extern void RateUs();

    [DllImport("__Internal")]
    public static extern int CheckRateUs();

    [DllImport("__Internal")]
    public static extern string GetCurentLang();

    [DllImport("__Internal")]
    public static extern string ShowRewardVideoForSkin(string str);

    [DllImport("__Internal")]
    public static extern string ShowRewardVideoForWeapon(string str);

    [DllImport("__Internal")]
    public static extern string BuySkin(string str);

    [DllImport("__Internal")]
    public static extern string BuyWeapon(string str);

    [DllImport("__Internal")]
    public static extern void CheckPurchase(string str);

    [DllImport("__Internal")]
    public static extern void SetLevel(string data);

    [DllImport("__Internal")]
    public static extern void GetLevel();

    [DllImport("__Internal")]
    public static extern int CheckDeviceInfo();

    [DllImport("__Internal")]
    public static extern void GetAllPurchase();

    [DllImport("__Internal")]
    public static extern void ReloadPage();

    [DllImport("__Internal")]
    public static extern void ShowRewardVideoForContinue();

    [DllImport("__Internal")]
    public static extern string GetCurentTLD();



}
