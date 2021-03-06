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

      // 同等のカードを扱う変数群
      int sameNum = 0;
      bool sameNumber = false;                  // 同等のカードがある時, true
      int yesOrNo;
      List<int> sameNumbers = new List<int>();  // 同じカードの値がある値を格納する

      // 手札に同じ値のカードが複数あるか
      for(int i = 0; i < card.Count; i++)
      {
        sameNum = card[i];
        card.RemoveAt(i);
        for(int j = 0; j < card.Count; j++)
        {
          // 同等のカードがあるか判定
          if(card[j] == sameNum)
          {
            // sameNumber = true;
            if(sameNumbers.Contains(sameNum) == false)
            {
              sameNumbers.Add(sameNum);
            }
          }
        }
        card.Insert(i, sameNum);
      }

      // 場に出す以上の数が手札にあるか
      int m = 0;
      while(m < card.Count)
      {
        if(card[m] > GameMaster.fieldNum)
        {
          // Console.WriteLine("{0} > {1}", card[m], GameMaster.fieldNum);
          discard = true;
          Console.WriteLine("場に出せる数があるよ");
          break;
        }
        else if(card[m] <= GameMaster.fieldNum) // 場の数「以下」ならm++ (8 < 8 のような時,場に出すことが出来ない時無限ループになる)
        {
          m++;
        }
      }
      // sameNumbersの数がある時、その２倍した数が場の数を越した時もdiscardをtrueとする
      if(sameNumbers.Count > 0)
      {
        for(int i = 0; i < sameNumbers.Count; i++)
        {
          if(sameNumbers[i] * 2 > GameMaster.fieldNum)
          {
            discard = true;
            Console.WriteLine("場に、数をを同時に出せば置くことが出来るよ");
          }
        }
      }
      // 場に出せる数がある時
      if(discard == true)
      {
        while(true)
        {
          n = random.Next(card.Count);
          num = card.Find(i => i == card[n]);
          // 選択した数がsameNumbersに含まれる数が
          if(sameNumbers.Contains(num))
          {
            sameNumber = true;
            Console.WriteLine("sameNumber == trueだよ");
          }
          // sameNumbersの時、1枚のみでも場に出せる時、1枚のみ or 2枚 同時に出すか 判定処理
          if(sameNumber == true && num > GameMaster.fieldNum)
          {
            yesOrNo = random.Next(1);
            // 同時に出す
            if(yesOrNo == 0)
            {
              DiscardNum(2, num);
              GameMaster.nextPlay = "player";
              break;
            }
            // 1枚のみ出す
            else if(yesOrNo == 1)
            {
              DiscardNum(1, num);
              GameMaster.nextPlay = "player";
              break;
            }
          }
          // sameNumbersの時、2枚同時に出せば場に出すことが出来る時の判定処理
          else if(sameNumber == true && num * 2 > GameMaster.fieldNum)
          {
            DiscardNum(2, num);
            GameMaster.nextPlay = "player";
            break;
          }
          // radomに選択した数が場の数以上のカードを選択した時
          else if(num > GameMaster.fieldNum)
          {
            DiscardNum(1, num);
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

    // 場に出すカードの数 手札から出した数(第二引数)を場が保持するカードに加えるメソッド
    protected virtual void DiscardNum(int n, int num)
    {
      switch(n)
      {
        case 1:
          Console.WriteLine("{0}が選択した数 : {1}", Name, num);
          break;
        case 2:
          Console.WriteLine("{0}が選択した数を同時に出した : {1}, {2}", Name, num, num);
          break;
      }
      for(int i = 0; i < n; i++)
      {
        card.Remove(num);
        GameMaster.fieldCard.Add(num);
      }
    }
  }
}