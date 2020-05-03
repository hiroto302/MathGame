using System;
using System.Collections.Generic;

// Lv4のゲームを実行するクラス
// 1~10のカードをそれぞれ、各枚数作成　作成したカードを山札とする
// 6枚ずつ配布する
// 同じ数は同時に出すことが出来る
// 場に数を出した後、手札が6枚になるように戻す
// 出すことが出来かった場合、現時点に場にある札の枚数が失点に追加されていく.場の数は0にリセット。
// 山札が0枚になった後も、どちらかのplayerの手札が0枚になるまでゲームを続ける
namespace MathGame.ConsoleApp
{
  class GameMaster_Lv4 : GameMaster_Lv3
  {
    // カードを作成するメソッド // 引数に10を与える
    public override void MakeCard(int n)
    {
      number = new List<int>();
      for(int i = 1; i < n + 1; i++)
      {
        switch(i)
        {
          case 1:
            MakeNum(2, i, number);
            break;
          case 2:
            MakeNum(3, i, number);
            break;
          case 3:
            MakeNum(3, i, number);
            break;
          case 4:
            MakeNum(3, i, number);
            break;
          case 5:
            MakeNum(4, i, number);
            break;
          case 6:
            MakeNum(3, i, number);
            break;
          case 7:
            MakeNum(2, i, number);
            break;
          case 8:
            MakeNum(2, i, number);
            break;
          case 9:
            MakeNum(1, i, number);
            break;
          case 10:
            MakeNum(1, i, number);
            break;
          // 計24枚
        }
      }
      // foreach (int i in number)
      // {
      //   Console.Write(i + "");
      // }
      // Console.WriteLine(number.Count + "枚");
    }
    // ある数のカードを作る枚数
    // 第一引数 : 作る枚数, 第二引数: 作るカードの値, 第三引数 : 作成したカード追加する山札
    protected void MakeNum(int n, int m,List<int> number)
    {
      for(int i = 0; i < n; i++)
      {
        number.Add(m);
      }
    }
    // カードの配布
    public void DistributeCard(Player_Lv4 player, CP_Lv4 cp, List<int> number)
    {
      // 各プレイヤーが保持するカード
      player.card = new List<int>();
      cp.card = new List<int>();
      DistributionTarget(player.card);
      DistributionTarget(cp.card);
      // Console.WriteLine(number.Count + "マイ");
    }
    // 配布する対象にカードを配る
    protected void DistributionTarget(List<int> card)
    {
      Random random = new Random();
      // 一人あたり持つ手札の数
      int cardNum = 6;
      // 引数への配布
      while(true)
      {
        // 山札が0枚でなければ下記を実行
        if(number != null)
        {
          int r = random.Next(number.Count);
          card.Add(number[r]);
          cardNum --;
          // 配布したカードを山札から削除
          number.RemoveAt(r);
          if(cardNum == 0)
          {
            cardNum = 6;
            break;
          }
        }
      }
      // foreach(int i in card)
      // {
      //   Console.Write(i + " ");
      // }
      // Console.WriteLine();
      // foreach(int i in number)
      // {
      //   Console.Write(i + " ");
      // }
      // Console.WriteLine();
    }
    // Lv4のゲームを実行するメソッド
    // 場に出すことが出来なかった時、場が保持しているカードを失点に追加し、場の数を０にリセットする。
    // そして、改めて最初のplayerとして自由な数からだす
    public void PlayGame(Player_Lv4 player, CP_Lv4 cp)
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
            player.Draw(number);
            turn = 3;
            break;
          // CPがカードを出す場面
          case 2:
            cp.ThinkingTime(1);
            cp.DiscardCard();
            cp.Draw(number);
            turn = 3;
            break;
          // フィールドと手札の更新 と　その後の処理
          case 3:
            // ゲーム終了判定
            // どちらかの手札が0枚になった時点でゲーム終了
            if(player.card.Count == 0 || cp.card.Count == 0)
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
            else if(nextPlay.Equals("playerRestart")) // プレイヤーが場に出さないことを選択した時の処理
            {
              if(fieldCard.Count > 0)
              {
                player.AddPoint(fieldCard.Count);
              }
              FieldReset();
              turn = 1; // 再び自分のターン
            }
            else if(nextPlay.Equals("cpRestart"))     // CPが場に出さないこと選択した時の処理
            {
              if(fieldCard.Count > 0)
              {
                cp.AddPoint(fieldCard.Count);
              }
              FieldReset();
              turn = 2; // 再び相手のターン
            }
            else if(nextPlay.Equals("finish"))
            {
              turn = 4;
            }
            if(fieldCard.Count > 0)
            {
              // 場が保持しているカードの表示
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