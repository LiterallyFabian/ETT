﻿using System.Collections;
using UnityEngine;

public class CardBackground : MonoBehaviour
{
    public Core Core;
    private bool move = false;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(GameObject.Find("Canvas/ImageFade").gameObject, 2.5f);
        StartCoroutine(StartMoving());
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("TemplateCard"))
        {
            c.transform.localScale = new Vector3(c.transform.localScale.x, 0, 0.00001f);
            GameObject card = Instantiate(Resources.Load("Prefabs/MainCard") as GameObject);

            card.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            card.transform.position = new Vector3(Random.Range(0, -15), Random.Range(-20, 20), Random.Range(-10, -35));
            card.transform.rotation = Random.rotation;
            card.transform.localScale *= Random.Range(0.7f, 1.5f);
            card.GetComponent<CardObject>().dest = $"Cards/{c.name}";
            card.gameObject.tag = "LEFT";
            card.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>($"Cards/{Core.FullDeck[UnityEngine.Random.Range(0, 108)].ID}");
        }
    }

    private IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(5.1f);
        move = true;
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("LEFT"))
        {
            c.transform.position = GameObject.Find(c.GetComponent<CardObject>().dest).transform.position;
            c.transform.rotation = GameObject.Find(c.GetComponent<CardObject>().dest).transform.rotation;
            c.GetComponent<CardObject>().dest = "LEFT";
            c.GetComponent<CardObject>().menuFlip = false;
        }
    }
}