using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(DebugLine))]
public class CurveTween : CurveTweenBase {
	public enCurveEquation tweenType;
	[Header("阿基米德蚊香线")]
	public float 蚊香半径 = 1;
	public bool 使用蚊香曲线;
	public AnimationCurve 蚊香曲线 = AnimationCurve.Linear(0,0,1,1);
	[Header("抛物线")]
	public float 焦准距 = 10;
	[Header("正弦曲线")]
	public float 振幅 = 2;
	public float 初相 = 1;
	public float 偏距 = 1;
	public float 角速度= 20;
	[Header("心形线")]
	public float 心形半径 = 2;
	public float 花瓣数 = 3;
	[Header("阻尼运动")]
	public float 阻尼振幅 = 3;
	public float 阻尼衰减度 = 1.4f;
	public float 阻尼频率 = 3;
	void Update ()
	{
		switch((int)tweenType){
		case (int)enCurveEquation.ArchimedeanSpiral:
			transform.position = CurveEquation.ArchimedeanSpiral (nowTime*_time, 使用蚊香曲线?蚊香曲线.Evaluate(nowTime)*蚊香半径:蚊香半径);
			break;
		case (int)enCurveEquation.Parabola:
			//Arg1 趋于0 则抛物线趋于平行线
			transform.position = CurveEquation.Parabola (2*nowTime*_time - _time, 焦准距);
			break;
		case (int)enCurveEquation.Sinusoid:
			transform.position = CurveEquation.Sinusoid (nowTime, 振幅, 偏距,角速度, 初相);
			break;
		case (int)enCurveEquation.Cardioid:
			transform.position = CurveEquation.Cardioid (nowTime * _time, 心形半径,花瓣数);
			break;
		case (int)enCurveEquation.Damping:
			transform.position = CurveEquation.Damping (nowTime * _time, 阻尼振幅,阻尼衰减度,阻尼频率);
			break;
		}
		DebugLine.Instance.AddLine (transform.position);
	} 
}
