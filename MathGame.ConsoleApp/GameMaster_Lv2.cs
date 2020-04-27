using System;

// Lv2ゲームを実行するGameMasterクラス
// Lv2のゲームルール
// 1~10(計10枚)の各１枚ずつあるカードを均等(5枚)にランダムに配布する
// 場に出ている数より大き数を出すことが出来る
// 出来ない場合、場に保持されている枚数分が失点となる, 場の数は0からリスタートされる
// どちらかの手札が0枚になった時点でゲーム終了
namespace MathGame.ConsoleApp
{
  class GameMaster_Lv2 : GameMaster
  {

    // fieldの状態をリセットするメソッド
    void FieldReset()
    {
      fieldCard.Clear();
      fieldNum = 0;
    }
    // Lv2のゲームを実行するメソッド
    public void PlayGame(Player_Lv2 player, CP_Lv2 cp)
    {
      turn = 0;
      playGame = true;
      while(playGame)
      {
        switch(turn)
        {
          // ゲーム開始場面
          case 0:
            cp.ShowCard();
            FieldCard(turn);
            player.ShowCard();
            turn = 1;
            Line();
            break;
          // Playerがカードを出す場面
          case 1:
            TurnName(player);
            player.DiscardCard();
            turn = 3;
            break;
          // CPがカードを出す場面
          case 2:
            cp.ThinkingTime(2);
            cp.DiscardCard();
            turn = 3;
            break;
          // フィールドと手札の更新 と　その後の処理
          case 3:
            
            // ゲーム終了判定
            // どちらかの手札が0枚になった時点でゲーム終了
            // 両方とも出すことが出来ない状態の時
            if(player.card.Count == 0 || cp.card.Count == 0 ||
                player.skipNum == 0 || cp.skipNum == 0)
            {
              nextPlay = "finish";
            }
            // 条件分岐でどのplayerがプレーするか判断
            if(nextPlay.Equals("player"))
            {
              turn = 1;
            }
            else if(nextPlay.Equals("cp"))
            {
              turn = 2;
            }
            else if(nextPlay.Equals("playerSkip")) // プレイヤーがskipを利用した時の処理
            {
              if(fieldCard.Count > 0)
              {
                player.Skip(fieldCard.Count);
              }
              FieldReset();
              turn = 2;
            }
            else if(nextPlay.Equals("cpSkip"))     // CPがskipを利用した時
            {
              if(fieldCard.Count > 0)
              {
                cp.Skip(fieldCard.Count);
              }
              FieldReset();
              turn = 1;
            }
            else if(nextPlay.Equals("finish"))
            {
              turn = 4;
            }
            if(fieldCard.Count > 0)
            {
              CountFieldCard();
            }
            Line();
            cp.ShowCard();
            FieldCard(turn);
            player.ShowCard();
            Line();
            break;
          case 4:
            // playGame = false;
            FinishGame();
            break;
        }
      }
      GameResult(player, cp);
      Reset(player, cp);
    }
  }
}