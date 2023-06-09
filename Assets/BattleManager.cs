using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{ 
    [SerializeField] State state;
    [SerializeField] GameObject battleResult;
    [SerializeField] TMP_Text battleResultText;
    [SerializeField] Player player1;
    [SerializeField] Player player2; 


    enum State
    {
        Preparation,
        Player1Selec,
        Player2Selec,
        Attacking,
        Damaging,
        Returning,
        BattleIsOver
    }
    void Update()
    {
        switch (state)
        {
          case State.Preparation:
              player1.Prepare();
              player2.Prepare();

              player1.setPlay(true);
              player2.setPlay(false);
               state = State.Player1Selec;
                break;

          case State.Player1Selec:
                 if(player1.SelectedCharacter != null)
                  {
                   player1.setPlay(false);
                   player2.setPlay(true);
                   state = State.Player2Selec;
                  }
                break;  

          case State.Player2Selec:
                 if (player2.SelectedCharacter != null)
                  {
                    player2.setPlay(false);
                    player1.Attack();
                    player2.Attack();
                    state = State.Attacking;
                  }

                break;  
                
          case State.Attacking:
                // if(player1.IsAttacking() == false && player2.IsAttacking() == false)
                //  {
                //     CalculateBattle(player1, player2, out Player winner,out Player loser);
                //     if(loser == null)
                //     {
                //         player1.TakeDamage(player2.SelectedCharacter.AttackPower);
                //         player2.TakeDamage(player1.SelectedCharacter.AttackPower);
                //     }
                //     else
                //     {
                //         loser.TakeDamage(winner.SelectedCharacter.AttackPower);    
                //     }
                                
                //     state = State.Damaging;
                //  }

                break;

          case State.Damaging:
                // if(player1.IsDamaging() == false && player2.IsDamaging() == false)
                {
                    if(player1.SelectedCharacter.CurrentHP == 0)
                    {
                        player1.Remove(player1.SelectedCharacter);
                    }

                    if(player2.SelectedCharacter.CurrentHP == 0)
                    {
                        player2.Remove(player2.SelectedCharacter);
                    }
                   // animasi return 
                   if(player1.SelectedCharacter != null)
                        {
                            player1.Return();
                        }
                if(player2.SelectedCharacter != null)
                        {
                            player2.Return();
                        }


                   state = State.Returning; 
                }
                break; 

          case State.Returning:
                // if(player1.IsReturning() == false && player2.IsReturning()  == false)
                {
                    if (player1.CharacterList.Count == 0 && player2.CharacterList.Count == 0)
                    { 
                         battleResult.SetActive(true);
                         battleResultText.text = "Permainan Telah Berak Hire!!!\nDraw";
                        state =State.BattleIsOver;
                    }  
                    else if(player1.CharacterList.Count == 0) 
                    {
                         battleResult.SetActive(true);
                         battleResultText.text = "Permainan Telah Berak Hire!!!\nPlayer 2 Win!";
                        state =State.BattleIsOver;
                    }

                    else if(player2.CharacterList.Count == 0) 
                    {
                         battleResult.SetActive(true);
                         battleResultText.text = "Permainan Telah Berak Hire!!!\nPlayer 1 Win!";
                        state =State.BattleIsOver;
                    }
 
                   else
                   {
                    state = State.Preparation;
                   } 

                }
                break;
          case State.BattleIsOver:

                
              break;
        }
    }

    private void CalculateBattle(Player player1, Player player2, out Player winner, out Player loser)
    {
       var type1 = player1.SelectedCharacter.Type;
       var type2 = player2.SelectedCharacter.Type;

       if(type1 == CharacterType.Rock && type2 == CharacterType.Paper)
       {
            winner = player2;
            loser = player1;
       }
       else if(type1 == CharacterType.Rock && type2 == CharacterType.scissor)
       {
            winner = player1;
            loser = player2;
       }
       else if(type1 == CharacterType.Paper && type2 == CharacterType.Rock)
       {
            winner = player1;
            loser = player2;
       }
       else if(type1 == CharacterType.Paper && type2 == CharacterType.scissor)
       {
            winner = player2;
            loser = player1;
       }
       else if(type1 == CharacterType.scissor && type2 == CharacterType.Rock)
       {
            winner = player2;
            loser = player1;
       }
       else if(type1 == CharacterType.scissor && type2 == CharacterType.Paper)
       {
            winner = player1;
            loser = player2;
       }
       else
       {
            winner = null;
            loser = null;
       }
       
    }

    
    public void Replay()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }
}
