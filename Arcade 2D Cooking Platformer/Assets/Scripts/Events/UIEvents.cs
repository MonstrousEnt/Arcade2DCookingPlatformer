using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour
{
	//Class Variables
	public static UIEvents instance { private set; get; }

	//Unity Events
	public UnityEvent<int> onSetCollectableText;
	public UnityEvent<int> onSetLivesText;
	public UnityEvent<bool, int> onActiveIngredientImage;
	public UnityEvent<int, int> onUpdateHealthBar;

    //Class Methods
	public void SetCollectableText(int coinNum) { instance.onSetCollectableText.Invoke(coinNum); }
    public void ActiveIngredientImage(bool flag, int ingredinetImagesActiveIndex) { instance.onActiveIngredientImage.Invoke(flag, ingredinetImagesActiveIndex); }
    public void SetLivesText(int liveCount) { instance.onSetLivesText.Invoke(liveCount); }

    private void Awake()
    {
        #region Singleton Reference
        //---Make sure there is only one instance of this class for each Scene.

        //If there is no instance of the object
        if (instance == null)
        {
            //Set an instance of it
            instance = this;
        }

        //Else, if there's already an instance
        else
        {
            //Destroy it
            Destroy(gameObject);
            return;
        }

        //This won't get destroy when you switch scene
        DontDestroyOnLoad(gameObject);
        #endregion
    }

}
