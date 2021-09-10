using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SamusMod.Misc
{
    public class hudBall : MonoBehaviour
    {
        hudChangingColors.hudColors hudColors;
        GameObject bombDeco;
        Image bombIcon;
        Text bombDigits;
        List<Image> bombFilled;
        List<Image> bombEmpty;
        GameObject energyDeco;
        int pbAmount, pbCapacity, availableBombs;
        bool hasPb;
        public bool ballini;
        // Start is called before the first frame update
        void Start()
        {

            hudColors = new hudChangingColors.hudColors(false);
            string hudN = "ballHUD";

            bombDeco = gameObject.transform.Find(hudN + "/bombdeco").gameObject;
            bombIcon = gameObject.transform.Find(hudN + "/Bombs/Powerbomb/PBfill").gameObject.GetComponent<Image>();
            bombDigits = gameObject.transform.Find(hudN + "/Text/PowerBombText").gameObject.GetComponent<Text>();
            //Debug.Log(bombDigits);
            bombFilled = new List<Image>();
            bombEmpty = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                string path = hudN + "/Bombs/bomb" + (i + 1) + "/Bfill" + (i + 1);
                bombFilled.Add(gameObject.transform.Find(path).gameObject.GetComponent<Image>());
                bombFilled[i].color = hudColors.ballBombFilled;
                path = hudN + "/Bombs/bomb" + (i + 1) + "/Bempty" + (i + 1);
                bombEmpty.Add(gameObject.transform.Find(path).gameObject.GetComponent<Image>());
                bombEmpty[i].color = hudColors.ballBombEmpty;
            }
            bombDeco.GetComponent<Image>().color = hudColors.ballBombDeco;
            energyDeco = gameObject.transform.Find(hudN + "/energydeco").gameObject;
            energyDeco.GetComponent<Image>().color = hudColors.ballBombEnergyDeco;
            //SetBombParams(PbAmount, PbCapacity, AvailableBombs, true);
            ballini = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdatePowerBombReadoutColors()
        {
            Color fontColor;
            Color outlineColor;
            if (pbAmount > 0)
            {
                fontColor = hudColors.powerBombDigitAvaliableFont;
                outlineColor = hudColors.powerBombDigitAvaliableOutline;
            }
            else if (pbCapacity > 0)
            {
                fontColor = hudColors.powerBombDigitDepletedFont;
                outlineColor = hudColors.powerBombDigitDepletedOutline;
            }
            else
            {
                fontColor = Color.clear;
                outlineColor = Color.clear;
            }
            bombDigits.color = fontColor;
            bombDigits.gameObject.GetComponent<Outline>().effectColor = outlineColor;
            Color iconColor;
            if (pbAmount > 0)
                iconColor = hudColors.powerBombIconAvaliable;
            else if (pbCapacity > 0)
                iconColor = hudColors.powerBombIconUnavaliable;
            else
                iconColor = Color.clear;
            bombIcon.color = iconColor;
        }

        public void SetBombParams(int PbAmount, int PbCapacity, int AvailableBombs, bool init)
        {
            if (PbAmount != pbAmount || init)
            {
                //Debug.Log("PbAmount test: " + PbAmount);
                //Debug.Log("bombDigits test: " + bombDigits);
                bombDigits.text = PbAmount.ToString("D2");
                pbAmount = PbAmount;
                UpdatePowerBombReadoutColors();
            }
            if (pbCapacity != PbCapacity || init)
            {
                pbCapacity = PbCapacity;
                UpdatePowerBombReadoutColors();

            }
            for (int i = 0; i < 3; i++)
            {
                bool lit = i < AvailableBombs;
                bombFilled[i].gameObject.SetActive(lit);

            }
            availableBombs = AvailableBombs;

            bombDeco.SetActive(pbCapacity > 0);
            if (init)
                ballini = true;
        }
        public void SetavaliableBombs(int AvailableBombs) { availableBombs = AvailableBombs; }
    }
}