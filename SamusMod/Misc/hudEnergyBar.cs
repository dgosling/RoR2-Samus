using UnityEngine;
using UnityEngine.UI;

namespace SamusMod.Misc
{
    public class hudEnergyBar : MonoBehaviour
    {
        public enum ESetMode { Normal, Wrapped, Insta };
        //public enum CalculateMode { automatic, manual};
        //CalculateMode calculateMode;
        [SerializeField]
        public Image filledImage, shadowImage;
        [SerializeField]
        public lineTest filledLinetest, shadowLinetest;
        [SerializeField]
        LineRenderer filledLine, shadowLine;
        Color emptyColor, filledColor, shadowColor, color2;

        float tesselation, maxEnergy, filledSpeed, shadowSpeed, shadowDrainDelay, setEnergy, filledEnergy, shadowEnergy, shadowDrainDelayTimer, fullHealthLength;
        bool alwaysResetDelayTimer, wrapping;
        float fillPercentage;
        //hudTimer hudTimer;
        // Start is called before the first frame update
        void Awake()
        {
            //filledImage = gameObject.GetComponent<Image>();
            //shadowImage = gameObject.transform.parent.Find(gameObject.name + "Shadow").gameObject.GetComponent<Image>();
            if (filledImage != null && filledLine == null)
                color2 = filledImage.material.color;
            else
                color2 = filledLine.material.color;
            tesselation = 1f;
            
                maxEnergy = 0f;
            filledSpeed = 1000f;
            shadowSpeed = 1000f;
            shadowDrainDelay = 0f;
            setEnergy = 0f;
            filledEnergy = 0f;
            shadowEnergy = 0f;
            shadowDrainDelayTimer = 0f;
            fullHealthLength = 7.858663f;
            //hudTimer = new hudTimer();
            //calculateMode = CalculateMode.automatic;
            fillPercentage = 0f;
        }

        // Update is called once per frame
        private void Update()
        {
            //
        }

        public void updateBar(float dt)
        {

            if (shadowDrainDelayTimer > 0f)
                shadowDrainDelayTimer = Mathf.Max(shadowDrainDelayTimer - dt, 0f);
            if (filledEnergy < setEnergy)
            {
                if (wrapping)
                {
                    filledEnergy -= dt * filledSpeed;
                    if (filledEnergy < 0f)
                    {
                        filledEnergy = Mathf.Max(setEnergy, filledEnergy + maxEnergy);
                        wrapping = false;
                        shadowEnergy = maxEnergy;
                    }
                }
                else
                    filledEnergy = Mathf.Min(setEnergy, filledEnergy + dt * filledSpeed);
            }
            else if (filledEnergy > setEnergy)
            {
                if (wrapping)
                {
                    filledEnergy += dt * filledSpeed;
                    if (filledEnergy > maxEnergy)
                    {
                        filledEnergy = Mathf.Min(setEnergy, filledEnergy - maxEnergy);
                        wrapping = false;
                        shadowEnergy = filledEnergy;
                    }
                }
                else
                    filledEnergy = Mathf.Max(setEnergy, filledEnergy - dt * filledSpeed);
            }

            if (shadowEnergy < filledEnergy)
                shadowEnergy = filledEnergy;
            else if (shadowEnergy > filledEnergy && shadowDrainDelayTimer == 0f)
                shadowEnergy = Mathf.Max(filledEnergy, shadowEnergy - dt * shadowSpeed);
        }
        public void Draw()
        {
            //float filledT;
            //if (calculateMode == CalculateMode.automatic)
            //    filledT = fillPercentage;
            //else
            float filledT = maxEnergy > 0f ? filledEnergy / maxEnergy : 0f;
            float alphaMod = 1f;

            float shadowT = maxEnergy > 0f ? shadowEnergy / maxEnergy : 0f;

            Color filledColorT = filledColor;
            filledColorT.a *= alphaMod;
            filledColorT *= color2;
            Color shadowColorT = shadowColor;
            shadowColorT.a *= alphaMod;
            shadowColorT *= color2;
            Color emptyColorT = emptyColor;
            emptyColorT.a *= alphaMod;
            emptyColorT *= color2;
            if (filledImage != null && (filledLine == null && filledLinetest == null))
            {
                filledImage.fillAmount = filledT;
                filledImage.color = filledColorT;
                if (shadowImage)
                {
                    shadowImage.fillAmount = shadowT;
                    shadowImage.color = shadowColorT;
                }
            }
            else if (filledImage == null && (filledLinetest != null && filledLine != null))
            {
                filledLinetest.FillState = filledT;

                filledLine.colorGradient = convertColorToGradient(filledColorT);
                if (shadowLine && shadowLinetest)
                {
                    shadowLinetest.FillState = shadowT;
                    shadowLine.colorGradient = convertColorToGradient(shadowColorT);
                }
            }
        }
        public void Draw(float alphaMod)
        {
            float filledT = maxEnergy > 0f ? filledEnergy / maxEnergy : 0f;


            float shadowT = maxEnergy > 0f ? shadowEnergy / maxEnergy : 0f;


            Color filledColorT = filledColor;
            filledColorT.a *= alphaMod;
            filledColorT *= color2;
            Color shadowColorT = shadowColor;
            shadowColorT.a *= alphaMod;
            shadowColorT *= color2;
            Color emptyColorT = emptyColor;
            emptyColorT.a *= alphaMod;
            emptyColorT *= color2;

            if (filledImage != null && (filledLine == null && filledLinetest == null))
            {
                filledImage.fillAmount = filledT;
                filledImage.color = filledColorT;
                if (shadowImage)
                {
                    shadowImage.fillAmount = shadowT;
                    shadowImage.color = shadowColorT;
                }
            }
            else if (filledImage == null && (filledLinetest != null && filledLine != null))
            {
                filledLinetest.FillState = filledT;

                filledLine.colorGradient = convertColorToGradient(filledColorT);
                if (shadowLine && shadowLinetest)
                {
                    shadowLinetest.FillState = shadowT;
                    shadowLine.colorGradient = convertColorToGradient(shadowColorT);
                }
            }

        }

        public float GetActualFraction() { return maxEnergy == 0f ? 0f : setEnergy / maxEnergy; }
        public float GetSetEnergy()
        {
            //Debug.Log("got: "+setEnergy);
            return setEnergy;
        }
        public float GetMaxEnergy() { return maxEnergy; }
        public float GetFilledEnergy() { return filledEnergy; }
        public void SetCurrEnergy(float e, ESetMode mode)
        {
            e = Mathf.Clamp(e, 0f, maxEnergy);
            if (e == setEnergy)
                return;
            if (alwaysResetDelayTimer || filledEnergy == shadowEnergy)
                shadowDrainDelayTimer = shadowDrainDelay;
            wrapping = mode == ESetMode.Wrapped;
            setEnergy = e;
            //Debug.Log("set"+setEnergy);
            if (mode == ESetMode.Insta)
                filledEnergy = setEnergy;
        }
        public void SetEmptyColor(Color c) { emptyColor = c; }
        public void SetFilledColor(Color c) { filledColor = c; }
        public void SetShadowColor(Color c) { shadowColor = c; }
        public void SetMaxEnergy(float LmaxEnergy)
        {
            maxEnergy = LmaxEnergy;
            setEnergy = Mathf.Min(maxEnergy, setEnergy);
            filledEnergy = Mathf.Min(maxEnergy, filledEnergy);
            shadowEnergy = Mathf.Min(maxEnergy, shadowEnergy);
        }
        public void ResetMaxEnergy() { SetMaxEnergy(tesselation); }
        public void SetTesselation(float t) { tesselation = t; }
        public void SetIsAlwaysResetTimer(bool b) { alwaysResetDelayTimer = b; }
        public void SetFilledDrainSpeed(float s) { filledSpeed = s; }
        public void SetShadowDrainSpeed(float s) { shadowSpeed = s; }
        public void SetShadowDrainDelay(float d) { shadowDrainDelay = d; }

        public void initBars(Image healthBar, Image shadowBar)
        {
            filledImage = healthBar;
            shadowImage = shadowBar;
        }

        public void initBars(lineTest healthBar, lineTest shadowBar, LineRenderer healthbarLine, LineRenderer shadowBarLine)
        {
            filledLinetest = healthBar;
            shadowLinetest = shadowBar;
            filledLine = healthbarLine;
            shadowLine = shadowBarLine;
        }

        public Color GetFilledColor() { return filledColor; }
        public Color GetShadowColor() { return shadowColor; }
        public Color GetEmptyColor() { return emptyColor; }

        Gradient convertColorToGradient(Color color)
        {
            GradientColorKey[] tempKeys = new GradientColorKey[2]
    {
                new GradientColorKey(color,0),
                new GradientColorKey(color,1)
    };
            GradientAlphaKey[] tempAKeys = new GradientAlphaKey[2]
            {
                new GradientAlphaKey(color.a,0),
                new GradientAlphaKey(color.a,1)
            };
            Gradient tempGradient = new Gradient();
            tempGradient.colorKeys = tempKeys;
            tempGradient.alphaKeys = tempAKeys;
            return tempGradient;
        }

        //public CalculateMode GetCalculateMode()
        //{
        //    return calculateMode;
        //}
        //public void SetCalulateMode(CalculateMode PcalculateMode)
        //{
        //    calculateMode = PcalculateMode;
        //}

        //public void SetBarPercentage(float perc)
        //{
        //    fillPercentage = Mathf.Clamp01(perc);
        //}
    }
}

