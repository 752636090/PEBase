using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMobaJoystick : WindowRoot
{
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    public Image imgDirPointRefer;
    public Transform ArrowRoot;

    private Vector2 startPos = Vector2.zero;
    private Vector2 defaultPos = Vector2.zero;

    static class MConstDefine
    {
        public static Color ColorOpaque { get; } = new Color(1, 1, 1, 1f);
        public static Color ColorTransparent { get; } = new Color(1, 1, 1, 0.5f);
        public static float PointDis { get; set; }
    }

    private void Start()
    {
        ArrowRoot.gameObject.SetActive(false);
        defaultPos = imgDirBg.transform.position;
        //Debug.Log(imgDirBg.rectTransform.TransformPoint(0.5f, 0, 0));
        //Debug.Log(imgDirPoint.rectTransform.TransformPoint(0.5f, 0, 0));
        MConstDefine.PointDis =  imgDirPointRefer.transform.position.x - imgDirBg.transform.position.x;
        RegisterMoveEvent();
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

            InputMoveKey(Vector2.zero);
        });

        imgTouch.gameObject.RegisterDrag((PointerEventData pointer, GameObject go, object[] args) =>
        {
            Vector2 dir = pointer.position - startPos;
            float len = dir.magnitude;
            if (len > MConstDefine.PointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, MConstDefine.PointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else
            {
                imgDirPoint.transform.position = pointer.position;
            }

            if (dir != Vector2.zero)
            {
                ArrowRoot.gameObject.SetActive(true);
                float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
            }

            InputMoveKey(dir.normalized);
        });
    }

    private void InputMoveKey(Vector2 dir)
    {
        Debug.Log($"Input Dir:{dir}");
    }
}
