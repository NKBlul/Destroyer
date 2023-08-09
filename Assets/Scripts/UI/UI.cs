using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] public TextMeshProUGUI warning;

    private void Awake()
    {
        instance = this;
    }
}
