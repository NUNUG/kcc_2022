class Settings:
	"""These are game settings.  You can change these to adjust the dynamics of the game."""
	PRIMARY_DELAY_MS : int = 200
	INITIAL_SIZE : int = 10
	SCALE : int = 4
	BLOCK_SIZE : int = 8
	STEAKS_PER_MAZE : int = 5
	SPEEDUP_ON_LEVELUP : int = 10
	GROWTH_ON_LEVELUP : int = 15

class Paths:
	"""These are the locations on disk of all the game assets, such as sounds and graphics."""
	GRAPHICS_STEAK_PATH : str = "../assets/graphics/steak.png"
	GRAPHICS_WALL_PATH : str = "../assets/graphics/wall.png"

	GRAPHICS_BODY_UP_PATH : str = "../assets/graphics/body-up.png"
	GRAPHICS_BODY_DOWN_PATH : str = "../assets/graphics/body-down.png"
	GRAPHICS_BODY_LEFT_PATH : str = "../assets/graphics/body-left.png"
	GRAPHICS_BODY_RIGHT_PATH : str = "../assets/graphics/body-right.png"

	GRAPHICS_HEAD_UP_PATH : str = "../assets/graphics/head-up.png"
	GRAPHICS_HEAD_DOWN_PATH : str = "../assets/graphics/head-down.png"
	GRAPHICS_HEAD_LEFT_PATH : str = "../assets/graphics/head-left.png"
	GRAPHICS_HEAD_RIGHT_PATH : str = "../assets/graphics/head-right.png"

	GRAPHICS_TONGUE_UP_PATH : str = "../assets/graphics/tongue-up.png"
	GRAPHICS_TONGUE_DOWN_PATH : str = "../assets/graphics/tongue-down.png"
	GRAPHICS_TONGUE_LEFT_PATH : str = "../assets/graphics/tongue-left.png"
	GRAPHICS_TONGUE_RIGHT_PATH : str = "../assets/graphics/tongue-right.png"

	GRAPHICS_TAIL_UP_PATH : str = "../assets/graphics/tail-up.png"
	GRAPHICS_TAIL_DOWN_PATH : str = "../assets/graphics/tail-down.png"
	GRAPHICS_TAIL_LEFT_PATH : str = "../assets/graphics/tail-left.png"
	GRAPHICS_TAIL_RIGHT_PATH : str = "../assets/graphics/tail-right.png"

	FONT_PATH : str = "../assets/fonts/SnakeStitch.ttf"
	SOUND_EAT_PATH : str = "../assets/sounds/eat.wav"

