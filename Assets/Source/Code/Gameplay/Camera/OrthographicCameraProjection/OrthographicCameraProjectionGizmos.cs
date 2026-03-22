using UnityEngine;

namespace Source.Code.Gameplay.Camera.OrthographicCameraProjection
{
    [RequireComponent(typeof(MeshFilter))]
    public class OrthographicCameraProjectionGizmos : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private MeshFilter _meshFilter;

        private Vector3 _projectedBottomLeft;
        private Vector3 _projectedBottomRight;
        private Vector3 _projectedTopLeft;
        private Vector3 _projectedTopRight;

        private float _width;
        private float _height;

        private void OnDrawGizmos()
        {
            if (_camera == null)
            {
                Debug.LogError($"You need to assign the {nameof(_camera)}");
                return;
            }

            if (_camera.orthographic == false)
            {
                Debug.LogError($"Camera must be orthographic.");
                return;
            }

            _height = _camera.orthographicSize * 2;
            _width = _height * _camera.aspect;

            Vector3 bottomLeftLocal = new Vector3(-_width / 2, -_height / 2, _camera.nearClipPlane);
            Vector3 bottomRightLocal = new Vector3(_width / 2, -_height / 2, _camera.nearClipPlane);
            Vector3 topLeftLocal = new Vector3(-_width / 2, _height / 2, _camera.nearClipPlane);
            Vector3 topRightLocal = new Vector3(_width / 2, _height / 2, _camera.nearClipPlane);

            Vector3 bottomLeft = _camera.transform.TransformPoint(bottomLeftLocal);
            Vector3 bottomRight = _camera.transform.TransformPoint(bottomRightLocal);
            Vector3 topLeft = _camera.transform.TransformPoint(topLeftLocal);
            Vector3 topRight = _camera.transform.TransformPoint(topRightLocal);

            DrawRectangle(new[] { bottomLeft, bottomRight, topRight, topLeft }, Color.green);

            Plane plane = new Plane(Vector3.up, Vector3.zero);

            DrawSolidPlane(plane, 50);

            _projectedBottomLeft = ProjectPointOnPlane(bottomLeft, plane);
            _projectedBottomRight = ProjectPointOnPlane(bottomRight, plane);
            _projectedTopLeft = ProjectPointOnPlane(topLeft, plane);
            _projectedTopRight = ProjectPointOnPlane(topRight, plane);

            DrawRectangle(
                new[] { _projectedBottomLeft, _projectedBottomRight, _projectedTopRight, _projectedTopLeft },
                Color.red);

            DrawArea(bottomLeft, bottomRight, topLeft, topRight);
        }

        private void DrawRectangle(Vector3[] points, Color color)
        {
            if (points.Length != 4)
                return;

            Gizmos.color = color;

            for (var i = 0; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i], points[(i + 1) % points.Length]);
            }
        }

        private void DrawArea(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
        {
            Mesh mesh = new Mesh();
            _meshFilter.mesh = mesh;

            Vector3[] vertices =
            {
                bottomLeft, _projectedBottomLeft, topLeft, _projectedTopLeft,
                bottomRight, _projectedBottomRight, topRight, _projectedTopRight
            };

            int[] triangles =
            {
                0, 1, 2,
                1, 3, 2,
                2, 3, 6,
                6, 3, 7,
                4, 6, 5,
                6, 7, 5,
                4, 5, 1,
                1, 0, 4,

                2, 1, 0,
                2, 3, 1,
                6, 3, 2,
                7, 3, 6,
                5, 6, 4,
                5, 7, 6,
                1, 5, 4,
                4, 0, 1
            };

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawMesh(mesh);
        }

        private void DrawSolidPlane(Plane plane, float size)
        {
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Vector3 center = plane.ClosestPointOnPlane(Vector3.zero);
            Gizmos.DrawCube(center, new Vector3(size, 0.01f, size));
        }

        private Vector3 ProjectPointOnPlane(Vector3 point, Plane plane)
        {
            Ray ray = new Ray(point, _camera.transform.forward);

            if (plane.Raycast(ray, out var distance))
            {
                Gizmos.color = Color.magenta;
                Vector3 projectPointOnPlane = ray.GetPoint(distance);
                Gizmos.DrawSphere(projectPointOnPlane, 0.15f);
                return projectPointOnPlane;
            }

            return point;
        }
    }
}