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
    int n = 6;
    // フィールド(中央)に配置されていくカード
    public static List<int> fieldCard = new List<int>();
    // フィールドに現在置かれている数
    public static int fieldNum = 0;
    // nまでの数字を作成するメソッド
    public void MakeCard()
    {
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
      if(n > 0)
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


    // ゲームを実行するメソッド
    public void PlayGame(Player player1, Player cp)
    {
      int turn = 0;
      while(true)
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
            player1.DiscardCard(player1.card);
            turn = 3;
            Line();
            break;
          // CPがカードを出す場面
          case 2:

            turn = 4;
            break;
          // フィールドと手札の更新
          case 3:
            cp.ShowCard(cp);
            FieldCard(turn);
            player1.ShowCard(player1);
            if(turn == 1)
            {
              turn = 2;
            }
            Line();
            break;
        }
        if(turn == 4)
        {
          break;
        }
      }
    }
  }
}