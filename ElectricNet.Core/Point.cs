using System;

namespace ElectricNet.Core
{
    /// <summary>
    /// 方向
    /// </summary>
    public enum Direction
    {
        Left = -1,
        Right = 1,
        Up = -2,
        Down = 2,
    }

    /// <summary>
    /// 表示点的坐标
    /// </summary>
    public struct Point
    {
        public uint X;
        public uint Y;

        /// <summary>
        /// 检查此点是否在允许范围内
        /// </summary>
        /// <param name="size">表示尺寸的点。类似于 string 的 Length 的值</param>
        /// <returns></returns>
        public bool IsLegal(Point size)
        {
            // X or Y = 2^32-1 是非法点,仅能用于表示尺寸,一定不能过 IsLegal 
            return X < size.X && Y < size.Y;
        }

        /// <summary>
        /// 检查此点是否在区域内
        /// </summary>
        /// <param name="area">区域</param>
        /// <returns></returns>
        public bool IsLegal(Area area)
        {
            return this.IsLegal(area.Size);
        }

        /// <summary>
        /// 获取指定方向的相邻的点
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns></returns>
        public Point GetNextPoint(Direction direction)
        {
            unchecked
            {
                switch (direction)
                {
                    case Direction.Left:
                        return new Point { X = this.X - 1, Y = this.Y };
                    case Direction.Right:
                        return new Point { X = this.X + 1, Y = this.Y };
                    case Direction.Up:
                        return new Point { X = this.X, Y = this.Y - 1 };
                    case Direction.Down:
                        return new Point { X = this.X, Y = this.Y + 1 };
                    default:
                        //这个没用
                        return this;
                }
            }
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }
    }
}
