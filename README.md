AR_experimentフォルダには、VuforiaによるARマーカーを用いた、子供向けサービス「おつかい体験アプリ」の実装コードを格納。
商品をカメラにかざすと、商品がカードから浮いているように見え、購入操作が可能になる（購入数量も決定可能）
所持金がランダムに設定され、その所持金から購入金額に対して、最適な出し方ができるように練習できる。
購入金額よりも高い金額を提出することで購入できるが、お釣りの枚数が最適でない場合、指摘を行う。


Baseball_Gameには、Nintendo Switchのジョイコンを用いた、体を動かす野球ゲームの実装コードを格納。
8アウトをとられるまでに、何点取ることが出来るかを競うバッティングゲームになり、ジョイコンを振ることでバットをスイングすることが出来る（Wiiスポーツようなイメージ）。
ジョイコンのボタンを押すことでもバットをスイングすることが出来る。
ジョイコンの振る角度によって、ゲーム内のバットのスイングの軌道が変化する。具体的には、上向きにジョイコンを振った場合アッパースイングになり、下向きにジョイコンを振った場合ダウンスイングに、それ以外はレベルスイング(平行スイング)になる。
壁や風船に「3ベース」「アウト」「ホームラン」などが記してあり、打ったボールがそれらに衝突すると実際にアウトやヒットになる「リアル野球盤方式」
ゲーム終了後、獲得した得点と同時にプレーヤーのスイング軌道に対する解析結果も表示する。
