using System;
using DG.Tweening;
using Source.Code.EventBus.Common;
using Source.Code.Gameplay.Blocks.Signals;
using Source.Code.Gameplay.Camera.Common;
using UnityEngine;
using Zenject;

namespace Source.Code.Gameplay.Camera
{
    public class CameraSystem : ICameraSystem, IDisposable
    {
        private readonly UnityEngine.Camera _mainCamera;
        private readonly IEventBus _eventBus;
        private readonly CameraConfig _config;
        private readonly float _startY;

        private Tween _moveTween;

        [Inject]
        public CameraSystem(UnityEngine.Camera mainCamera, IEventBus eventBus, CameraConfig config)
        {
            _mainCamera = mainCamera;
            _config = config;
            _startY = _mainCamera.transform.position.y;

            _eventBus = eventBus;
            _eventBus.Subscribe<BlockSpawnedSignal>(OnBlockSpawned);
        }

        public void Dispose()
        {
            _moveTween?.Kill();
            _eventBus.Unsubscribe<BlockSpawnedSignal>(OnBlockSpawned);
        }

        public void MoveCamera(Vector3 targetPosition)
        {
            throw new NotImplementedException();
        }

        private void OnBlockSpawned(BlockSpawnedSignal signal)
        {
            MoveCamera(signal.Position);
        }
    }
}