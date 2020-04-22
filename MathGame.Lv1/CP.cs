using System;
using System.Collections.Generic;
using System.Diagnostics; //Stopwatchクラスの利用
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


// Playerクラスを継承したコンピュータクラス
namespace MathGame.Lv1
{
  class CP : Player
  {
    Stopwatch stopWatch = new Stopwatch();
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
    // CPが処理を実行にかける時間
    public void ThinkingTime(int second)
    {
      // 思考時間 ３秒 (３秒間sleepにするか時間を計測する方法)
      // Thread.Sleep(3000);
      Console.WriteLine("思考中....");
      stopWatch.Start();
      TimeSpan ts = stopWatch.Elapsed;
      while(ts.Seconds < second)
      {
        ts  = stopWatch.Elapsed;
      }
      stopWatch.Stop();
      stopWatch.Reset();
    }

    // CPが保持しているカードを出すメソッド // 引数に参照したいListを追加
    public override void DiscardCard(List<int> cpCard)
    {
      // センターの場に出すカード選択
      Random random = new Random();
      int n = 0;
      int num = 0;
      while(true)
      {
        if(card.Count > 0)
        {
          n = random.Next(card.Count - 1);
          Console.WriteLine("ランダムで生成された数" + n);
          num = card.Find(i => i == card[n]);
          Console.WriteLine("cpが選択した数" + num);
        }
        break;
      }
      card.Remove(num);
      GameMaster.fieldCard.Add(num);
      Console.Write("フィールドが保持している数 : ");
      foreach(int i in GameMaster.fieldCard)
      {
        Console.Write(i + " ,");
      }
      Console.WriteLine();
      GameMaster.nextPlay = "player";
    }
  }
}