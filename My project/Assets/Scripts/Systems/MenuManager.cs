using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
    [SerializeField] private Toggle myToggle;
    [SerializeField] private TMP_InputField myInput;

    void Start()
    {
        mySlider.value = PersistenceManager.Instance.GetFloat(PersistenceManager.KeyVolume);
        myToggle.isOn = PersistenceManager.Instance.GetBool(PersistenceManager.KeyMusic);
        myInput.text = PersistenceManager.Instance.GetString(PersistenceManager.KeyUser);
    }

    private void OnDisable()
    {
        PersistenceManager.Instance.Save();
    }

    private void OnEnable()
    {
        PersistenceManager.Instance.SetInt(PersistenceManager.KeyLifes, GameManager.Instance.GetLifes());
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}
