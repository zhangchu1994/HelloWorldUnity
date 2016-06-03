using UnityEngine;
using System.Collections;
public enum enCurveEquation{
	/// <summary>
	/// 阿基米德螺旋曲线
	/// </summary>
	ArchimedeanSpiral,	
	/// <summary>
	/// 抛物线公式
	/// </summary>
	Parabola,
	/// <summary>
	/// 正旋公式
	/// </summary>
	Sinusoid,
	/// <summary>
	/// 心形曲线
	/// </summary>
	Cardioid,
	/// <summary>
	/// 阻尼
	/// </summary>
	Damping,
}
/// <summary>
/// 各种曲线方程
/// </summary>
public class CurveEquation{
	private static float _PI;
	private static float PI{
		get{ 
			if (_PI == 0)
				_PI = Mathf.PI;
			return _PI;
		}
	}

	/// <summary>
	/// 阿基米德曲线
	/// 蚊香线
	/// </summary>
	/// <returns>The spiral.</returns>
	/// <param name="time">Time.</param>
	/// <param name="agr1">Agr1.</param>
	public static Vector3 ArchimedeanSpiral(float time,float arg1){
		Vector3 pos = Vector3.zero;
		float r = arg1 * (1 + time);
		pos.x = r * Mathf.Cos (time * 2*PI);
		pos.y = r * Mathf.Sin (time * 2*PI);
		return pos;
	}
	/// <summary>
	/// 抛物线公式
	/// P 焦准距  焦点到抛物线的准线的距离
	/// </summary>
	/// <param name="time">Time.</param>
	public static Vector3 Parabola(float time,float P){
		Vector3 pos = Vector3.zero;
		pos.y = (time*time)/(2* P);
		pos.x = time;
		return pos;
	}
	public static Vector3 Sinusoid(float time,float amplitude,float setover,float angularspeed,float initialphase){
		Vector3 pos = Vector3.zero;
		pos.x = time;
		pos.y = amplitude*Mathf.Sin(angularspeed*time + initialphase) + setover;
		return pos;
	}
	public static Vector3 Cardioid(float time,float CardioidR,float CardioidL){
//		CardioidL++;
		Vector3 pos = Vector3.zero;
		pos.y = CardioidR * (CardioidL * Mathf.Cos (time) - Mathf.Cos (CardioidL * time));
		pos.x = CardioidR * (CardioidL * Mathf.Sin (time) - Mathf.Sin (CardioidL * time));
		return pos;
	}

	public static Vector3 Damping(float time,float swing,float damping,float hz){
		Vector3 pos = Vector3.zero;
		pos.x = time;
		pos.y = swing * Mathf.Pow (damping, -time) * Mathf.Sin (hz * time);
		return pos;
	}
}
