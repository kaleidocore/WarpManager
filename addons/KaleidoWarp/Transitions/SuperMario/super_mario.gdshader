shader_type canvas_item;

uniform float progress : hint_range(0.0, 1.0) = 0.0;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
uniform sampler2D image : hint_default_transparent;
uniform float speed : hint_range(1.0, 100.0) = 50.0;

void fragment() {
    float t = progress * 2.0;

    vec2 resolution = 1.0 / SCREEN_PIXEL_SIZE;
    float block_size = floor(pow(t, 1.4) * speed) * 2.0 + 0.01;

    vec2 uv = SCREEN_UV;
    uv *= resolution / block_size;
    uv = floor(uv);
    uv /= resolution / block_size;
    uv += block_size * 0.002;

    vec4 pixelated = texture(SCREEN_TEXTURE, uv);

    float mix_factor = smoothstep(1.0, 2.0, t);
    vec4 img = texture(image, UV);
    vec4 target = mix(COLOR, img, img.a);

    COLOR = mix(pixelated, target, mix_factor);
}