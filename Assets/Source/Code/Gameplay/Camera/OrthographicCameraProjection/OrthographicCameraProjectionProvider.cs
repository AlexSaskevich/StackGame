using System;
using Source.Code.Gameplay.Camera.Common;
using Source.Code.Gameplay.Camera.OrthographicCameraProjection.Common;
using UnityEngine;

namespace Source.Code.Gameplay.Camera.OrthographicCameraProjection
{
    public class OrthographicCameraProjectionProvider : IOrthographicCameraProjectionProvider
    {
        private readonly UnityEngine.Camera _camera;
        private readonly Plane _plane;

        private float Height => _camera.orthographicSize * 2f;
        private float Width => _camera.orthographicSize * 2f * _camera.aspect;

        public OrthographicCameraProjectionProvider(UnityEngine.Camera camera)
        {
            if (camera.orthographic == false)
                throw new Exception("Camera must be orthographic.");

            _camera = camera;
            _plane = new Plane(Vector3.up, Vector3.zero);
        }

        public Vector3 GetProjectedBottomLeft() => GetProjectedCorner(CameraCorner.BottomLeft);

        public Vector3 GetProjectedBottomRight() => GetProjectedCorner(CameraCorner.BottomRight);

        public Vector3 GetProjectedTopLeft() => GetProjectedCorner(CameraCorner.TopLeft);

        public Vector3 GetProjectedTopRight() => GetProjectedCorner(CameraCorner.TopRight);

        private Vector3 GetProjectedCorner(CameraCorner corner)
        {
            Vector3 localPosition = corner switch
            {
                CameraCorner.BottomLeft => new Vector3(-Width / 2, -Height / 2, _camera.nearClipPlane),
                CameraCorner.BottomRight => new Vector3(Width / 2, -Height / 2, _camera.nearClipPlane),
                CameraCorner.TopLeft => new Vector3(-Width / 2, Height / 2, _camera.nearClipPlane),
                CameraCorner.TopRight => new Vector3(Width / 2, Height / 2, _camera.nearClipPlane),
                _ => throw new ArgumentOutOfRangeException(nameof(corner), corner, null)
            };

            Vector3 worldPosition = _camera.transform.TransformPoint(localPosition);
            return ProjectPointOnPlane(worldPosition, _plane);
        }

        private Vector3 ProjectPointOnPlane(Vector3 point, Plane plane)
        {
            Ray ray = new Ray(point, _camera.transform.forward);

            if (plane.Raycast(ray, out var distance))
            {
                Vector3 projectPointOnPlane = ray.GetPoint(distance);
                return projectPointOnPlane;
            }

            return point;
        }
    }
}