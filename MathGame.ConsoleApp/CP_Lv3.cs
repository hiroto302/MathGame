using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  class CP_Lv3 : CP_Lv2
  {
    // CPが保持しているカードを出すメソッド
    // Player_Lv3と同様の操作をCPが出来るようにする
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
    }    {
      Random ran
    }
  }
}