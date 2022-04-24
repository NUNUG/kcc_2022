import direction

class Snake:
	def __init__(self, size, max_size, position, direction):
		self.reset(size, max_size, position, direction)
	def reset(self, size : int, max_size: int, position : tuple[int, int], direction : tuple[int, int]):
		self.size = size
		self.max_size = max_size
		self.head_position = position
		self.direction = direction
		self.blocks = [(position, direction)]
	def move(self, direction):
		# Add a new section for the head in the new location.
		#old_head_position = self.head_position
		new_head_position = (self.head_position[0] + direction[0], self.head_position[1] + direction[1])
		self.head_position = new_head_position
		self.blocks.append(self.head_position)

		# Unless we're still growing, move the tail by removing the first item in the snake.
		if (self.size < self.max_size):
			#old_tail_position = self.blocks[0]
			# if (len(self.blocks) >= 2):
			# 	new_tail_position = self.blocks[1]
			# else:
			# 	new_tail_position = old_tail_position
			self.blocks = self.blocks[1:]

		self.size = len(self.blocks)
		

	def grow(self, grow_by):
		self.max_size = self.max_size + grow_by