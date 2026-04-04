shader_type canvas_item;

uniform float progress : hint_range(0.0, 1.0) = 0.0;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture;
uniform sampler2D image : hint_default_transparent;
uniform int image_fit = 0; // 0=None, 1=Contain, 2=Cover, 3=Stretch
uniform int direction = 0; // 0=Left, 1=Right, 2=Up, 3=Down
uniform bool sticky = false; // If the overlay (image) stays fixed

vec4 sample_image(vec2 uv, vec2 screen_pixel_size) {
    vec2 screen_size = 1.0 / screen_pixel_size;
    vec2 image_size = vec2(textureSize(image, 0));

    if (image_fit == 0) { // None
        vec2 scale = image_size / screen_size;
        uv = (uv - 0.5) / scale + 0.5;
    } else if (image_fit == 1) { // Contain
        float scale = min(screen_size.x / image_size.x, screen_size.y / image_size.y);
        uv = (uv - 0.5) / (image_size * scale / screen_size) + 0.5;
    } else if (image_fit == 2) { // Cover
        float scale = max(screen_size.x / image_size.x, screen_size.y / image_size.y);
        uv = (uv - 0.5) / (image_size * scale / screen_size) + 0.5;
    }

    bool oob = image_fit != 3 && (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0);
    return oob ? vec4(0.0) : texture(image, uv);
}

void fragment() {
    vec2 dir;
    if      (direction == 0) dir = vec2( 1.0,  0.0);  // Left
    else if (direction == 1) dir = vec2(-1.0,  0.0);  // Right
    else if (direction == 2) dir = vec2( 0.0,  1.0);  // Up
    else                     dir = vec2( 0.0, -1.0);  // Down

    vec2 offset = progress * dir;

    // Sticky image goes UNDER the scene
    if (sticky) {
        vec4 img = sample_image(UV, SCREEN_PIXEL_SIZE);
        COLOR = mix(COLOR, img, img.a);
    }

    // Scene slides out
    vec2 scene_uv = SCREEN_UV + offset;
    bool in_bounds = scene_uv.x >= 0.0 && scene_uv.x <= 1.0 &&
                     scene_uv.y >= 0.0 && scene_uv.y <= 1.0;
    if (in_bounds) {
        vec4 scene = texture(SCREEN_TEXTURE, scene_uv);
        COLOR = mix(COLOR, scene, scene.a);
    }

    // Non-sticky image slides in from the opposite side, OVER the scene
    if (!sticky) {
        vec2 img_uv = UV + (progress - 1.0) * dir;
        vec4 img = sample_image(img_uv, SCREEN_PIXEL_SIZE);
        COLOR = mix(COLOR, img, img.a);
    }
}