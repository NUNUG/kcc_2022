class Cooldown:
	def __init__(self, time_function, cooldown_ticks: int):
		self.get_current_time = time_function
		self.cooldown_ticks : int = cooldown_ticks
		self.elapsed_ticks : int = 0
		self.start()
	def start(self):
		self.reset()
		self.resume()
	def reset(self : bool):
		self.paused = True
		self.elapsed_ticks = 0
	def resume(self):
		self.Paused = False
		self.start_ticks = self.get_current_time()
	def pause(self):
		now_ticks = self.get_current_time()
		self.paused = True
		elapsed_this_time = now_ticks - self.start_ticks
		self.elapsed_ticks = self.elapsed_ticks + elapsed_this_time
	def elapsed(self):
		if self.paused:
			now_ticks = 0
		else:
			now_ticks = self.get_current_time
			
		elapsed_this_time = now_ticks - self.start_ticks + self.elapsed_ticks
	def expired(self):
		return self.elapsed >= self.cooldown_ticks
	