using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class CameraExtensions
{
    static float getTriangleSideB(float i_AngleA, float i_AngleB, float i_SideC)
    {
        float angleC = 180f - i_AngleB - i_AngleA;
        return (i_SideC * Mathf.Sin(i_AngleB * Mathf.Deg2Rad)) / (Mathf.Sin(angleC * Mathf.Deg2Rad));
    }
    public static void FocusObjects(this Camera i_Camera, List<Transform> i_Objects, float i_Margins, bool i_Animate = false)
    {
        if (i_Objects.Count == 0)
            return;

        float minX = float.MaxValue, maxX = -float.MaxValue, minZ = float.MaxValue, maxZ = -float.MaxValue;

        for (int i = 0; i < i_Objects.Count; i++)
        {
            if (i_Objects[i].position.x < minX)
                minX = i_Objects[i].position.x;
            if (i_Objects[i].position.x > maxX)
                maxX = i_Objects[i].position.x;
            if (i_Objects[i].position.z < minZ)
                minZ = i_Objects[i].position.z;
            if (i_Objects[i].position.z > maxZ)
                maxZ = i_Objects[i].position.z;
        }

        float width = Mathf.Abs(minX - maxX);
        float height = Mathf.Abs(minZ - maxZ);

        Vector3 focusPoint = new Vector3(minX + width / 2, 0, minZ + height / 2);

        width += i_Margins;
        height += i_Margins;

        Vector3 moveDirection = -i_Camera.transform.forward;
        Vector3 finalPosition = focusPoint;

        float rectAspectRatio = width / height;

        if (rectAspectRatio < i_Camera.aspect)
        {
            float angleA = Vector3.Angle(moveDirection, Vector3.back);
            float angleB = 180f - (angleA + i_Camera.fieldOfView / 2);
            float sideC = height / 2;

            finalPosition = focusPoint + moveDirection * getTriangleSideB(angleA, angleB, sideC);

            Vector3 pointB = focusPoint + Vector3.forward * (height / 2);
            float sideA = Vector3.Distance(finalPosition, pointB);
            float angleD = Vector3.Angle(i_Camera.transform.forward, (pointB - finalPosition).normalized);
            float angleC = (i_Camera.fieldOfView / 2) - angleD;
            angleB = 180f - Vector3.Angle(moveDirection, Vector3.back) + angleD;
            angleA = 180f - angleB - angleC;

            sideC = (sideA * Mathf.Sin(angleC * Mathf.Deg2Rad)) / (Mathf.Sin(angleA * Mathf.Deg2Rad));

            finalPosition += Vector3.back * (sideC / 2f);
        }
        else
        {
            float angleA = Vector3.Angle(moveDirection, Vector3.right);
            float angleB = 90f - Camera.VerticalToHorizontalFieldOfView(i_Camera.fieldOfView, i_Camera.aspect) / 2;
            float sideC = width / 2;

            finalPosition = focusPoint + moveDirection * getTriangleSideB(angleA, angleB, sideC);
        }

        if (i_Animate && Application.isPlaying)
        {
            DOTween.Kill(i_Camera.transform);
            i_Camera.transform.DOMove(finalPosition, 1f).SetEase(Ease.InOutSine).SetUpdate(true);
        }
        else
        {
            i_Camera.transform.position = finalPosition;
        }
    }
}