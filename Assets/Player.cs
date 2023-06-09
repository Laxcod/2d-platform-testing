// using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
  [SerializeField] Character selectedCharacter; 
  [SerializeField] Transform atkRef;
  [SerializeField] bool isBot;
  [SerializeField] List<Character> characterList; 
  [SerializeField] UnityEvent onTakeDamage;

    public Character SelectedCharacter { get => selectedCharacter;}
    public List<Character> CharacterList { get => characterList;  }
  private void Start()
  {
      if(isBot)
      {
        foreach (var character in characterList)
        {
         character.Button.interactable = false;
        }
      }
  }


    public void Prepare()
    {
      selectedCharacter = null;
    }
    public void selectCharacter(Character character)
    {
      selectedCharacter = character;
    }


    public void setPlay(bool value)
    {
        if(isBot)
        {
            List<Character> lotteryList = new List<Character>();
            foreach (var character in characterList)
            {
              int ticket  =Mathf.CeilToInt ( (float) character.CurrentHP/ (float) character.MaxHP) * 10;
              for (int i = 0; i < ticket; i++)
              {
                lotteryList.Add(character);
              }
            }

            int index = Random.Range(0,lotteryList.Count);
            selectedCharacter = lotteryList[index];

        }
        else
        {
        foreach (var character in characterList)
        {
         character.Button.interactable = value;
        }
        }
       
    }


    public void Attack()
    {
        // selectedCharacter.transform.DOMove(atkRef.position,0.5f);
        
    }

    // public bool IsAttacking()
    // {
    //   if(selectedCharacter == null)
    //      return false;
    //     // return DOTween.IsTweening(selectedCharacter.transform);
    // }

    public void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend =  selectedCharacter.GetComponent<SpriteRenderer>();
        // spriteRend.DOColor(Color.red, 0.1f).SetLoops(6,LoopType.Yoyo);
        onTakeDamage.Invoke();
    } 

    // public bool IsDamaging()
    // {
    //   if(selectedCharacter == null)
    //      return false;
    //   var spriteRend =  selectedCharacter.GetComponent<SpriteRenderer>();
    //   // return DOTween.IsTweening(spriteRend);
    // }

    public void Remove(Character character)
    {
        if(characterList.Contains(character) == false)
          return;

        if(selectedCharacter == character)
              selectedCharacter = null;

        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }

    public void Return()
    {
        //  selectedCharacter.transform.DOMove(selectedCharacter.InitialPosition,0.5f);
    }

    // public bool IsReturning()
    // {
    //   if(selectedCharacter == null)
    //      return false;

    //   // return DOTween.IsTweening(selectedCharacter.transform);
    // }
}
