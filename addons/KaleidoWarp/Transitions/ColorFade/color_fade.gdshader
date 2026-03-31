shader_type canvas_item;

uniform float progress : hint_range(0, 1) = 0.0;
uniform sampler2D image : hint_default_transparent;

void fragment() {
    vec4 t = texture(image, UV);
    COLOR = mix(COLOR, t, t.a);
    COLOR.a *= progress;
}