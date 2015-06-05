# UPFTHeadTracker version 1.0.1

## How to Use

Android版ではcardboard.jarを使用します。<br/>
[https://github.com/googlesamples/cardboard](https://github.com/googlesamples/cardboard)<br/>
上記URLからcardboard.jarをダウンロードして、/Plugins/Android/ディレクトリ内にコピーしてください。<br/>
（v0.5.1で動作を確認済みです。v0.5.0は非対応です。） <br/>

使い方はいたってシンプルです。<br/>
/UPFTHeadTracker/Prefabs/UPFTHeadTracker.prefabをHierarchyにドラッグ＆ドロップしてください。<br/>

Inspector上では以下の設定が可能です。

* Camera Mode
	* Normal : レンダリングするカメラが１つです
	* Stereoscopic : レンダリングするカメラが２つです。
	
* Ipd
	* Camera ModeがStereoscopicの場合に、２つのカメラの距離を設定できます。
	
* Near Clip Plane
	* カメラのNear Clip Planeを設定します。

* Far Clip Plane
	* カメラのFar Clip Planeを設定します。

* Background Color
	* カメラのBackground Colorを設定します。

## Release History

### 1.0.0

Initial Release

### 1.0.1

Cardboard SDK for Android v0.5.1 supported. And v0.5.0 is not supported no longer.

