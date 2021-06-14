using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField]
    private PaperPart paperPart;

    public void OnClick()
    {
        paperPart.DoFold();
    }
}
