using System;
using System.Collections.Generic;
using System.Diagnostics; //Stopwatchクラスの利用
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


// Playerクラスを継承したコンピュータクラス
namespace MathGame.ConsoleApp
{
  class CP : Player
  {
    public CP() : base("相手")
    {
      // コンストラクタ CPのnameを、相手とする
    }
    Stopwatch stopWatch = new Stopwatch();
    // 保持するカードの表示
    // public override void ShowCard(Player cp)
    public override void ShowCard()
    {
      Console.WriteLine("{0}のカード", Name);
      for(int i = 0; i < card.Count; i++)
      {
        Console.Write("|?|" + " ");
        // Console.Write("|{0}| ",card[i]);
      }
      Console.WriteLine();
    }
    // CPが処理を実行にかける時間
    public void ThinkingTime(int second)
    {
      // 思考時間 ３秒 (３秒間sleepにするか時間を計測する方法)
      // Thread.Sleep(3000);
      Console.WriteLine("{0}が思考中....", Name);
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
    // public override void DiscardCard(List<int> cpCard)
    public override void DiscardCard()
    {
      // 場に出すカード選択
      Random random = new Random();
      int n = 0;
      int num = 0;
      bool discard = false;
      // 場に出す以上の数が手札にあるか
      int m = 0;
      while(m < card.Count)
      {
        if(card[m] > GameMaster.fieldNum)
        {
          // Console.WriteLine("{0} > {1}", card[m], GameMaster.fieldNum);
          discard = true;
          break;
        }
        else if(card[m] < GameMaster.fieldNum)
        {
          m++;
        }
      }
      // 場に出せる数がある時
      if(discard == true)
      {
        while(true)
        {
          n = random.Next(card.Count);
          num = card.Find(i => i == card[n]);
          // radomに選択した数が場の数以上のカードを選択した時
          if(num > GameMaster.fieldNum)
          {
            Console.WriteLine("{0}が選択した数 : {1}", Name, num);
            card.Remove(num);
            GameMaster.fieldCard.Add(num);
            GameMaster.nextPlay = "player";
            break;
          }
        }
      }
      // 場に出せる数が無い時
      else if(discard == false)
      {
        Console.WriteLine("{0}はスキップした", Name);
        GameMaster.nextPlay = "cpSkip";
        skipNum --;
      }
    }
  }
}