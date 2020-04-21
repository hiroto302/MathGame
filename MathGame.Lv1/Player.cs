using System;
using System.Collections.Generic;

namespace MathGame.Lv1
{
  class Player
  {
    // 保持するカード 保持するカードはゲームマスターにより生成される
    public List<int> card;

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
    // 出したカードを手持ちから削除し、場に表示する
    public virtual void DiscardCard(List<int> playerCard)
    {
      // センターの場に出すカード選択
      Console.Write("場に出す数を選択 : ");
      int n = int.Parse(Console.ReadLine());
      card.Remove(n);
      GameMaster.fieldCard.Add(n);
      foreach(int i in GameMaster.fieldCard)
      {
        Console.Write("フィールドが保持している数");
        Console.WriteLine(i);
      }
    }
  }
}