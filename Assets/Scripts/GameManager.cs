using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;
    private void Awake()
    {
        if(!instance)
            instance = this;
    }

    #endregion

    [SerializeField]
    private Material cutlineMaterial;
    [SerializeField]
    private int cutlinesAmount;
    [SerializeField]
    private float movementSpeed;

    private bool isCutlineActive;
    private string cutlineName;
    private Vector2 position;

    // DEMO
    [Header("Visual")]
    [SerializeField]
    private GameObject resultSprite;
    [SerializeField]
    private GameObject victoryPanel;

    private bool isVictory;
    private int paperPartsToFoldAmount;
    public int CurrentFoldedPaperPartsAmount { get; set; }

    private void Start()
    {
        isVictory = false;
        paperPartsToFoldAmount = 4;
        CurrentFoldedPaperPartsAmount = 0;
    }
    private void Update()
    {
        // cutlines infinity movement on OX
        for(int i = 1; i <= cutlinesAmount; i++)
        {
            cutlineName = $"Cutline{i}_";
            isCutlineActive = cutlineMaterial.GetInt(cutlineName + "IsActive") == 1 ? true : false;
            position = cutlineMaterial.GetVector(cutlineName + "Position");
            
            // check for activity
            if(!isCutlineActive)
            {
                position.x = 0;
            }
            // clamp position for infinity movement (visual)
            else
            {
                position.x += Time.deltaTime * movementSpeed;
                if (position.x > .404f)
                {
                    // .25 -> .404
                    position.x = .25f;
                }
            }

            // update material position
            cutlineMaterial.SetVector(cutlineName + "Position", position);
        }

        // check for victory
        if(!isVictory && CurrentFoldedPaperPartsAmount == paperPartsToFoldAmount)
        {
            Victory();
        }
    }

    private void Victory()
    {
        // show icon with animation .144f,
        isVictory = true;
        resultSprite.transform.localScale = new Vector3(.075f, .075f, .075f);
        Sequence iconSequence = DOTween.Sequence();
        iconSequence
            .OnStart(() => resultSprite.SetActive(true))
            .Append(resultSprite.transform.DOScale(new Vector3(.175f, .175f, .175f), .35f)).SetEase(Ease.OutBack)
            .Join(resultSprite.transform.DOMoveY(.15f, .35f)).SetEase(Ease.OutBack)
            .OnComplete(() => resultSprite.transform.DOMoveY(-2.75f, .5f).SetEase(Ease.OutBounce));

        // show victory pannel with button 'restart'
        victoryPanel.SetActive(true);
    }
    public void Restart()
    {
        // bug prevention
        Destroy(resultSprite);

        // scene reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
