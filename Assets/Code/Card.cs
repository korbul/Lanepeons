using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour {

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
        preHoverZ = transform.position.z;
        transform.DOScale(cardSize * 1.1f, 0.5f);
        transform.DOMoveZ(-20, 0);
    }

    public void OnHoverExit()
    {
        transform.DOScale(cardSize, 0.5f);
        transform.DOMoveZ(preHoverZ, 0);
    }

    protected readonly Vector3 cardSize = new Vector3(154, 280, 1);
    protected MeshRenderer meshRenderer;
    protected bool cardPlayed = false;

    private float preHoverZ;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
