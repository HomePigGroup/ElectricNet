using System;

namespace ElectricNet.Core
{
    public class Area
    {
        private bool[,] _area;

        /// <summary>
        /// 边界点
        /// </summary>
        public readonly Point Size;

        /// <summary>
        /// 生成空白区域
        /// </summary>
        /// <param name="size">表示尺寸的点</param>
        public Area(Point size)
        {
            Size = size;
            _area = new bool[size.X, size.Y];
        }

        /// <summary>
        /// 用指定的数组生成区域
        /// </summary>
        /// <param name="origin">二维数组</param>
        public Area(bool[,] origin)
        {
            Size.X = (uint)origin.GetLength(0);
            Size.Y = (uint)origin.GetLength(1);
            _area = (bool[,])origin.Clone();
        }

        /// <summary>
        /// 获取某个点在区域内对应位置的值
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool this[Point point]
        {
            get { return _area[point.X, point.Y]; }
            set { _area[point.X, point.Y] = value; }
        }

        /// <summary>
        /// 获取在区域内某个坐标对应位置的值
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public bool this[uint X, uint Y]
        {
            get { return _area[X, Y]; }
            set { _area[X, Y] = value; }
        }

        public static Area operator &(Area area1, Area area2)
        {
            if (area1.Size != area2.Size)
            {
                throw new ArgumentException("The sizes of the areas are not equal.");
            }
            Area area = new Area(area1.Size);
            for(uint ix = 0; ix < area.Size.X; ix++)
            {
                for(uint iy = 0; iy < area.Size.Y; iy++)
                {
                    area[ix, iy] = (area1[ix, iy] && area2[ix, iy]);
                }
            }
            return area;
        }

        public static Area operator |(Area area1, Area area2)
        {
            if (area1.Size != area2.Size)
            {
                throw new ArgumentException("The sizes of the areas are not equal.");
            }
            Area area = new Area(area1.Size);
            for (uint ix = 0; ix < area.Size.X; ix++)
            {
                for (uint iy = 0; iy < area.Size.Y; iy++)
                {
                    area[ix, iy] = (area1[ix, iy] || area2[ix, iy]);
                }
            }
            return area;
        }

        public static Area operator !(Area area1)
        {
            Area area = new Area(area1.Size);
            for (uint ix = 0; ix < area.Size.X; ix++)
            {
                for (uint iy = 0; iy < area.Size.Y; iy++)
                {
                    area[ix, iy] = !area1[ix, iy];
                }
            }
            return area;
        }

        public static Area operator -(Area area1, Area area2)
        {
            if (area1.Size != area2.Size)
            {
                throw new ArgumentException("The sizes of the areas are not equal.");
            }
            Area area = new Area(area1.Size);
            for (uint ix = 0; ix < area.Size.X; ix++)
            {
                for (uint iy = 0; iy < area.Size.Y; iy++)
                {
                    area[ix, iy] = (area1[ix, iy] && !area2[ix, iy]);
                }
            }
            return area;
        }

        public static Area operator +(Area area1, Area area2)
        {
            return (area1 | area2);
        }
    }
}