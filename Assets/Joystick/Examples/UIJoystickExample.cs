using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystickExample : WindowRoot
{
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    public Transform ArrowRoot;

    private Vector2 startPos = Vector2.zero;
    private Vector2 defaultPos = Vector2.zero;

    static class MConstDefine
    {
        public static Color ColorOpaque { get; } = new Color(1, 1, 1, 1f);
        public static Color ColorTransparent { get; } = new Color(1, 1, 1, 0.5f);
    }

    private void Start() 
    {
        defaultPos = imgDirBg.transform.position;

    }

    private void RegisterMoveEvent()
    {
        ArrowRoot.gameObject.SetActive(false);

        imgTouch.gameObject.RegisterPointerDown((PointerEventData pointer, GameObject go, object[] args) =>
        {
            startPos = pointer.position;
            Debug.Log($"eventData.pos:{pointer.position}");
            imgDirPoint.color = MConstDefine.ColorOpaque;
            imgDirBg.transform.position = pointer.position;
        });

        imgTouch.gameObject.RegisterPointerUp((PointerEventData pointer, GameObject go, object[] args) =>
        {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.color = MConstDefine.ColorTransparent;
            imgDirPoint.transform.localPosition = Vector2.zero;
            ArrowRoot.gameObject.SetActive(false);
        });
    }
}
