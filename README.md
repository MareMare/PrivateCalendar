# PrivateCalendar

⚠️ **_This repository is intended for me._**

[![.NET Build & UnitTests](https://github.com/MareMare/PrivateCalendar/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MareMare/PrivateCalendar/actions/workflows/ci.yml)

## ざっくり

とあるカレンダーから不定期な予定を抽出して ics ファイル、個人用 Google カレンダーへ出力するツールです。

* 入力元の種類
  * CSV
  * SQL Server
* 出力先の種類
  * ics ファイル
  * 個人用 Google カレンダー

## 不定期な予定について

以下表のように、個人的に見落としやすい不定期な予定を抽出します。

|日付|入力元の設定|不定期？|抽出種別|
|--|:--|:--|--|
|平日|なし|||
|平日|出勤日|||
|平日|休日|はい|休日|
|平日|祝日|||
|平日|有給|はい|有給|
|||||
|週末|なし|はい|出勤日|
|週末|出勤日|はい|出勤日|
|週末|休日|||
|週末|祝日|||
|週末|有給|はい|有給|

## iCalenderファイル（icsファイル）について

抽出した不定期な予定を ics ファイルとして出力することで任意の予定表へインポートが可能となります。
* Outlook/Outlook.com の予定表
* その他任意の予定表

## 個人用 Google カレンダについて

Google Calendar API を利用して個人用のカレンダへ直接出力します。
