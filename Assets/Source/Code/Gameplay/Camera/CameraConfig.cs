using DG.Tweening;
using UnityEngine;
using Constants = Source.Code.Infrastructure.Constants;

namespace Source.Code.Gameplay.Camera
{
    [CreateAssetMenu(menuName = Constants.Configs + nameof(CameraConfig), fileName = nameof(CameraConfig), order = 0)]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] public Ease MoveEase { get; private set; }
        [field: SerializeField] public float TopScreenOffset { get; private set; }
        [field: SerializeField] public float StartFollowBlockY { get; private set; }
    }
}