using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WebcamCoockie : MonoBehaviour
{
    public string deviceName;
    WebCamTexture wct;
    public static bool useWebCam = true;
    // Use this for initialization
    void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        deviceName = devices[0].name;
        wct = new WebCamTexture(deviceName, 400, 400, 12);
        GetComponent<Image>().material.mainTexture = wct;
        wct.Play();

        //Rect rect = new Rect();
        //rect.width = wct.width;
        //rect.height = wct.height;
        //Sprite sprite = Sprite.Create(Convert_WebCamTexture_To_Texture2d(wct), rect, rect.center);
        //GetComponent<Image>().sprite = sprite;
        //GetComponent<Image>().material = null;
    }

    public Texture2D Convert_WebCamTexture_To_Texture2d(WebCamTexture _webCamTexture)
    {
        Texture2D _texture2D = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        _texture2D.SetPixels(_webCamTexture.GetPixels());

        return _texture2D;
    }
}
