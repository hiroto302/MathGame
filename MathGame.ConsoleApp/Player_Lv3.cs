using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  class Player_Lv3 : Player_Lv2
  {
    // 保持しているカードを出すメソッド
    // 場の値以上のカードのみ出すこが可能、出せない場合はskip
    // 出したカードを手持ちから削除し、場に表示する
    // new 機能
    // 同じ値のカードは同時に出すことが出来る、ただし、その合計の値が場の数値以上であれ必要がある。
    // そして、新たに場に表示される値は合計値ではなく一枚あたりの数字のみ表示する　
    public override void DiscardCard()
    {
      int n;
      bool discard = false; // 場に出すことが出来るか判定のフラグ
      int num;
      bool sameNumber = false; // 同じ値の数を保持しているか判定フラグ
      // int sameNum = 0;
      int yesOrNo;
      List<int> sameNumbers = new List<int>(); // 同じ値の数値を格納

      // 手札に同じ値のカードが複数あるか
      for(int i = 0; i < card.Count; i++)
      {
        // i 番目の数を取り出す 1~6
        num = card[i];
        // Console.WriteLine("{0}番目の数 : card[{1}] = {2}", i, i, card[i]);
        card.RemoveAt(i);
        // 同等のカードが手札にあるか判定
        for(int j = 0; j < card.Count; j++)
        {
          if(card[j] == num)
          {
            sameNumber = true;
            // sameNum = num;
            // 同等のカードを複数持つことを考慮する
            if(sameNumbers.Contains(num) == false)
            {
              sameNumbers.Add(num);
            }
          }
        }
        // 取り出した値を戻す
        card.Insert(i, num);
      }

      // センターの場に出すカード選択
      while(true)
      {
        Console.Write("場に出す数を選択 : ");
        n = int.Parse(Console.ReadLine());
        //選択した値が手札にあるか skipを選択したか
        for(int i = 0; i < card.Count; i++)
        {
          if(card[i] == n || n == 0)
          {
            discard = true;
          }
        }
        // 選択した値が手札にある時
        if(discard)
        {
          //変数のリセット
          //場に出ている数以上　skip 判定
          if(n > GameMaster.fieldNum || n == 0 || (discard == true && sameNumbers.Find(i => i == n) * 2 > GameMaster.fieldNum))
          {
            discard = false;
            break;
          }
          else
          {
            discard = false;
            Console.WriteLine("場に出ている数より大きい数を選択 or 無い場合は,「0」を入力しスキップ");
          }
        }
        else if( discard == false)
        {
          Console.WriteLine("手札にあるカードを選択せよ");
        }
      }
      // 場にカードを出せた時
      Console.WriteLine(n);
      if(n > 0)
      {
        // 同じ値がある時、その値を同時に出すか
        // if(sameNumber == true && n == sameNum)
        // if(n == sameNum)
        Console.WriteLine(sameNumbers.Find(i => i == n));
        if(sameNumbers.Contains(n))
        {
          if( n > GameMaster.fieldNum)
          {
            Console.WriteLine("同じ値のカードを同時に出しますか? Yes : 0, No : 1");
            Console.Write("値を入力 : ");
            yesOrNo = int.Parse(Console.ReadLine());
            sameNumber = false; //リセット
            switch(yesOrNo)
            {
              case 0:
                // 2つの n を削除 & フィールドカードに追加
                for(int i = 0; i < 2; i++)
                {
                  card.Remove(n);
                  GameMaster.fieldCard.Add(n);
                }
                GameMaster.nextPlay = "cp";
                break;
              case 1:
                card.Remove(n);
                GameMaster.fieldCard.Add(n);
                GameMaster.nextPlay = "cp";
                break;
            }
          }
          else if(n < GameMaster.fieldNum)
          {
            Console.WriteLine("同等のカードを同時に出します");
            for(int i = 0; i < 2; i++)
            {
              card.Remove(n);
              GameMaster.fieldCard.Add(n);
            }
            GameMaster.nextPlay = "cp";
          }
        }
        // 同じ値がない時
        else
        {
          card.Remove(n);
          GameMaster.fieldCard.Add(n);
          GameMaster.nextPlay = "cp";
        }
      }
      // 入力 0 skip
      else if(n == 0)
      {
        Console.WriteLine("スキップします");
        GameMaster.nextPlay = "playerSkip";
        skipNum --;
      }
    }
  }
}