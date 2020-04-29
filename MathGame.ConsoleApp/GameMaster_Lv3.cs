using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  // Lv３のゲームを実行するクラス
  // 1~10のカードを2枚ずつ作成
  // 6枚ずつ配布する
  // 同じ数は同時に出すことが出来るようにする
  class GameMaster_Lv3 : GameMaster_Lv2
  {
    // 1~10のカードを各2枚ずつ生成 合計20枚
    public override void MakeCard(int n)
    {
      this.n = n;
      number = new List<int>();
      for(int i = 0; i < 2; i++)
      {
        for(int j = 0; j < n; j++)
        {
          number.Add(j + 1);
        }
      }
    }
    //20枚のカードを6枚ずつ配布
    public void DistributeCard(Player_Lv3 player, CP_Lv3 cp, int m)
    {
      Random random = new Random();
      // 保持するカード
      player.card = new List<int>();
      cp.card = new List<int>();
      // Player一人あたり持つ手札の数 6枚
      int cardNum = m;
      // playerへの配布
      while(true)
      {
        int r = random.Next(n * 2); // 0~19の数をランダムに r に代入
        // 配布カード重複防止
        if(number[r] != 0)
        {
          player.card.Add(number[r]);
          number[r] = 0;
          cardNum --;
          if(cardNum == 0 )
          {
            cardNum = m; //cpでも利用するので初期化 リセット
            break;
          }
        }
      }
      // cpへの配布
      while(true)
      {
        int r = random.Next(n * 2); // 0~19の数をランダムに r に代入
        // 配布カード重複防止
        if(number[r] != 0)
        {
          cp.card.Add(number[r]);
          number[r] = 0;
          cardNum --;
          if(cardNum == 0 )
          {
            cardNum = 6; //初期化 リセット
            break;
          }
        }
      }
    }
    // Lv3のゲームを実行するメソッド
    public void PlayGame(Player_Lv3 player, CP_Lv3 cp)
    {
      turn = 0;
      playGame = true;
      while(playGame)
      {
        switch(turn)
        {
          // ゲーム開始場面
          case 0:
            cp.ShowCard();
            FieldCard(turn);
            player.ShowCard();
            turn = 1;
            Line();
            break;
          // Playerがカードを出す場面
          case 1:
            TurnName(player);
            player.DiscardCard();
            turn = 3;
            break;
          // CPがカードを出す場面
          case 2:
            cp.ThinkingTime(2);
            cp.DiscardCard();
            turn = 3;
            break;
          // フィールドと手札の更新 と　その後の処理
          case 3:
            // ゲーム終了判定
            // どちらかの手札が0枚になった時点でゲーム終了
            // 両方とも出すことが出来ない状態の時
            if(player.card.Count == 0 || cp.card.Count == 0 ||
                player.skipNum == 0 || cp.skipNum == 0)
            {
              nextPlay = "finish";
            }
            // 条件分岐でどのplayerがプレーするか判断
            if(nextPlay.Equals("player"))
            {
              turn = 1;
            }
            else if(nextPlay.Equals("cp"))
            {
              turn = 2;
            }
            else if(nextPlay.Equals("playerSkip")) // プレイヤーがskipを利用した時の処理
            {
              if(fieldCard.Count > 0)
              {
                player.Skip(fieldCard.Count);
              }
              FieldReset();
              turn = 2;
            }
            else if(nextPlay.Equals("cpSkip"))     // CPがskipを利用した時
            {
              if(fieldCard.Count > 0)
              {
                cp.Skip(fieldCard.Count);
              }
              FieldReset();
              turn = 1;
            }
            else if(nextPlay.Equals("finish"))
            {
              turn = 4;
            }
            if(fieldCard.Count > 0)
            {
              CountFieldCard();
            }
            Line();
            cp.ShowCard();
            FieldCard(turn);
            player.ShowCard();
            Line();
            break;
          case 4:
            // playGame = false;
            FinishGame();
            break;
        }
      }
      GameResult(player, cp);
      Reset(player, cp);
    }
  }
}