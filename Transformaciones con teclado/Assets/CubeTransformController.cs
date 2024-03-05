using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTransformController : MonoBehaviour
{
    // Velocidad de las transformaciones
    public float rotationSpeed = 50f;
    public float scaleSpeed = 0.1f;
    public float translationSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Rotación
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RotateCube(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            RotateCube(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            RotateCube(Vector3.forward);

        // Escalamiento
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ScaleCube(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ScaleCube(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ScaleCube(Vector3.forward);

        // Traslación
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            TranslateCube(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            TranslateCube(-Vector3.right);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            TranslateCube(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            TranslateCube(-Vector3.up);
        else if (Input.GetKeyDown(KeyCode.Q))
            TranslateCube(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.W))
            TranslateCube(-Vector3.forward);
    }

    void RotateCube(Vector3 axis)
    {
        Matrix4x4 rotationMatrix = Transformations.RotateM(rotationSpeed * Time.deltaTime, AxisToEnum(axis));
        TransformCube(rotationMatrix);
    }

    void ScaleCube(Vector3 axis)
    {
        Matrix4x4 scaleMatrix = Transformations.ScaleM(1 + scaleSpeed * Time.deltaTime, 1 + scaleSpeed * Time.deltaTime, 1 + scaleSpeed * Time.deltaTime);
        TransformCube(scaleMatrix);
    }

    void TranslateCube(Vector3 direction)
    {
        Matrix4x4 translationMatrix = Transformations.TranslateM(direction.x * translationSpeed * Time.deltaTime, direction.y * translationSpeed * Time.deltaTime, direction.z * translationSpeed * Time.deltaTime);
        TransformCube(translationMatrix);
    }

    void TransformCube(Matrix4x4 transformationMatrix)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Transformations.Vector4To3(transformationMatrix * Transformations.Vector3To4(vertices[i]));
        }

        mesh.vertices = vertices;
    }

    Transformations.AXIS AxisToEnum(Vector3 axis)
    {
        if (axis == Vector3.right)
            return Transformations.AXIS.AX_X;
        else if (axis == Vector3.up)
            return Transformations.AXIS.AX_Y;
        else if (axis == Vector3.forward)
            return Transformations.AXIS.AX_Z;
        else
            return Transformations.AXIS.AX_X;
    }
}
