// create by 长生但酒狂
// create time : 2019-12-13 22:00
// ------------------------------【片元着色器】----------------------------
// GLSH 画布边界点

#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕大小
uniform vec2 u_resolution;

uniform float u_time;

vec3 colorA = vec3(1.000,0.0,0.000);
vec3 colorB = vec3(0.000,0.000,1.0);

// 线的颜色
vec3 lineColor1 = vec3(1.0,0.0,0.0);
vec3 lineColor2 = vec3(128.0,0.0,128.0);

// 线宽度
float lineWidth = 0.1;


//正弦波函数
float sineWave(float x){
	//频率
	float frequency = 3.0;
	//震幅
	float amplitude = 0.2;
	return sin(x * frequency) * amplitude + 0.5;
}

void main() {
	// gl_FragCoord: 当前像素坐标
	// 对坐标进行规范化，把坐标控制在[0,1]范围.
	vec2 st = gl_FragCoord.xy/u_resolution;
	
    //正弦
   float y = sineWave(st.x);

	// 阙值, 判断该像素是否属于线: 0为线 , 1为背景
	float threshold = step(lineWidth, abs(y-st.y));
	float ratio = abs(y-st.y);
	float val =  1.0 - threshold ;
	// 线的颜色
   vec3 _lineColor;
   _lineColor.r = mix(lineColor1.r, lineColor2.r, y) * val;
   _lineColor.g = mix(lineColor1.g, lineColor2.g, y) * val;
   _lineColor.b = mix(lineColor1.b, lineColor2.b, y) * val;

	// 背景色, 里面mix混合两种颜色,类似插值, y是控制变量
   vec3 color = mix(colorA, colorB, y) * threshold;
    
	gl_FragColor = vec4(vec3(color +  _lineColor ),1);
}