# class DirectionVector:
# 	def __init__(self):
# 		# self.directionIndexes = [
# 		# 	DIRECTION_UP, 
# 		# 	DIRECTION_DOWN, 
# 		# 	DIRECTION_LEFT, 
# 		# 	DIRECTION_RIGHT
# 		# ]
# 		self.directionVectors = [(0,-1), (0, 1), (-1, 0), (1, 0)]
# 		self.up = self.directionVectors[DIRECTION_UP]
# 		self.down = self.directionVectors[DIRECTION_DOWN]
# 		self.left = self.directionVectors[DIRECTION_LEFT]
# 		self.right = self.directionVectors[DIRECTION_RIGHT]
# 	# def Up(self):
# 	# 	return self.directionVectors[DIRECTION_UP]
# 	# def Down(self):
# 	# 	return self.directionVectors[DIRECTION_DOWN]
# 	# def Left(self):
# 	# 	return self.directionVectors[DIRECTION_LEFT]
# 	# def Right(self):
# 	# 	return self.directionVectors[DIRECTION_RIGHT]

DIRECTION_VECTOR_UP : tuple[int, int] = (0, -1)
DIRECTION_VECTOR_DOWN : tuple[int, int] = (0, 1)
DIRECTION_VECTOR_LEFT : tuple[int, int] = (-1, 0)
DIRECTION_VECTOR_RIGHT : tuple[int, int] = (1, 0)

DIRECTION_UP : int = 0
DIRECTION_DOWN : int = 1
DIRECTION_LEFT : int = 2
DIRECTION_RIGHT : int = 3

DIRECTION_VECTORS : list[tuple[int, int]] = [
	DIRECTION_VECTOR_UP,
	DIRECTION_VECTOR_DOWN,
	DIRECTION_VECTOR_LEFT,
	DIRECTION_VECTOR_RIGHT,
]