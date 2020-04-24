using System;
using System.Collections.Generic;

// Numberクラス
// ゲームの進行を管理するクラス
namespace MathGame.Lv1
{
  class GameMaster
  {
    // 生成する数字カードを格納
    public static List<int> number;
    // 作成する枚数 今回は6までの数
    int n ;
    // フィールド(中央)に配置されていくカード
    public static List<int> fieldCard = new List<int>();
    // フィールドの一番上に現在置かれている数
    public static int fieldNum = 0;
    // case3に置いて、次の処理判の断を行う文字列の変数
    public static string nextPlay = "";
    // nまでの数字を作成するメソッド
    public void MakeCard(int n)
    {
      this.n = n;
      number = new List<int>();
      for(int i = 0; i < n; i++)
      {
        number.Add(i + 1);
      }
    }
    // 数字をランダムに半分に配るメソッド 2人を想定
    public void DistributeCard(Player player1, Player cp)
    {
      Random random = new Random();
      // player1とcpが保持するカード
      player1.card = new List<int>();
      cp.card = new List<int>();

      // Player一人あたりが持つ手札の数 カード数 / Player人数
      int cardNum = n / 2;
      while(true)
      {
        int r = random.Next(n);
        // 配布カードの重複防止
        if(number[r] != 0)
        {
          player1.card.Add(number[r]);
          number[r] = 0;
          cardNum--;
          if(cardNum == 0)
          {
            break;
          }
        }
      }
      // cpが持つ手札 playerに配布した以外のカード
      for(int i = 0; i < number.Count; i++)
      {
        if(number[i] != 0)
        {
          cp.card.Add(number[i]);
        }
      }
    }

    // フィールド 中央のカード置き場所
    static void FieldCard(int n)
    {
      if(n > 0 && fieldCard.Count > 0)
      {
        // 現在フィールドにある数の表示
        int topNum = fieldCard.Count;
        fieldNum = fieldCard[topNum - 1];
        Console.WriteLine("場の数　{0}", fieldNum);
      }
      else
      {
        Console.WriteLine("場の数　{0}", fieldNum);
      }
    }

    // 線
    public static void Line()
    {
      Console.WriteLine("-------------------------");
    }

    // 誰のターンか表示
    public void TurnName(Player player)
    {
      Console.WriteLine("{0}のターン", player.Name);
    }
    // ゲームの終了
    void FinishGame()
    {
      Console.WriteLine("ゲーム終了");
      playGame = false;
    }

    // ゲーム結果の表示
    void GameResult(Player player1, CP cp)
    {
      Console.WriteLine("-------- 結果 --------");
      player1.Point = player1.card.Count;
      cp.Point = cp.card.Count;
      if(player1.Point < cp.Point)
      {
        Console.WriteLine("{0}の勝ち!!!", player1.Name);
      }
      else if(player1.Point > cp.Point)
      {
        Console.WriteLine("CPの勝ち!!!");
      }
      else if(player1.Point == cp.Point)
      {
        Console.WriteLine("引き分け....");
      }
      Console.WriteLine("{0}の失点 : {1}", player1.Name, player1.Point);
      Console.WriteLine("CPの失点 : {0}", cp.Point);
    }

    // ゲームを実行するメソッド
    public static int turn = 0;
    public bool playGame = true;
    public void PlayGame(Player player1, CP cp)
    {
      while(playGame)
      {
        switch(turn)
        {
          // ゲーム開始場面
          case 0:
            cp.ShowCard(cp);
            FieldCard(turn);
            player1.ShowCard(player1);
            turn = 1;
            Line();
            break;
          // Playerがカードを出す場面
          case 1:
            TurnName(player1);
            player1.DiscardCard(player1.card);
            turn = 3;
            Line();
            break;
          // CPがカードを出す場面
          case 2:
            cp.ThinkingTime(3);
            cp.DiscardCard(cp.card);
            turn = 3;
            Line();
            break;
          // フィールドと手札の更新 と　その後の処理
          case 3:
            cp.ShowCard(cp);
            FieldCard(turn);
            player1.ShowCard(player1);
            // ゲーム終了判定
            // どちらかの手札が0枚になった時点でゲーム終了
            // 両方とも出すことが出来ない状態の時
            if(player1.card.Count == 0 || cp.card.Count == 0 ||
                player1.skipNum == 0 || cp.skipNum == 0)
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
              turn = 2;
            }
            else if(nextPlay.Equals("cpSkip"))     // CPがskipを利用した時
            {
              turn = 1;
            }
            else if(nextPlay.Equals("finish"))
            {
              turn = 4;
            }
            Line();
            break;
          case 4:
            // playGame = false;
            FinishGame();
            break;
        }
      }
      GameResult(player1, cp);
    }
  }
}