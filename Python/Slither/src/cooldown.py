class Cooldown:
	def __init__(self, time_function, cooldown_ticks: int):
		self.get_current_time = time_function
		self.cooldown_ticks : int = cooldown_ticks
		self.reset()
	def reset(self):
		self.start_ticks = self.get_current_time()
	def elapsed(self):
		now_ticks = self.get_current_time()
		elapsed_this_time = now_ticks - self.start_ticks
		return elapsed_this_time
	def expired(self):
		result : bool = self.elapsed() >= self.cooldown_ticks
		return result
	