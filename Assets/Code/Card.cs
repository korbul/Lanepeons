using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour {

    public GameObject cardMesh;
    public Texture2D image;
    
    public string cardName;

    public ActionChain onPlayActionChain;
    public ActionChain OnDestroyActionChain;

    public Player Owner { get; set; }

    public void OnCardPlay(CardPresenterController presenter, System.Action callback)
    {
        if (cardPlayed)
            return;

        cardPlayed = true;

        if (null != onPlayActionChain)
            presenter.ExecuteActionChain(onPlayActionChain, callback);
        else
        {
            if(null != callback)
                callback();
        }
    }

    public void OnCardDestroy(CardPresenterController presenter, System.Action callback)
    {
        if (null != OnDestroyActionChain)
            presenter.ExecuteActionChain(OnDestroyActionChain, callback);
        else
        {
            if (null != callback)
                callback();
        }
    }

    public void OnHoverEnter()
    {
        cardMesh.transform.DOScale(Vector3.one * 1.75f, 0.25f);
        cardMesh.transform.DOLocalMove(hoverOffset, 0.25f);
    }

    public void OnHoverExit()
    {
        cardMesh.transform.DOScale(Vector3.one, 0.25f);
        cardMesh.transform.DOLocalMove(Vector3.zero, 0.25f);
    }

    protected readonly Vector3 cardSize = new Vector3(0.5f, 0.91f, 1.0f);
    protected readonly Vector3 hoverOffset = new Vector3(0.4f, 0.75f, -0.1f);
    protected MeshRenderer meshRenderer;
    protected bool cardPlayed = false;

    void Start()
    {
        transform.localScale = cardSize;

        meshRenderer = cardMesh.GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = image;

        if (null != onPlayActionChain)
        {
            onPlayActionChain = Instantiate(onPlayActionChain);
            onPlayActionChain.SetCardReference(this);
        }
        if (null != OnDestroyActionChain)
        {
            OnDestroyActionChain = Instantiate(OnDestroyActionChain);
            OnDestroyActionChain.SetCardReference(this);
        }
    }
}
