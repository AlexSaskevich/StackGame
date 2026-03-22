using UnityEngine;

namespace Source.Code.Gameplay.Camera.Common
{
    public interface ICameraSystem
    {
        void MoveCamera(Vector3 targetPosition);
    }
}