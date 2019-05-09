using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCoin : MonoBehaviour
{
    public CoinDisplay coinDisplay;

    public void Start()
    {
        VarManager.CollectCoin();
    }
    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
