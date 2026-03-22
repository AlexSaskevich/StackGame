using UnityEngine;

namespace Source.Code.Gameplay.Camera.OrthographicCameraProjection.Common
{
    public interface IOrthographicCameraProjectionProvider
    {
        Vector3 GetProjectedBottomLeft();
        Vector3 GetProjectedBottomRight();
        Vector3 GetProjectedTopLeft();
        Vector3 GetProjectedTopRight();
    }
}