using DG.Tweening;
using UnityEngine;

public class LoadingImageController : MonoBehaviour
{
    [SerializeField] private Transform _loadingImage;
    private void OnEnable()
    {
        _loadingImage.gameObject.SetActive(true);
        _loadingImage.DORotate(new Vector3(0, 0, -360), 2).SetLoops(10, LoopType.Restart).SetRelative().SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _loadingImage.gameObject.SetActive(false);
        _loadingImage.transform.DOKill();
    }
}
