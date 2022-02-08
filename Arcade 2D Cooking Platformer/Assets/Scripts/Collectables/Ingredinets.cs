using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredinets : MonoBehaviour
{
    [SerializeField] private int ingredinetImagesActiveIndex;

    [SerializeField] private GameObject imageGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int tempFoundIngredinetsNum = 1;
            GameManager.instance.SetFoundIngredinetsNum(GameManager.instance.GetFoundIngredinetsNum() + tempFoundIngredinetsNum);

            PointManger.instance.SetTolatPoints(PointManger.instance.GetTolatPoints() + PointManger.instance.GetPointData().GetIngredientPointNum());
            
            Debug.Log("found one!");
            //play the sound
            FindObjectOfType<AudioManager>().playAudio("getitem");

            //Set UI
            LevelObjectiveCakeIngredientsUI.instance.ActiveImage(imageGameObject, true, ingredinetImagesActiveIndex);

            gameObject.SetActive(false);
        }
    }
}
