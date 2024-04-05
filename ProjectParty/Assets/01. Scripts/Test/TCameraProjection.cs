using UnityEngine;

public class TCameraProjection : MonoBehaviour
{
	private void Awake()
    {
        
    }

    private void Start()
    {
        Camera cam = Camera.main;
        Rect rt = new Rect(Vector2.zero, new Vector2());
        Camera.main.projectionMatrix = Orthographic(rt.xMin, rt.xMax, rt.yMin, rt.yMax, cam.nearClipPlane, cam.farClipPlane);
    }

    Matrix4x4 Orthographic(float left, float right, float bottom, float top, float near, float far)
    {
        Matrix4x4 matrix = new Matrix4x4();

        matrix[0, 0] = 2.0f / (right - left);
        matrix[0, 3] = -(right + left) / (right - left);
        matrix[1, 1] = 2.0f / (top - bottom);
        matrix[1, 3] = -(top + bottom) / (top - bottom);
        matrix[2, 2] = -2.0f / (far - near);
        matrix[2, 3] = -(far + near) / (far - near);
        matrix[3, 3] = 1.0f;

        return matrix;
    }

    Rect NearPlaneDimensions(Camera cam) {
        Rect r = new Rect();
        r.height = 2 * cam.nearClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        r.width = r.height * cam.aspect;
        r.x = 0;
        r.y = 0;
        // r.x = -(r.width / 2);
        // r.y = -(r.height / 2);
        return r;
    }
}
