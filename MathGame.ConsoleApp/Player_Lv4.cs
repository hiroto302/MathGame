using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  class Player_Lv4 : Player_Lv3
  {
    // 保持しているカードを場に出すメソッド
    // 場の値以上のカードのみ出すこが可能
    // 出したカードを手持ちから削除し、場に表示する
    // 同じ値のカードは同時に出すことが出来る、ただし、その合計の値が場の数値以上であれ必要がある。
    // そして、新たに場に表示される値は合計値ではなく一枚あたりの数字のみ表示する
    // new 機能
    // 場に出すことが出来た時、山札から手札が6枚になるようにする　メソッドを分けていいかも
    // 場に出すことが出来なかった時、場が保持している数を失点に追加
    // そして、改めて場をリセットし自由な数からだす
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
        card.RemoveAt(i);
        // 同等のカードが手札にあるか判定
        for(int j = 0; j < card.Count; j++)
        {
          if(card[j] == num)
          {
            sameNumber = true;
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
        //選択した値が手札にあるか 0を選択したか
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
          //場に出ている数以上　0入力 判定
          if(n > GameMaster.fieldNum || n == 0 || (discard == true && sameNumbers.Find(i => i == n) * 2 > GameMaster.fieldNum))
          {
            discard = false;
            break;
          }
          else
          {
            discard = false;
            Console.WriteLine("場に出ている数より大きい数を選択 or 無い場合は,「0」を入力");
          }
        }
        else if( discard == false)
        {
          Console.WriteLine("手札にあるカードを選択せよ");
        }
      }
      // 場にカードを出せた時
      if(n > 0)
      {
        // 同じ値がある時、その値を同時に出すか
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
          else if(n <= GameMaster.fieldNum)
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
      // Lv4では, 0入力を場に出すことができなことを意味する
      else if(n == 0)
      {
        // Console.WriteLine("スキップします");
        // GameMaster.nextPlay = "playerSkip";
        // skipNum --;
        GameMaster.nextPlay = "playerRestart";
      }
    }

    // 場に出した時手札が６枚になるように、山札からカードを手札に加える
    public void Draw(List<int> number)
    {
      Random random = new Random();
      // 手札が６枚でない時、かつ、山札にカードが残っている時下記の処理を実行
      if(card.Count != 6 && number != null)
      {
        // 手札が6枚になるまで山札から引く
        while(card.Count < 6)
        {
          if(number.Count == 0)
          {
            break;
          }
          int r = random.Next(number.Count);
          card.Add(number[r]);
          Console.WriteLine(number[r] + "をドローした");
          number.RemoveAt(r);
        }
      }
    }
    // 場に出すことが出来なかった時の処理 場が保持するカードの枚数分失点に追加
    public void AddPoint(int n)
    {
      Console.WriteLine("場に出すことが出来ないので{0}失点が追加されました", GameMaster.fieldCard.Count);
      point += n;
      Console.WriteLine("現在の失点 : {0}", point);
    }
  }
}