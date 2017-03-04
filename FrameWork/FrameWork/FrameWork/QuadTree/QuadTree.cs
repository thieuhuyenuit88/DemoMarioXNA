using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FrameWork.FrameWork.Object;

namespace FrameWork.FrameWork.QuadTree
{
    class QuadTree
    {
        QuadTreeNode m_root;
        Rectangle m_rectangle;
        public QuadTree(Rectangle rectangle)
        {
            m_rectangle = rectangle;
            m_root = new QuadTreeNode(m_rectangle);
        }
        public int Count { get { return m_root.Count; } }

        public void Insert(MyObject item)
        {
            m_root.Insert(item);
        }

        public List<MyObject> Query(Rectangle area)
        {
            return m_root.Query(area);
        }

    }
}
