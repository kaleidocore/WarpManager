shader_type canvas_item;

uniform float progress : hint_range(0, 1) = 0.0;
uniform sampler2D image : hint_default_transparent;
uniform sampler2D dissolve_texture : hint_default_transparent;
uniform bool flip_x = false;
uniform bool flip_y = false;
uniform bool invert = false;
uniform float feather : hint_range(0.0, 0.25) = 0.1f;

void fragment() {
    vec2 uv = UV;
    if (flip_x) uv.x = 1.0 - uv.x;
    if (flip_y) uv.y = 1.0 - uv.y;
    
    float sample = texture(dissolve_texture, uv).r;
	if(invert) sample = 1.0 - sample;
	float t = mix(-feather, 1.0 + feather, progress);
	float alpha = smoothstep(t - feather, t + feather, sample);
   
    vec4 img = texture(image, UV);
    vec4 base = mix(COLOR, img, img.a);
    COLOR = vec4(base.rgb, base.a * alpha);
}