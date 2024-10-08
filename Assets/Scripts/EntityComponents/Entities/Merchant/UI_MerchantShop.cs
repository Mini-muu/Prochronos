using System;
using UnityEngine;

public class UI_MerchantShop : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Toggle() => gameObject.SetActive(!gameObject.activeSelf);

    internal void UpdateUI()
    {
        throw new NotImplementedException();
    }
}
