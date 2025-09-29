using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Events;

public class GameView : MonoBehaviour
{
    // UI 요소

    public Text plyaerNameText;
    public Text metalText;
    public Text crystaltext;
    public Text deuteriumText;
    public InputField playerNameInput;

    public Button registerButton;
    public Button loginButton;
    public Button developButton;
    public Slider progressBar;

    public  void SetPlayerName(string name)
    {
        playerNameInput.text = name;
    }

    // 버튼 클릭 리스너 설정 함수

    public void SetRegisterButtonListener(UnityAction action)
    {
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(action);
    }

    public void SetCollectButtonListener(UnityAction action)
    {
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(action);
    }

    public void SetDevelopButtonListener(UnityAction action)
    {
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(action);
    }

    public void UpdateResources(int metal, int crystal, int deuterium)
    {
        metalText.text = $"Metal : {metal}";
        crystaltext.text = $"Crystal : {crystal}";
        deuteriumText.text = $"deuterium : {deuterium}";
    }

    public void UpdateProgressBar(float value)
    {
        progressBar.value = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
