// Author:
// Title:

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0,0.0,1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix( vec3(1.0), rgb, c.y);
}

// 创建齿轮
vec3 createGear(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
	//造型函数
   	float f = smoothstep(-0.484,1., cos(angle*10.0+u_time*50.))*0.080+0.372;

    vec3 col = hsb2rgb(vec3((pos.y),1.000,1.0));

    return vec3( 1.-smoothstep(f,f+0.02,radius)) *col;
}


//
void main(){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    vec3 gearleLists = vec3(0.030,0.063,0.165);
    float spaceX = 0.1;
    float speed = 0.672;
	 for(int i = 0;i<30;i ++){
      spaceX += 0.1;
      vec2 pos = vec2(spaceX,spaceX);
      pos.x =  abs(sin(speed+u_time+spaceX));
      pos.y =  abs(cos(speed*u_time*spaceX));
      vec3 gear = createGear(pos,25.128,uv);
      gearleLists += gear;
    }

    gl_FragColor = vec4(gearleLists , 1.0);
}
