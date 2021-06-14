using UnityEngine;
using DG.Tweening;

public class PaperPart : MonoBehaviour
{
    [SerializeField]
    private Vector3 unfoldStateAmount;

    private bool isFolded;
    private float animDuration;

    private void Start()
    {
        isFolded = false;
        animDuration = .075f;
    }

    public void DoFold()
    {
        Quaternion rotation = new Quaternion();
        if (isFolded)
        {
            rotation.eulerAngles = unfoldStateAmount;
            transform
                .DORotateQuaternion(rotation, animDuration)
                .OnComplete(() => 
                {
                    isFolded = !isFolded;
                    GameManager.instance.CurrentFoldedPaperPartsAmount--;
                });
        }
        else
        {
            // TODO: fix wrong angle rotation
            rotation.eulerAngles = Vector3.zero;
            transform
                .DORotateQuaternion(rotation, animDuration)
                .OnComplete(() =>
                {
                    isFolded = !isFolded;
                    GameManager.instance.CurrentFoldedPaperPartsAmount++;
                });
        }
    }
}
