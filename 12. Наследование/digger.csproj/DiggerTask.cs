using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 999;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var key = Game.KeyPressed;
            var delta = new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
            if (key == System.Windows.Forms.Keys.Left)
            {
                delta.DeltaX = -1;
            }
            else if (key == System.Windows.Forms.Keys.Right)
            {
                delta.DeltaX = 1;
            }
            else if (key == System.Windows.Forms.Keys.Up)
            {
                delta.DeltaY = -1;
            }
            else if (key == System.Windows.Forms.Keys.Down)
            {
                delta.DeltaY = 1;
            }

            if (!CanWalkTo(x + delta.DeltaX, y + delta.DeltaY)) delta.DeltaX = delta.DeltaY = 0;
            else if (Game.Map.GetValue(x + delta.DeltaX, y + delta.DeltaY) is Gold) Game.Scores += 10;

            return delta;
        }

        private bool CanWalkTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y) return false;
            var cell = Game.Map.GetValue(x, y);
            return !(cell is Sack);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return (conflictedObject is Sack) || (conflictedObject is Monster);
        }
    }

    public class Sack : ICreature
    {
        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var delta = new CreatureCommand { DeltaX = 0, DeltaY = 1, TransformTo = this };
            
            if (CanFallTo(x + delta.DeltaX, y + delta.DeltaY)) ++FallingTime;
            else
            {
                if (FallingTime > 1) delta.TransformTo = new Gold();
                FallingTime = 0;
                delta.DeltaY = 0;
            }

            return delta;
        }

        int FallingTime = 0;

        private bool CanFallTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y)
                return false;
            var cell = Game.Map.GetValue(x, y);
            return (cell == null) ||
                (IsFalling() && ((cell is Player) || (cell is Monster)));
        }

        public bool IsFalling()
        {
            return FallingTime > 0;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            var delta = new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
            if (PlayerIsOnSection(0, 0, x, Game.MapHeight) && CanWalkTo(x - 1, y))
                delta.DeltaX = -1;
            else if (PlayerIsOnSection(x + 1, 0, Game.MapWidth, Game.MapHeight) && CanWalkTo(x + 1, y))
                delta.DeltaX = 1;
            else if (PlayerIsOnSection(0, 0, Game.MapWidth, y) && CanWalkTo(x, y - 1))
                delta.DeltaY = -1;
            else if (PlayerIsOnSection(0, y + 1, Game.MapWidth, Game.MapHeight) && CanWalkTo(x, y + 1))
                delta.DeltaY = 1;
            return delta;
        }

        public static bool PlayerIsOnSection(int x0, int y0, int x1, int y1)
        {
            for (int x = x0; x < x1; x++)
            {
                for (int y = y0; y < y1; y++)
                {
                    var cell = Game.Map.GetValue(x, y);
                    if (cell is Player) return true;
                }
            }
            return false;
        }

        private bool CanWalkTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y)
                return false;
            var cell = Game.Map.GetValue(x, y);
            return (cell == null) ||
                !((cell is Player) || (cell is Monster) || (cell is Sack));
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return (conflictedObject is Monster) ||
            ((conflictedObject is Sack) && (conflictedObject as Sack).IsFalling());
        }
    }

}
