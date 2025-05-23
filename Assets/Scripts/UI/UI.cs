using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] public TextMeshProUGUI warning;
    [SerializeField] public TextMeshProUGUI attackWarning;
    [SerializeField] public Image HUD;
    [SerializeField] public Image hpBar;
    [SerializeField] public Image hpBorder;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator TextTime(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
    }

    public void EnableTextOnTime(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        StartCoroutine(TextTime(text));
    }

    public void LoadWinScene()
    {

    }
}
