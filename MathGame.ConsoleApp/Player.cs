using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  class Player
  {
    // 保持するカード 保持するカードはゲームマスターにより生成される
    // static にすることで引数にクラスのインスタンス変数を受け取らずにメソッドを作成できる　が　今回は勉強のためstaticなし
    public List<int> card;
    // プレイヤ-の名前
    private string name = "Player";
    // スキップできる回数
    public int skipNum = 2;

    // 勝敗を分ける失点ポイント
    private int point = 0;
    // 各プロパティ
    public string Name
    {
      set{name = value;}
      get{return name ;}
    }
    // プロパティを静的(static)にしたい場合、pointもstaticである必要がある
    public int Point
    {
      set{point = value;}
      get{return point;}
    }


    // 保持するカードの表示
    public virtual void ShowCard(Player player)
    {
      Console.WriteLine("playerのカード");
      foreach(int c in player.card)
      {
        Console.Write(c + " ");
      }
      Console.WriteLine();
    }

    // 保持しているカードを出すメソッド
    // 場に出ている以上のカードのみ出せるようにし、出せない場合skipする機能の追加 skipは一度のみ
    // 出したカードを手持ちから削除し、場に表示する
    public virtual void DiscardCard(List<int> playerCard)
    {
      int n;
      // センターの場に出すカード選択
      while(true)
      {
        Console.Write("場に出す数を選択 : ");
        n = int.Parse(Console.ReadLine());
        //場に出ている数以上　skip 判定
        if(n > GameMaster.fieldNum || n == 0)
        {
          break;
        }
        else
        {
          Console.WriteLine
            ("場に出ている数より大きい数を選択 or 無い場合は,「0」を入力しスキップ");
        }
      }
      // 場にカードを出せた時
      if(n > 0)
      {
        card.Remove(n);
        GameMaster.fieldCard.Add(n);
        GameMaster.nextPlay = "cp";
      }
      // 入力 0 skip
      else if(n ==0)
      {
        GameMaster.nextPlay = "playerSkip";
        skipNum --;
      }
      if(GameMaster.fieldCard.Count > 0)
      {
        Console.Write("フィールドが保持している数 : ");
        foreach(int i in GameMaster.fieldCard)
        {
          Console.Write(i + " ");
        }
        Console.WriteLine();
      }
    }
  }
}