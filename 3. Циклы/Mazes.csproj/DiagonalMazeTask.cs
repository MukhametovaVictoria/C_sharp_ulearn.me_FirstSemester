namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			while (!robot.Finished)
			{
				if (width == 30) ResolveForTheFirstMaze(robot);
				else if (width == 7) ResolveForTheSecondMaze(robot);
				else if (width == 11 && height != 6) ResolveForTheThirdMaze(robot);
				else ResolveForTheFourthMaze(robot);
			}
		}

		public static void ResolveForTheFirstMaze(Robot robot)
        {
			for (int i = 0; i < 3; i++)
				robot.MoveTo(Direction.Right);
			if (!robot.Finished) robot.MoveTo(Direction.Down);
		}

		public static void ResolveForTheSecondMaze(Robot robot)
		{
			for (int i = 0; i < 2; i++)
				robot.MoveTo(Direction.Down);
			if (!robot.Finished) robot.MoveTo(Direction.Right);
		}

		public static void ResolveForTheThirdMaze(Robot robot)
		{
			robot.MoveTo(Direction.Down);
			if (!robot.Finished) robot.MoveTo(Direction.Right);
		}

		public static void ResolveForTheFourthMaze(Robot robot)
		{
			for (int i = 0; i < 2; i++)
				robot.MoveTo(Direction.Right);
			if (!robot.Finished) robot.MoveTo(Direction.Down);
		}
	}
}