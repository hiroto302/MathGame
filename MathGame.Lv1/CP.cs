using System;

// Playerクラスを継承したコンピュータクラス
namespace MathGame.Lv1
{
  class CP : Player
  {
    // 保持するカードの表示
    public override void ShowCard(Player cp)
    {
      Console.WriteLine("相手のカード");
      for(int i = 0; i < cp.card.Count; i++)
      {
        Console.Write("?" + " ");
      }
      Console.WriteLine();
    }
  }
}