using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RoR2;

public abstract class hudDecoInterface 
{
    public abstract void SetIsVisibleGame(bool v);
    
    public abstract void SetHudRotation(Quaternion rot);

        public abstract void SetHudOffset(Vector3 off);
        public virtual void SetDecoRotation(float angle) { }
    public virtual void SetFrameColorValue(float v) { }
   // public abstract void Update(float dt);
        public virtual void Draw() { }
    public virtual void ProcessInput(InputBankTest input) { }
    
    public virtual float GetHudTextAlpha() { return 1f; }
    public hudDecoInterface() { }
}

public class hudDecoInterfaceCombat : hudDecoInterface
{
    hudChangingColors.hudColors hudColors = new hudChangingColors.hudColors(true);
    Quaternion rotation;
    //Vector3 pivotPosition;
    Vector3 offset;
    Vector3 camPos;
    Vector3 basePosition;
    
    bool visGame = true;
    Camera camera;
    List<GameObject> pivot; //parentObject
    List<GameObject> decos;//frame + energyText
    List<GameObject> tickdeco0s;//tickBars
    GameObject frame;//frame
    
    
    public hudDecoInterfaceCombat(GameObject parentObject)
    {
        camera = parentObject.transform.parent.gameObject.GetComponent<Camera>();
        camPos = camera.transform.localPosition;
        string combatVisorName="";
        string combatHudName = "";
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            if (parentObject.transform.GetChild(i).name.StartsWith("combatVisor"))
                combatVisorName = parentObject.transform.GetChild(i).name;
            else if (parentObject.transform.GetChild(i).name.StartsWith("combatHud"))
                combatHudName = parentObject.transform.GetChild(i).name;
        }
        pivot = new List<GameObject>();
        decos = new List<GameObject>();
        frame = parentObject.transform.Find(combatVisorName + "/BaseWidget_Pivot/basewidget_nonfunctional/basewidget_deco/basewidget_frame/model_frame/CMDL_E64E5DBA").gameObject;
        for (int i = 0; i < frame.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            frame.GetComponent<MeshRenderer>().materials[i].color *= hudColors.hudFrameColor;
        }
        decos.Add(frame);
        
        decos.Add(parentObject.transform.Find(combatHudName + "/energy").gameObject);
        pivot.Add(decos[0]);
        pivot.Add(decos[1]);
        tickdeco0s = new List<GameObject>
        {
            parentObject.transform.Find(combatHudName+"/Missiles/Mtick").gameObject,
            parentObject.transform.Find(combatHudName+"/Envirorment/Entick").gameObject
        };
        pivot.Add(tickdeco0s[0]);
        pivot.Add(tickdeco0s[1]);
        
        for (int i = 0; i < tickdeco0s.Count; i++)
        {
            tickdeco0s[i].GetComponent<Image>().color = hudColors.tickDecoColor;
        }
        basePosition = frame.transform.localPosition;
        
    }
    void UpdateVisibility()
    {
        bool vis = visGame;
        for (int i = 0; i < decos.Count; i++)
        {
            decos[i].SetActive(vis);
        }
        for (int i = 0; i < tickdeco0s.Count; i++)
        {
            tickdeco0s[i].SetActive(vis);
        }
    }
    public override void SetIsVisibleGame(bool v)
    {
        visGame = v;
        UpdateVisibility();
    }

    public override void SetHudRotation(Quaternion rot)
    {
        rotation = rot;
    }

    public override void SetHudOffset(Vector3 off)
    {
        offset = off;
    }
    public override void SetFrameColorValue(float v)
    {
        Color color = v > 0f ? Color.white : hudColors.hudFrameColor;
        Material[] mat = frame.GetComponent<MeshRenderer>().materials;
        for (int i = 0; i < mat.Length; i++)
        {
            frame.GetComponent<MeshRenderer>().materials[i].color = hudColors.hudFrameColor;
        }
    }




}
