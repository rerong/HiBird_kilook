﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mountItemManager : MonoBehaviour {

    public canvasManager canvas;
    public UnityEngine.UI.Text lpsBuyMoney;
    public float cost;
    public float objLPS;

    private int level = 0;
    private float baseCost;
    private int unit = -1;
    private char unitChar = ' ';

    //현진 추가
    public GameObject ground;
    public GameObject origin_cube;

    private Vector3 origin;
    private Vector3 trans;

    void Start()
    {
        baseCost = cost;
        origin = ground.transform.position;
        trans = origin_cube.transform.position;
    }

    void Update()
    {
        if (cost / 1000 > 1)
        {
            cost /= 1000;
            unit = (unit == -1) ? 65 : unit + 1;
            unitChar = (char)unit;
        }

        lpsBuyMoney.text = cost.ToString("F2") + unitChar + "\n" + level + "level\n";
    }

    public void purchasedItem()
    {
        if (unit != -1)
            cost *= Mathf.Pow(1000, unit % 65 + 1);

        if (canvas.gameManaging.getLove() >= cost)
        {
            canvas.gameManaging.setLove(canvas.gameManaging.getLove() - cost);
            level += 1;
            objLPS = objLPS * 1.07f;
            cost *= 1.04f;

        }

        if (unit != -1)
            cost /= Mathf.Pow(1000, unit % 65 + 1);

        if (level <= 50)
        {
            ground.transform.position = Vector3.Lerp(origin, trans, level * 0.02f);
        }
        
        //현진 추가
        if (level <= 250 && level > 50)
            ground.GetComponent<MeshRenderer>().material.color
            = Color.Lerp(canvas.level0.color, canvas.level1.color, (level-50) * 0.005f);
        else if (level < 450 && level >250)
            ground.GetComponent<MeshRenderer>().material.color
            = Color.Lerp(canvas.level1.color, canvas.level2.color, (level - 250) * 0.005f);
    }
    public int getLevel()
    {
        return level;
    }
}
