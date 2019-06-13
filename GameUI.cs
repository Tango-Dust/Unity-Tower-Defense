using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Text moneyText;
    public Text powerText;
    public Text waveText;

    public GameObject Economy;
    public GameObject Wave;
	
	// Update is called once per frame
	void Update ()
    {
        //sets the text every frame
        SetMoneyText();
        SetPowerText();
        SetWaveText();
	}

    //sets the money text
    void SetMoneyText()
    {
        //pulls money from economy and makes it a string
        moneyText.text =   Economy.GetComponent<EconomyController>().getMoney().ToString();
    }

    //sets power text
    void SetPowerText()
    {
        //pulls power from economy and makes it a string
        powerText.text =  Economy.GetComponent<EconomyController>().GetUsedPower().ToString() + " / " +Economy.GetComponent<EconomyController>().GetPower().ToString();
    }

    //sets wave text
    void SetWaveText()
    {
        //pulls wave count from spawntester and makes it a string
        waveText.text = Wave.GetComponent<SpawnTester>().GetCurrentWave().ToString();
    }
}
